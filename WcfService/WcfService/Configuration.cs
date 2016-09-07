using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Configuration
    {
        public static int TOKEN_VALID_HOURS = 10;
        public static int JOB_STATUS_PAID = 3;


        public string CONNECTION_STRING
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
            }
        } 
    }
}