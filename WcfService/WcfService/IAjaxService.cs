using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IAjaxService
    {
        [OperationContract]
        string Test();

        /*
        General functions
        */
        [OperationContract]
        string Login(string username, string password);

        [OperationContract]
        string ChangePassword(string oldPassword, string newPassword);

        [OperationContract]
        string ForgotPassword(string username);

        /*
        Fleet management functions
        */
        string GetFleets();
        string AddFleet(string identifier, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);
        string EditFleet(string identifier, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);
        string DeleteFleet(string identifier);
        string DeleteFleets(string[] identifier);

        /*
        User/Driver management functions
        */
        string GetDrivers();
        string AddDriver(string username, string displayName, string mykad);
        string EditDriver(string username, string displayName, string mykad);
        string RemoveDriver(string username);
        string RemoveDrivers(string[] usernames);

        string ResetRating(string username);

        /*
        Job management functions
        */
        string GetJobsPartner(Constants.EJobStatus status);
        string AddJobPartner(string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2, 
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2, 
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);
        string EditJob(int jobId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);
        string DeleteJob(int jobId);
        string DeleteJobs(int[] jobIds);

        string AssignJobPartner(int jobId, string driverUsername);

        /*
        GPS tracking functions
        */
        string GetLastTrackingPos(string driverUsername);
        string GetLastTrackingPosbyList(string[] driverUsername);

        /*
        Master admin functions
        */
        [OperationContract]
        string GetCountries();

        [OperationContract]
        string AddCountry(string name);

        [OperationContract]
        string EditCountry(int countryId, string name);

        [OperationContract]
        string DeleteCountry(int countryId);

        [OperationContract]
        string DeleteCountries(int[] countryIds);

        string GetStates(int countryId);
        string AddState(int countryId, string name);
        string EditState(int stateId, int countryId, string name);
        string DeleteState(int stateId);
        string DeleteStates(int[] stateIds);

        string GetFleetTypes();
        string AddFleetType(string name, int capacity, string design);
        string EditFleetType(int fleetId, string name, int capacity, string design);
        string DeleteFleetType(int fleetId);
        string DeleteFleetTypes(int[] fleetIds);

        string GetCompanies();
        string AddCompany(string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm);
        string EditCompany(int companyId, string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm);
        string RemoveCompany(int companyId);
        string RemoveCompanies(int[] companyIds);

        string GetRoles();
        string GetPermissions();
        string AddRole(string name);
        string EditRole(int roleId, string name);
        string RemoveRole(int roleId);
        string RemoveRoles(int[] roleIds);

        string GetAdmins();
        string AddAdmin(string username, string displayName, int companyId, int roleId);
        string EditAdmin(string username, string displayName, int companyId, int roleId);
        string DeleteAdmin(string username);
        string DeleteAdmins(string[] usernames);

        /*
        Client app functions
        */
        string GetPickUpErrorsEle();
        string GetDeliveryErrorsEle();
        
        string UpdateJobStatus(int jobId, Constants.EJobStatus jobStatus);
        string UpdateJobPickUpError(int jobId, Constants.PickUpError pickupError);
        string UpdateJobDeliveryError(int jobId, Constants.DeliveryError deliveryError);
        string UpdateJobStatusRemarks(int jobId, string remarks);
        string UpdateDeliveryOrder(int jobId, string base64DO);
        string AddRating(int jobId, float score);

        string UpdateLocation(string username, float longitude, float latitude, float speed);

        /*
        For future customer / Master admin
        */
        string AddJob(string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        string AssignJob(int jobId, int deliveryCompanyId);
    }
}
