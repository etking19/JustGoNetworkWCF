using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Constant
{
    public class ErrorMsg
    {
        private static Dictionary<int, string> errorMsgDic; 

        static ErrorMsg()
        {
            // initialize all error value here
            errorMsgDic = new Dictionary<int, string>();

            errorMsgDic.Add(ErrorCode.ESuccess, "Request success.");

            errorMsgDic.Add(ErrorCode.EGeneralError, "General error. Please see administrator to resolve issue.");
            errorMsgDic.Add(ErrorCode.EUnknownError, "Internal error. Please try again later.");

            errorMsgDic.Add(ErrorCode.ECredentialError, "Username or Password incorrect.");
            errorMsgDic.Add(ErrorCode.ETokenError, "Token was invalid");
            errorMsgDic.Add(ErrorCode.ETokenExpired, "Token expired. Please login again.");
            errorMsgDic.Add(ErrorCode.EMultipleLogin, "You have been logout. Someone login from another device.");
            errorMsgDic.Add(ErrorCode.EAccountDisabled, "You account been disabled. Please see administrator to activate back.");
            errorMsgDic.Add(ErrorCode.EAccountDeleted, "You account been deleted. Please see administrator for more info.");
        }

        public static string GetErrorMsg(int errorCode)
        {
            string errorMsg = "";
            errorMsgDic.TryGetValue(errorCode, out errorMsg);

            return errorMsg;
        }
    }
}