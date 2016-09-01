using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class JobDeliveryDao
    {
        public string Add(string jobId, string companyId, string driverId)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.JobDelivery> Get(string limit, string skip)
        {
            List<Model.JobDelivery> jobDetailsList = new List<Model.JobDelivery>();
            jobDetailsList.Add(new Model.JobDelivery() { jobDeliveryId = "1", companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" });
            jobDetailsList.Add(new Model.JobDelivery() { jobDeliveryId = "2", companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" });

            return jobDetailsList;
        }

        public Model.JobDelivery Get(string id)
        {
            return new Model.JobDelivery() { jobDeliveryId = id, companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" };
        }

        public Model.JobDelivery GetByUniqueId(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public List<Model.JobDelivery> GetByDeliverCompany(string companyId, string limit, string skip, string status)
        {
            throw new NotImplementedException();
        }

        public List<Model.JobDelivery> GetByDriver(string driverId, string limit, string skip, string status)
        {
            throw new NotImplementedException();
        }

        public List<Model.JobDelivery> GetByStatus(string statusId, string limit, string skip)
        {
            throw new NotImplementedException();
        }

        public bool Update(string jobId, string companyId, string driverId)
        {
            return true;
        }

        public void UpdateRating(string uniqueId, float rating)
        {
            throw new NotImplementedException();
        }

        public float GetRating(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(string jobId, string statusId)
        {
            throw new NotImplementedException();
        }
    }
}