using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Model;
using WcfService.Utility;

namespace WcfService.Controller
{
    public class JobController : BaseController
    {
        private readonly ulong JOB_ID_PAD = 999999;

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
                jobId = decodeUniqueId(uniqueId);
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

        public Response AddJob(Model.JobDetails jobDetails, Model.Address[] addressesFrom,
            Model.Address[] addressTo)
        {
            // first add the user if not existed
            var userId = jobDetails.ownerUserId;
            var userObj = userDao.GetUserById(userId);
            if(userObj == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
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
            var uniqueId = Utility.IdGenerator.Encode(ulong.Parse(jobId) + JOB_ID_PAD);

            response.payload = javaScriptSerializer.Serialize(uniqueId);
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
            var companyId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["companyId"];
            var ownerId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["ownerId"];

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
                var decodedJobId = decodeUniqueId(uniqueId);
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
                var result = jobDetailsDao.GetByOwnerId(ownerId, limit, skip);
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

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];

            var result = jobDetailsDao.GetOpenJobs(limit, skip);
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
            // decode the unique id
            var jobId = decodeUniqueId(uniqueId);

            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.Get(jobId.ToString()));
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
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);

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
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);

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
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);

                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
            }

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

            var jobId = decodeUniqueId(uniqueId);
            jobDeliveryDao.UpdateRating(jobId, rating);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetRating(string uniqueId)
        {
            var jobId = decodeUniqueId(uniqueId);
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.GetRating(jobId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateDeliveryStatus(string jobId, string jobStatusId)
        {
            response.payload = javaScriptSerializer.Serialize(jobDeliveryDao.UpdateStatus(jobId, jobStatusId));
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        private string decodeUniqueId(string uniqueId)
        {
            return (Utility.IdGenerator.Decode(uniqueId) -JOB_ID_PAD).ToString();
        }
    }
}