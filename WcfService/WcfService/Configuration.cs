using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Configuration
    {
        public static readonly int TOKEN_VALID_HOURS = 10;
        public static readonly string CONNECTION_STRING = 
            System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
        public static readonly string SUPER_ADMIN_USERID = 
            System.Configuration.ConfigurationManager.ConnectionStrings["SuperAdminUserId"].ConnectionString;

        public static readonly int JOB_STATUS_PAID = 3;
    }
}