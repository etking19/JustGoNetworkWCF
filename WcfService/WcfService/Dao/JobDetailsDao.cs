using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class JobDetailsDao
    {
        public string Add(Model.JobDetails payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.JobDetails> Get()
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();
            jobDetailsList.Add(new Model.JobDetails() { jobId = "1", amount = 10f, cashOnDelivery = true, enabled = true, deleted = false, remarks = "mno remarks", uniqueId = "1234-5678-9000", workerAsistance = 2, ownerUserId = "1", addressFromId = "1", addressToId = "2", createdBy = "1", modifiedBy = "1", creationDate = "20160808 000000", lastModifiedDate= "20160808 000000" });
            jobDetailsList.Add(new Model.JobDetails() { jobId = "2", amount = 10f, cashOnDelivery = true, enabled = true, deleted = false, remarks = "mno remarks", uniqueId = "1234-5678-9000", workerAsistance = 2, ownerUserId = "1", addressFromId = "1", addressToId = "2", createdBy = "1", modifiedBy = "1", creationDate = "20160808 000000", lastModifiedDate = "20160808 000000" });

            return jobDetailsList;
        }

        public Model.JobDetails Get(string id)
        {
            return new Model.JobDetails() { jobId = "1", amount = 10f, cashOnDelivery = true, enabled = true, deleted = false, remarks = "mno remarks", uniqueId = "1234-5678-9000", workerAsistance = 2, ownerUserId = "1", addressFromId = "1", addressToId = "2", createdBy = "1", modifiedBy = "1", creationDate = "20160808 000000", lastModifiedDate = "20160808 000000" };
        }

        public bool Update(string id, Model.JobDetails payload)
        {
            return true;
        }
    }
}