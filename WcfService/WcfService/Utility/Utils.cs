using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Utility
{
    public class Utils
    {
        public static Response SetResponse(Response response, bool success, int errorCode)
        {
            response.success = success;
            response.errorCode = errorCode;
            response.errorMessage = Constant.ErrorMsg.GetInstance().GetErrorMsg(errorCode);

            return response;
        }
    }
}