using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class AddressDao
    {
        public enum EType
        {
            From = 0,
            To = 1
        }

        public string Add(Model.Address payload, EType type)
        {
            return "1";
        }

        public bool Delete(string id, EType type)
        {
            return true;
        }

        public List<Model.Address> Get(string userId, string limit, string skip, EType type)
        {
            return null;
        }

        public Model.Address Get(string id, EType type)
        {
            return null;
        }

        public bool Update(string id, Model.Address payload, EType type)
        {
            return true;
        }
    }
}