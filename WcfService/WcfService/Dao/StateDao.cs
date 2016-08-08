using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class StateDao
    {
        public string Add(Model.State payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.State> Get()
        {
            List<Model.State> stateList = new List<Model.State>();
            stateList.Add(new Model.State() { stateId = "1", name = "Selangor", countryId = "1" });
            stateList.Add(new Model.State() { stateId = "2", name = "Wilayah", countryId = "1" });

            return stateList;
        }

        public Model.State Get(string id)
        {
            return new Model.State() { stateId = "1", name = "Selangor", countryId = "1" };
        }

        public bool Update(string id, Model.State payload)
        {
            return true;
        }
    }
}