using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WcfService
{
    public class Constants
    {
        public static string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
        public static JavaScriptSerializer sJavaScriptSerializer = new JavaScriptSerializer();

        // enums
        public enum OrderStatus
        {
            WaitingToProcess = 0,
            Processing = 1,
            Delivering = 2,
            DeliveredFailed = 10,
            Delivered = 99,
        }

        public enum Gender
        {
            Male = 0,
            Female = 1,
        }

        public enum PaymentMode
        {
            CashOnDelivery = 0,
            OnlineTransaction = 1,
            Paid = 2
        }

        public enum AdminPermissions
        {
            // master admins
            AdminManagement =   0x00000001,
            GlobalTracking =    0x00000002,

            // company admins
            AssetManagement =   0x00010000,
            DriverManagement =  0x00020000,
            JobManagement =     0x00040000,
            AssetTracking =     0x00080000,
        }



        // stuctures
        public class Result
        {
            public bool Success;
            public int ErrorCode;
            public string ErrorMessage;
            public object Payload;
        }

        public class AdminInfo
        {
            public int AdminId;
            public string Username;
            public string Token;
            public int Permission;

        }

        public class Asset
        {
            public string Name;
            public string RegNum;
            public string Capacity;
            public string RoadTaxExpiry;
        }

    }
}