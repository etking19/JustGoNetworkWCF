using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class FleetTypeDao
    {
        public string Add(Model.FleetType payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.FleetType> Get()
        {
            List<Model.FleetType> fleetTypeList = new List<Model.FleetType>();
            fleetTypeList.Add(new Model.FleetType() {fleetTypeId="1", capacity=1, name = "1 tonne lorry", type = "Canvas" });
            fleetTypeList.Add(new Model.FleetType() { fleetTypeId = "2", capacity = 3, name = "3 tonne lorry", type = "Canvas" });

            return fleetTypeList;
        }

        public Model.FleetType Get(string id)
        {
            return new Model.FleetType() { fleetTypeId = "1", capacity = 1, name = "1 tonne lorry", type = "Canvas" };
        }

        public bool Update(string id, Model.FleetType payload)
        {
            return true;
        }
    }
}