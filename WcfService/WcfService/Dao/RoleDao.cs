using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Dao
{
    public class RoleDao : BaseDao
    {
        public string AddRole(Role role)
        {
            return "1";
        }

        public Role GetRoleById(string roleId)
        {
            return new Model.Role()
            {
                name = "MasterAdmin",
                roleId = "123"
            };
        }

        public List<Role> GetRoles()
        {
            List<Role> roleList = new List<Role>()
            {
                new Role()
                { name="MasterAdmin", roleId="123"},
                new Role() { name="Driver", roleId="333"}

            };

            return roleList;
        }

        public bool UpdateRole(string roleId, Role role)
        {
            return true;
        }

        public bool DeleteRole(string roleId)
        {
            return true;
        }
    }
}