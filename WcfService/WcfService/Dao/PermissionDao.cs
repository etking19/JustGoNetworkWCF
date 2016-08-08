using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class PermissionDao
    {
        public string Add(Model.Permission payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.Permission> Get()
        {
            List<Model.Permission> permissionList = new List<Model.Permission>();
            permissionList.Add(new Model.Permission() { permissionId = "1", name = "Add" });
            permissionList.Add(new Model.Permission() { permissionId = "2", name = "Edit" });

            return permissionList;
        }

        public Model.Permission Get(string id)
        {
            return new Model.Permission() { permissionId = "1", name = "Add" };
        }

        public bool Update(string id, Model.Permission payload)
        {
            return true;
        }
    }
}