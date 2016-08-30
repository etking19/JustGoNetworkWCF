using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Configuration
    {
        public static int TOKEN_VALID_HOURS = 10;
        public static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
    }
}