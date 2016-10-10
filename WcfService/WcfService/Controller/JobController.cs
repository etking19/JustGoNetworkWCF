using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Constant;
using WcfService.Model;
using WcfService.Model.Google;
using WcfService.Utility;

namespace WcfService.Controller
{
    public class JobController : BaseController
    {
        public Response GetJobStatus()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var uniqueId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["uniqueId"];
            var jobId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobId"];
            if (uniqueId != null)
            {
                jobId = Utils.DecodeUniqueId(uniqueId);
            }

            if (jobId == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            var result = jobDetailsDao.GetJobStatus(jobId);

            if (result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EJobNotFound);
                return response;
            }

            if (result.deleted)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EJobDeleted);
                return response;
            }

            if (result.enabled == false)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EJobDisabled);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateJobDeliveryStatus(string jobId, string statusId, string pickupErrId, string deliverErrId)
        {
            var result = jobDeliveryDao.UpdateJobStatus(jobId, statusId, pickupErrId, deliverErrId);
            if (false == result)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // notify owner status update
            var jobDetails = jobDetailsDao.GetByJobId(jobId);
            var clientIdentifiers = userDao.GetDeviceIdentifier(jobDetails.ownerUserId);
            var uniqueId = Utils.EncodeUniqueId(jobId);
            var msg = NotificationMsg.JobStatusUpdate_Desc + uniqueId;
            if (clientIdentifiers != null)
            {
                // user have app installed and identifier found, send push notification
                var extraData = Helper.PushNotification.ConstructExtraData(Helper.PushNotification.ECategories.OrderStatusUpdate, uniqueId);
                Utility.UtilNotification.BroadCastMessage(clientIdentifiers.ToArray(), extraData, NotificationMsg.NewJob_Title, msg);
            }
            else
            {
                // no device record, send sms instead
                var userObj = userDao.GetUserById(jobDetails.ownerUserId);
                UtilSms.SendSms(userObj.contactNumber, msg);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddJob(Model.JobDetails jobDetails, Model.Address[] addressesFrom,
            Model.Address[] addressTo)
        {
            // first add the user if not existed
            var userId = jobDetails.ownerUserId;
            var userObj = userDao.GetUserById(userId);
            if (userObj == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            // set the correct lorry type
            switch (int.Parse(jobDetails.fleetTypeId))
            {
                case (int)Configuration.LorryType.Lorry_1tonne:
                    jobDetails.fleetTypeId = "1";
                    break;
                case (int)Configuration.LorryType.Lorry_3tonne:
                    jobDetails.fleetTypeId = "2";
                    break;
                case (int)Configuration.LorryType.Lorry_5tonne:
                    jobDetails.fleetTypeId = "3";
                    break;
                default:
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                    return response;
            }

            // get the gps coordinate if not passed in
            // get the state id and country id if not passed in
            foreach (Model.Address address in jobDetails.addressFrom)
            {
                if (address.gpsLongitude == 0 ||
                    address.gpsLatitude == 0 ||
                    address.stateId == null ||
                    address.countryId == null)
                {
                    // request gps cordinate
                    AddressComponents mapsObj = Utils.GetGpsCoordinate(address.address1, address.address2, address.address3, address.postcode);
                    if (mapsObj == null)
                    {
                        // find from local database
                        Postcode postcodeClass = new Postcode();
                        string nameLocal;
                        var result = postcodeClass.PostcodeNameList.TryGetValue(address.postcode, out nameLocal);
                        if (result == false)
                        {
                            response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                            return response;
                        }
                        mapsObj = Utils.GetGpsCoordinate(nameLocal);
                    }

                    if (address.gpsLongitude == 0)
                    {
                        address.gpsLongitude = mapsObj.geometry.location.lng;
                    }

                    if (address.gpsLatitude == 0)
                    {
                        address.gpsLatitude = mapsObj.geometry.location.lat;
                    }

                    if (address.countryId == null)
                    {
                        var countryObj = countryDao.GetCountries().Find(t => t.name.Contains(mapsObj.address_components.Find(c => c.types.Contains("country")).long_name));
                        address.countryId = countryObj.countryId;
                    }

                    if (address.stateId == null)
                    {
                        var stateList = stateDao.GetByCountryId(address.countryId);
                        var stateObj = stateList.Find(t => t.name.Contains(mapsObj.address_components.Find(a => a.types.Contains("administrative_area_level_1")).long_name));
                        if (stateObj == null)
                        {
                            // cannot find from google api, use local database
                            Postcode postcodeClass = new Postcode();
                            string stateLocal;
                            var localDic = postcodeClass.PostcodeList.TryGetValue(address.postcode, out stateLocal);
                            address.stateId = stateList.Find(t => t.name.Contains(stateLocal)).stateId;
                        }
                        else
                        {
                            address.stateId = stateObj.stateId;
                        }
                    }
                }
            }

            foreach (Model.Address address in jobDetails.addressTo)
            {
                if (address.gpsLongitude == 0 ||
                    address.gpsLatitude == 0 ||
                    address.stateId == null ||
                    address.countryId == null)
                {
                    // request gps cordinate
                    AddressComponents mapsObj = Utils.GetGpsCoordinate(address.address1, address.address2, address.address3, address.postcode);
                    if (mapsObj == null)
                    {
                        // find from local database
                        Postcode postcodeClass = new Postcode();
                        string nameLocal;
                        var result = postcodeClass.PostcodeNameList.TryGetValue(address.postcode, out nameLocal);
                        if (result == false)
                        {
                            response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                            return response;
                        }
                        mapsObj = Utils.GetGpsCoordinate(nameLocal);
                    }

                    if (address.gpsLongitude == 0)
                    {
                        address.gpsLongitude = mapsObj.geometry.location.lng;
                    }

                    if (address.gpsLatitude == 0)
                    {
                        address.gpsLatitude = mapsObj.geometry.location.lat;
                    }

                    if (address.countryId == null)
                    {
                        var countryObj = countryDao.GetCountries().Find(t => t.name.Contains(mapsObj.address_components.Find(c => c.types.Contains("country")).long_name));
                        address.countryId = countryObj.countryId;
                    }

                    if (address.stateId == null)
                    {
                        var stateList = stateDao.GetByCountryId(address.countryId);
                        var stateObj = stateList.Find(t => t.name.Contains(mapsObj.address_components.Find(a => a.types.Contains("administrative_area_level_1")).long_name));
                        if (stateObj == null)
                        {
                            // cannot find from google api, use local database
                            Postcode postcodeClass = new Postcode();
                            string stateLocal;
                            postcodeClass.PostcodeList.TryGetValue(address.postcode, out stateLocal);
                            address.stateId = stateList.Find(t => t.name.Contains(stateLocal)).stateId;
                        }
                        else
                        {
                            address.stateId = stateObj.stateId;
                        }
                    }
                }
            }


            // add the job details
            jobDetails.createdBy = userId;
            jobDetails.modifiedBy = userId;
            var jobId = jobDetailsDao.Add(jobDetails);
            if (jobId == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // add the job status
            if(null == jobDetailsDao.AddOrder(jobId, userId))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // add the address from, to
            foreach (Model.Address add in addressesFrom)
            {
                add.createdBy = userId;
                var result = addressDao.Add(add, jobId, userObj.displayName, userObj.contactNumber, Dao.AddressDao.EType.From);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
            }

            foreach (Model.Address add in addressTo)
            {
                add.createdBy = userId;
                var result = addressDao.Add(add, jobId, userObj.displayName, userObj.contactNumber, Dao.AddressDao.EType.To);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
            }

            // generate the unique job id
            var uniqueId = Utils.EncodeUniqueId(jobId);

            // send notification to creator
            var clientIdentifiers = userDao.GetDeviceIdentifier(userId);
            var msg = NotificationMsg.NewJob_Desc + uniqueId;
            if (clientIdentifiers != null)
            {
                // user have app installed and identifier found, send push notification
                var extraData = Helper.PushNotification.ConstructExtraData(Helper.PushNotification.ECategories.OrderCreated, uniqueId);
                Utility.UtilNotification.BroadCastMessage(clientIdentifiers.ToArray(), extraData, NotificationMsg.NewJob_Title, msg);
            }
            else
            {
                // no device record, send sms instead
                UtilSms.SendSms(userObj.contactNumber, msg);
            }

            response.payload = uniqueId;
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);

            return response;
        }

        public Response GetJob()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var jobId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobId"];
            var uniqueId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["uniqueId"];
            var ownerId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["ownerId"];
            
            var jobTypeId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobTypeId"];
            var fromDate = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["fromDate"];
            var toDate = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["toDate"];

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];

            if (jobId != null)
            {
                // get by job id
                var result = jobDetailsDao.GetByJobId(jobId);
                if(result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(uniqueId != null)
            {
                // get by job unique id
                // convert to job id
                var decodedJobId = Utils.DecodeUniqueId(uniqueId);
                var result = jobDetailsDao.GetByJobId(decodedJobId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(ownerId != null)
            {
                // get by creator id
                var result = jobDetailsDao.GetByOwnerId(ownerId, fromDate, toDate, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if (jobTypeId != null)
            {
                var result = jobDetailsDao.GetByJobTypeId(jobTypeId, fromDate, toDate, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(fromDate != null ||
                toDate != null)
            {
                var result = jobDetailsDao.GetByDateRange(fromDate, toDate, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                // get all
                var result = jobDetailsDao.Get(limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetOpenJobs()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var companyId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["companyId"];

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];

            var result = jobDetailsDao.GetOpenJobs(companyId, limit, skip);
            if (result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteJob(string jobId)
        {
            if(jobDetailsDao.Delete(jobId))
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                return response;
            }

            response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            return response;
        }


        public Response GetAddresses()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];

            var userId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["userId"];
            var fromAdd = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["from"];
            var toAdd = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["to"];

            if (userId == null ||
                (fromAdd == null && toAdd == null))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            var type = fromAdd != null ? Dao.AddressDao.EType.From : Dao.AddressDao.EType.To;
            var result = addressDao.Get(userId, limit, skip, type);

            if(result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="jobDetails"></param>
        /// <returns></returns>
        public Response UpdateJob(string jobId, JobDetails jobDetails)
        {
            if (jobId == null ||
                jobDetails == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            // update the job base on input
            jobDetails.jobId = jobId;
            if (false == jobDetailsDao.Update(jobDetails))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // TODO: update the address



            // TODO: inform job delivery company and driver if any changes


            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }


        public Response GetJobDelivery()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];

            var jobid = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobId"];
            var companyId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["companyId"];
            var driverId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["driverId"];
            var statusId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["statusId"];
            var uniqueId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["uniqueId"];

            if (jobid != null)
            {
                var result = jobDeliveryDao.Get(jobid);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(uniqueId != null)
            {
                try
                {
                    var jobId = Utils.DecodeUniqueId(uniqueId).ToString();
                    var result = jobDeliveryDao.Get(jobid);
                    if (result == null)
                    {
                        response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                        return response;
                    }

                    response.payload = javaScriptSerializer.Serialize(result);
                }
                catch (Exception)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                    return response;
                }
            }
            else if(companyId != null)
            {
                var result = jobDeliveryDao.GetByDeliverCompany(companyId, statusId, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(driverId != null)
            {
                var result = jobDeliveryDao.GetByDriver(driverId, statusId, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if(statusId != null)
            {
                var result = jobDeliveryDao.GetByStatus(statusId, limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = jobDeliveryDao.Get(limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddJobDelivery(string jobId, string companyId, string driverId)
        {
            var result = jobDeliveryDao.Add(jobId, companyId, driverId);
            if (result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // pre-caution step to avoid the job delivery cancelled
            jobDeclineDao.Remove(jobId, companyId);

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeclineJobDelivery(string jobId, string companyId)
        {
            var result = jobDeclineDao.Add(jobId, companyId);
            if (result == false)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateJobDelivery(string jobId, string companyId, string driverId)
        {
            if (false == jobDeliveryDao.Update(jobId, companyId, driverId))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteJobDelivery(string jobId)
        {
            if (false == jobDeliveryDao.Delete(jobId))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response SetRating(string uniqueId, float rating)
        {
            if(rating > 5)
            {
                DBLogger.GetInstance().Log(Utility.DBLogger.ESeverity.Warning, "SetRating, " + uniqueId + "," + rating);
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            var jobId = Utils.DecodeUniqueId(uniqueId);
            if(false == jobDeliveryDao.UpdateRating(jobId, rating))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // notify company admin rating update
            var companyAdminsIdentifiers = userDao.GetDeliveryComIdentifierByJobId(jobId, ((int)Configuration.Role.CompanyAdmin).ToString());

            if (companyAdminsIdentifiers.Count > 0)
            {
                var jobDetails = jobDetailsDao.GetByJobId(jobId);
                var extraData = Helper.PushNotification.ConstructExtraData(Helper.PushNotification.ECategories.RatingUpdate, jobId);

                var description = NotificationMsg.JobRating_Desc.Replace("@rating", rating.ToString());
                description = description.Replace("@jobId", jobId.ToString());
                description = description.Replace("@from", jobDetails.addressFrom[0].address3);

                Utility.UtilNotification.BroadCastMessage(
                    companyAdminsIdentifiers.ToArray(),
                    extraData,
                    NotificationMsg.JobRating_Title,
                    description
                    );
            }



            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}