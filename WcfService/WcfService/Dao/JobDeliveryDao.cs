using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class JobDeliveryDao
    {
        public string Add(Model.JobDelivery payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.JobDelivery> Get()
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

        public bool Update(string id, Model.JobDelivery payload)
        {
            return true;
        }
    }
}