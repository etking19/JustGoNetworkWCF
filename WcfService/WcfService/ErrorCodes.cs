using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class ErrorCodes
    {
        public static int ESuccess = 0;
        public static int EAuthenticationFormatErr = 1;

        public static int EGeneralError = 100;
        public static int EGeneralDatabaseErr = 101;

        public static int ELoginExpired = 200;
        public static int ELoginCredential = 201;
        public static int ELoginPermissionDenied = 202;
        public static int ELoginAccSuspended = 203;
        public static int ELoginOldPWNotMatched = 204;

    }
}