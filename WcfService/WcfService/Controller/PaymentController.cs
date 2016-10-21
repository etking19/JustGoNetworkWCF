using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WcfService.Constant;
using WcfService.Model;
using WcfService.Model.BillPlz;
using WcfService.Utility;

namespace WcfService.Controller
{
    public class PaymentController : BaseController
    {
        public Response RequestPayment(string orderId)
        {
            try
            {
                // get info about the order
                var jobId = Utils.DecodeUniqueId(orderId);

                // get from existing database to see if record existed
                var previousResult = paymentsDao.GetByJobId(jobId);
                if (previousResult != null)
                {
                    response.payload = previousResult.url;
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                var jobDetails = jobDetailsDao.GetByJobId(jobId);
                if (jobDetails == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                    return response;
                }

                var ownerDetails = userDao.GetUserById(jobDetails.ownerUserId);
                if (ownerDetails == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                // send billplz request
                var request = WebRequest.Create("https://www.billplz.com/api/v3/bills") as HttpWebRequest;

                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";

                var usernameEncoded = Utils.Base64Encode(System.Configuration.ConfigurationManager.AppSettings["BillPlzApi"]);
                request.Headers.Add("authorization", "Basic " + usernameEncoded + ":");

                var obj = new
                {
                    collection_id = System.Configuration.ConfigurationManager.AppSettings["BillPlzCollectionId"],
                    description = string.Format("Booking id: {0}. Created on: {1}", orderId, jobDetails.creationDate),
                    email = ownerDetails.email,
                    name = ownerDetails.displayName,
                    amount = jobDetails.amount * 100,
                    callback_url = System.Configuration.ConfigurationManager.AppSettings["BillPlzCallbackUrl"],
                    reference_1_label = "orderId",
                    reference_1 = orderId,
                    reference_2_label = "mobile",
                    reference_2 = ownerDetails.contactNumber
                };

                var param = javaScriptSerializer.Serialize(obj);
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(param);

                string responseContent = null;
                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var responseApi = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(responseApi.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                            Bill jsonObj = JsonConvert.DeserializeObject<Bill>(responseContent);

                            // add to database
                            paymentsDao.AddOrUpdate(jobId, jsonObj);

                            response.payload = jsonObj.url;
                            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                            return response;
                        }
                    }
                }
                catch (WebException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(e.Response.GetResponseStream()).ReadToEnd());

                    DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                    DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
                }
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
           

            response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            return response;
        }

        public void PaymentCallback(Bill bill)
        {
            var jobId = Utils.DecodeUniqueId(bill.reference_1);
            var jobDetails = jobDetailsDao.GetByJobId(jobId);
            if (jobDetails == null)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Critical, string.Format("Payment callback does not found job details. Job id: {0}, PaymentId: {1}. {2}", jobId, bill.id, bill));
                return;
            }

            // cross check the job id match with exisiting payment id
            var dbBill = paymentsDao.Get(bill.id);
            if (dbBill == null ||
                dbBill.reference_1 != jobId)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Critical, string.Format("Payment callback does not found matched job id. Job id: {0}, PaymentId: {1}. {2}", jobId, bill.id, bill));
                return;
            }

            // cross check with billplz server status is correct
            var request = WebRequest.Create(string.Format("https://www.billplz.com/api/v3/bills/{0}", bill.id)) as HttpWebRequest;
            using (var responseApi = request.GetResponse() as HttpWebResponse)
            {
                using (var reader = new StreamReader(responseApi.GetResponseStream()))
                {
                    String responseContent = reader.ReadToEnd();
                    Bill jsonObj = JsonConvert.DeserializeObject<Bill>(responseContent);

                    // add to database
                    var id = paymentsDao.AddOrUpdate(bill.reference_1, jsonObj);
                    if(id == "0")
                    {
                        DBLogger.GetInstance().Log(DBLogger.ESeverity.Critical, string.Format("Payment callback unable to update database. Job id: {0}, PaymentId: {1}. {2}", jobId, bill.id, bill));
                        return;
                    }


                    // update the job details on the amount paid
                    var totalPaidAmount = jobDetailsDao.UpdatePaidAmount(jobId, jsonObj.paid_amount);
                    if (totalPaidAmount < jobDetails.amount)
                    {
                        DBLogger.GetInstance().Log(DBLogger.ESeverity.Warning, string.Format("Payment callback paid amount not same as total amount. Job id: {0}, PaymentId: {1}. {2}", jobId, bill.id, bill));
                        return;
                    }

                    // update the job order status
                    if (false == jobDeliveryDao.UpdateJobStatus(jobId, ((int)Configuration.JobStatus.PaymentVerifying).ToString()))
                    {
                        DBLogger.GetInstance().Log(DBLogger.ESeverity.Critical, string.Format("Payment callback unable to update job order status. Job id: {0}, PaymentId: {1}. {2}", jobId, bill.id, bill));
                        return;
                    }

                    // send notification to partners
                    var extraDataPartner = Helper.PushNotification.ConstructExtraData(Helper.PushNotification.ECategories.NewOpenJob, jobId);
                    var partnerListIdentifiers = userDao.GetUserIdentifiersByRoleId(((int)Configuration.Role.CompanyAdmin).ToString());
                    if (int.Parse(jobDetails.jobTypeId) == (int)Configuration.DeliveryJobType.Standard)
                    {
                        Utility.UtilNotification.BroadCastMessage(
                            partnerListIdentifiers.ToArray(),
                            extraDataPartner,
                            NotificationMsg.NewOpenJob_Title,
                            NotificationMsg.NewOpenJob_Desc + string.Format("From: {0}\nTo: {1}\nAmount:{2}",
                                jobDetails.addressFrom[0].address3,
                                jobDetails.addressTo[0].address3,
                                jobDetails.amount * 0.9)
                            );
                    }
                    else if (int.Parse(jobDetails.jobTypeId) == (int)Configuration.DeliveryJobType.Disposal)
                    {
                        Utility.UtilNotification.BroadCastMessage(
                            partnerListIdentifiers.ToArray(),
                            extraDataPartner,
                            NotificationMsg.NewOpenJob_Title,
                            NotificationMsg.NewOpenJob_Desc + string.Format("Dispose items from: {0}\nAmount:{1}",
                                jobDetails.addressFrom[0].address3,
                                jobDetails.amount * 0.9)
                            );
                    }
                }
            }
        }
    }
}