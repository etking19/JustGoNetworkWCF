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
        // --------- coutries ------
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

        // --------- states ------
        [OperationContract]
        string GetStates(int countryId);

        [OperationContract]
        string AddState(int countryId, string name);

        [OperationContract]
        string EditState(int stateId, int countryId, string name);

        [OperationContract]
        string DeleteState(int stateId);

        [OperationContract]
        string DeleteStates(int[] stateIds);

        // --------- fleet types ------
        [OperationContract]
        string GetFleetTypes();

        [OperationContract]
        string AddFleetType(string name, int capacity, string design);

        [OperationContract]
        string EditFleetType(int fleetId, string name, int capacity, string design);

        [OperationContract]
        string DeleteFleetType(int fleetId);

        [OperationContract]
        string DeleteFleetTypes(int[] fleetIds);

        // --------- companies ------
        [OperationContract]
        string GetCompanies();

        [OperationContract]
        string EnableCompany(int companyId, bool enabled);

        [OperationContract]
        string AddCompany(string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm);

        [OperationContract]
        string EditCompany(int companyId, string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm);

        [OperationContract]
        string RemoveCompany(int companyId);

        [OperationContract]
        string RemoveCompanies(int[] companyIds);

        // --------- users ------
        [OperationContract]
        string GetRoles();

        [OperationContract]
        string GetUsers();

        [OperationContract]
        string EnableUser(int userId, bool enabled);

        [OperationContract]
        string AddUser(string username, string displayName, int[] permissions, int companyId);

        [OperationContract]
        string EditUser(int userId, string displayName, int[] permissions, int companyId);

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
