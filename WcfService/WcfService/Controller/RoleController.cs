using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class RoleController : BaseController
    {
        public Response AddRole(Role role)
        {
            response.payload = roleDao.AddRole(role);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response EditRole(string roleId, Role role)
        {
            response.payload = roleDao.UpdateRole(roleId, role);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetRole(string roleId)
        {
            response.payload = roleDao.GetRoleById(roleId);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetRoles()
        {
            response.payload = roleDao.GetRoles();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteRole(string roleId)
        {
            response.payload = roleDao.DeleteRole(roleId);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}