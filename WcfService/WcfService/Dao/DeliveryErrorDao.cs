using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class DeliveryErrorDao
    {
        public string Add(Model.DeliveryError payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.DeliveryError> Get()
        {
            List<Model.DeliveryError> deliveryErrList = new List<Model.DeliveryError>();
            deliveryErrList.Add(new Model.DeliveryError() { deliveryErrorId = "1", name = "error 1" });
            deliveryErrList.Add(new Model.DeliveryError() { deliveryErrorId = "2", name = "error 2" });

            return deliveryErrList;
        }

        public Model.DeliveryError Get(string id)
        {
            return new Model.DeliveryError() { deliveryErrorId = "1", name = "error 1" };
        }

        public bool Update(string id, Model.DeliveryError payload)
        {
            return true;
        }
    }
}