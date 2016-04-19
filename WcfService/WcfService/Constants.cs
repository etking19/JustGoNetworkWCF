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
            General = 0x0000,

            FleetManagement = 0x0001,
            DriverManagement = 0x0002,
            JobPartnerManagement = 0x0004,
            PartnerTrackingManagement = 0x0008,

            CountryManagement = 0x0010,
            StateManagement = 0x0020,
            FleetTypeManagement = 0x0040,
            CompanyManagement = 0x0080,
            RoleManagement = 0x0100,
            AdminManagement = 0x0200,

            Driver = 0x1000,
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
            public string Design;
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
            public int Id;
            public string Name;
            public int Permission;
        }

        public class Admin
        {
            public string Username;
            public string DisplayName;
            public Company Company;
            public Role Role;
            public string Token;
            public string TokenValidity;
            public bool Enabled;
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

        public class Driver
        {
            public string Username;
            public string DisplayName;
            public string MyKad;
            public float Rating;
            public int JobCompleted;
            public Company Company;
            public Fleet Fleet;
            public bool Enabled;
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

            public Driver Driver;
            public float Rating;

            public EJobStatus JobStatus;

            public PickUpError PickUpError;
            public DeliveryError DeliveryError;
            public string StatusRemarks;
        }











    }
}