using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class JobController : BaseController
    {
        public Response GetJobDetails(string limit, string skip)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.Get(limit, skip));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDetails(string jobId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.Get(jobId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDetailsByUniqueId(string uniqueId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.GetByUniqueId(uniqueId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDetailsByDeliveryCompany(string companyId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.GetByDeliveryCompany(companyId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDetailsByOwner(string userId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.GetByOwner(userId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddJobDetails(Model.JobDetails payload)
        {
            // generate unique job id
            var uniqueJobId = payload.ownerUserId.ToString() + "-" + Guid.NewGuid().ToString("N");

            // fill other details
            payload.uniqueId = uniqueJobId;
            payload.createdBy = payload.ownerUserId;

            // add to database
            payload.jobId = jobDetailsDao.Add(payload);

            response.payload = javaScriptSerializer.Serialize(payload);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);

            return response;
        }

        public Response UpdateJobDetails(string jobId, Model.JobDetails payload)
        {
            if(jobDetailsDao.Update(jobId, payload))
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            }
            else
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            }
            
            return response;
        }

        public Response DeleteJobDetails(string jobId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.Get(jobId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddAddressFrom(string userId, Model.Address payload)
        {
            response.payload = javaScriptSerializer.Serialize(addressDao.Add(payload, Dao.AddressDao.EType.From));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetAddressesFromLimit(string userId, string limit, string skip)
        {
            response.payload = javaScriptSerializer.Serialize(addressDao.Get(userId, limit, skip, Dao.AddressDao.EType.From));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddAddressTo(string userId, Model.Address payload)
        {
            response.payload = javaScriptSerializer.Serialize(addressDao.Add(payload, Dao.AddressDao.EType.To));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetAddressesToLimit(string userId, string limit, string skip)
        {
            response.payload = javaScriptSerializer.Serialize(addressDao.Get(userId, limit, skip, Dao.AddressDao.EType.To));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDelivery(string limit, string skip)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.Get(limit, skip));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDelivery(string jobId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.Get(jobId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDeliveryByUniqueId(string uniqueId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetByUniqueId(uniqueId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDeliveryByCompany(string companyId, string limit, string skip, string status)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetByDeliverCompany(companyId, limit, skip, status));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDeliveryByDriver(string driverId, string limit, string skip, string status)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetByDriver(driverId, limit, skip, status));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobDeliveryByStatus(string statusId, string limit, string skip)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetByStatus(statusId, limit, skip));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddJobDelivery(string jobId, string companyId, string driverId)
        {
            try
            {
                response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.Add(jobId, companyId, driverId));
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            }
            catch (Exception e)
            {
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Error, e.Message);
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Info, e.StackTrace);

                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            }

            return response;
        }

        public Response UpdateJobDelivery(string jobId, string companyId, string driverId)
        {
            try
            {
                jobDeliveryDao.Update(jobId, companyId, driverId);
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            }
            catch (Exception e)
            {
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Error, e.Message);
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Info, e.StackTrace);

                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            }

            return response;
        }

        public Response DeleteJobDelivery(string jobId)
        {
            try
            {
                jobDeliveryDao.Delete(jobId);
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            }
            catch (Exception e)
            {
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Error, e.Message);
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Info, e.StackTrace);

                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            }

            return response;
        }


        public Response GetOpenJobs()
        {
            response.payload = javaScriptSerializer.Serialize(jobDetailsDao.GetOpenJobs());
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response SetRating(string uniqueId, float rating)
        {
            if(rating > 5)
            {
                Utility.DBLogger.Log(Utility.DBLogger.ESeverity.Warning, "SetRating, " + uniqueId + "," + rating);
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            jobDeliveryDao.UpdateRating(uniqueId, rating);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetRating(string uniqueId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetRating(uniqueId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateDeliveryStatus(string jobId, string jobStatusId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.UpdateStatus(jobId, jobStatusId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}