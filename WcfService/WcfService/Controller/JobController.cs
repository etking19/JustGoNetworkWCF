using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class JobController : BaseController
    {
        private readonly ulong JOB_ID_PAD = 999999;

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

        public Response GetJobStatus(string uniqueId)
        {
            var jobId = decodeUniqueId(uniqueId);
            var result = jobDetailsDao.Get(jobId);

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

            if(result.enabled == false)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EJobDisabled);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
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
            // add to database
            payload.createdBy = payload.ownerUserId;
            var jobId = jobDetailsDao.Add(payload);

            // unique id was base on job id
            payload.jobId = jobId;
            var ulongjobId = ulong.Parse(jobId);
            payload.uniqueId = Utility.IdGenerator.Encode(ulongjobId + JOB_ID_PAD);

            response.payload = javaScriptSerializer.Serialize(payload);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);

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