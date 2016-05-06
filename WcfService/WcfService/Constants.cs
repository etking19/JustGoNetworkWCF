using System;
using System.Web.Script.Serialization;

namespace WcfService
{
    public class Constants
    {
        public static string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JustGoNetworkDB"].ConnectionString;
        public static JavaScriptSerializer sJavaScriptSerializer = new JavaScriptSerializer();

        public enum ERolePermission
        {
            MasterAdmins         = 0xFFFFFFF,
            LorryPartners       = 0x0007840,
            Drivers             = 0x0006000,
            CorporatePartners   = 0x0001000,
            Users               = 0x0004000
        }

        public enum EPermission
        {
            CountryManagement           = 0x0000001,
            StateManagement             = 0x0000002,
            FleetTypeManagement         = 0x0000004,
            PickUpErrorManagement       = 0x0000008,
            DeliveryErrorManagement     = 0x0000010,
            UserManagement              = 0x0000020,
            CompanyManagement           = 0x0000040,
            MasterAdminManagement       = 0x0000080,
            LorryPartnerManagement      = 0x0000100,
            Driversmanagement           = 0x0000200,
            CorporatePartnerManagement  = 0x0000400,
            FleetManagement             = 0x0000800,
            JobsManagement              = 0x0001000,
            JobsDispatchManagement      = 0x0002000,
            TrackingManagement          = 0x0004000
        }

        public enum EJobDispatchStatus
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
            public ERolePermission Permission;
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
            public string Remarks;
            public Company Company;
            public FleetType FleetType;
            public string RoadTaxExpiry;
            public string ServiceDueDate;
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
            public long Id;
            public int CompanyId;
            public Company Company;
            public bool Enabled;
            public string CustomerName;
            public string CustomerContact;

            public AddressInfo PickUpInfo;
            public AddressInfo DeliveryInfo;

            public string DeliveryDateTime;
            public float Amount;
            public bool CashOnDelivery;
            public int WorkerAssistance;
            public string Remarks;
        }

        public class JobDispatch
        {
            public long Id;

            public long JobId;
            public Job Job;

            public int CompanyId;
            public int DriverId;
            public DriverAdmin Driver;
            public float Rating;

            public EJobDispatchStatus JobStatus;

            public PickUpError PickUpError;
            public DeliveryError DeliveryError;
            public string StatusRemarks;
        }












    }
}