using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class PickUpErrorDao
    {
        public string Add(Model.PickupError payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.PickupError> Get()
        {
            List<Model.PickupError> pickupErrList = new List<Model.PickupError>();
            pickupErrList.Add(new Model.PickupError() { pickupErrorId = "1", name = "error 1" });
            pickupErrList.Add(new Model.PickupError() { pickupErrorId = "2", name = "error 2" });

            return pickupErrList;
        }

        public Model.PickupError Get(string id)
        {
            return new Model.PickupError() { pickupErrorId = "1", name = "error 1" };
        }

        public bool Update(string id, Model.PickupError payload)
        {
            return true;
        }
    }
}