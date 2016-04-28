using System;
using System.Web.Script.Serialization;

namespace WcfService
{
    public class Constants
    {
        public static string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
        public static JavaScriptSerializer sJavaScriptSerializer = new JavaScriptSerializer();

        public enum EPermission
        {
            NoRole = 0x0000,
            Users = 0x0001,
            LorryPartners = 0x0002,
            Drivers = 0x0004,
            CorporatePartners = 0x0008,
            MasterAdmins = 0xFFFF,
        }

        public enum EJobStatus
        {
            Open = 0,
            OrderReceived = 1,
            PickedUp = 2,
            Delivered = 3,
            DeliverError = 99,
        }

        // stuctures
        public class Result
        {
            public bool Success;
            public int ErrorCode;
            public string ErrorMessage;
            public object Payload;
        }

        /*
        Master Admin
        */
        public class Country
        {
            public int Id;
            public string Name;
        }

        public class State
        {
            public int Id;
            public string Name;
            public int CountryId;
        }

        public class FleetType
        {
            public int Id;
            public string Name;
            public int Capacity;
            public string Type;
        }

        public class Company
        {
            public int Id;
            public string Name;
            public string Address1;
            public string Address2;
            public string Postcode;
            public State State;
            public Country Country;
            public string SSM;
            public bool Enabled;
        }

        public class Role
        {
            public string Name;
            public EPermission Permission;
        }

        public class User
        {
            public int Id;
            public string Username;
            public string Password;
            public string DisplayName;
            public string Token;
            public string TokenValidity;
            public bool Enabled;
            public string LastLogin;
            public string PushIdentifier;
            public EPermission[] Permissions;
        }

        public class MasterAdmin
        {
            public int Id;
            public int UserId;
            public User UserObj;
        }

        public class LorryPartnerAdmin
        {
            public int Id;
            public int UserId;
            public int CompanyId;
            public User UserObj;
            public Company CompanyObj;
        }

        public class DriverAdmin
        {
            public int Id;
            public int UserId;
            public int CompanyId;
            public User UserObj;
            public Company CompanyObj;
            public float Rating;
            public string IdentityCard;
        }

        public class CorporatePartnerAdmin
        {
            public int Id;
            public int UserId;
            public int CompanyId;
            public User UserObj;
            public Company CompanyObj;
        }

        public class PickUpError
        {
            public int Id;
            public string Name;
        }

        public class DeliveryError
        {
            public int Id;
            public string Name;
        }


        /*
        Asset
        */
        public class Fleet
        {
            public string Identifier;
            public Company Company;
            public FleetType FleetType;
            public DateTime RoadTaxExpiry;
            public DateTime ServiceDueDate;
        }

        public class AddressInfo
        {
            public string Name;
            public string Contact;

            public string Address1;
            public string Address2;
            public string Postcode;
            public State State;
            public Country Country;

            public float Longitude;
            public float Latitude;
        }

        public class Job
        {
            public int Id;
            public bool Enabled;
            public string CustomerName;
            public string CustomerContact;

            public AddressInfo PickUpInfo;
            public AddressInfo DeliveryInfo;

            public DateTime DeliveryDateTime;
            public float Amount;
            public bool CashOnDelivery;
            public int WorkerAssistance;
            public string Remarks;

            public DriverAdmin Driver;
            public float Rating;

            public EJobStatus JobStatus;

            public PickUpError PickUpError;
            public DeliveryError DeliveryError;
            public string StatusRemarks;
        }











    }
}