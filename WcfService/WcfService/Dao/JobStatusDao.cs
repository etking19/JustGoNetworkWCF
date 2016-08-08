using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class JobStatusDao
    {
        public string Add(Model.JobStatus payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.JobStatus> Get()
        {
            List<Model.JobStatus> deliveryErrList = new List<Model.JobStatus>();
            deliveryErrList.Add(new Model.JobStatus() { jobStatusId = "1", name = "picked up" });
            deliveryErrList.Add(new Model.JobStatus() { jobStatusId = "2", name = "delivered" });

            return deliveryErrList;
        }

        public Model.JobStatus Get(string id)
        {
            return new Model.JobStatus() { jobStatusId = "1", name = "error 1" };
        }

        public bool Update(string id, Model.JobStatus payload)
        {
            return true;
        }
    }
}