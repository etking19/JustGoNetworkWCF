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

        public enum BuildingType
        {
            Landed = 1,
            HighRise_nolift = 2,
            HighRise_lift = 3
        };

        public enum VoucherType
        {
            Percentage = 1,
            Value = 2
        };

        public enum LorryType
        {
            Lorry_1tonne = 1,
            Lorry_3tonne = 3,
            Lorry_5tonne = 5,
            Lorry_10tonne = 10
        }
    }
}