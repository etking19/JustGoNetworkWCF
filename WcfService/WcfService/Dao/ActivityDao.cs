using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class ActivityDao
    {
        public string Add(Model.Activity payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.Activity> Get()
        {
            List<Model.Activity> activitiesList = new List<Model.Activity>();
            activitiesList.Add(new Model.Activity() { activityId = "1", name = "Job" });
            activitiesList.Add(new Model.Activity() { activityId = "2", name = "Tracking" });

            return activitiesList;
        }

        public Model.Activity Get(string id)
        {
            return new Model.Activity() { activityId = "1", name = "Add" };
        }

        public bool Update(string id, Model.Activity payload)
        {
            return true;
        }
    }
}