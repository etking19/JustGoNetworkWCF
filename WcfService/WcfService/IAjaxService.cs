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
        [OperationContract]
        string GetFleets();

        [OperationContract]
        string GetFleetsByCompanyId(int companyId);

        [OperationContract]
        string AddFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);

        [OperationContract]
        string EditFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);

        [OperationContract]
        string DeleteFleet(string identifier);

        [OperationContract]
        string DeleteFleets(string[] identifier);

        /*
        User/Driver management functions
        */
        [OperationContract]
        string GetDrivers();

        [OperationContract]
        string GetDriversByCompanyId(int companyId);

        [OperationContract]
        string RemoveDriver(int userId);

        [OperationContract]
        string RemoveDrivers(int[] userIds);

        /*
        Job management functions
        */
        [OperationContract]
        string AddJob(int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        [OperationContract]
        string EditJob(int id, int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        [OperationContract]
        string RemoveJob(int id);

        [OperationContract]
        string GetOpenJobs();

        /*
        Job dispatch management functions
        */
        [OperationContract]
        string GetJobsDispatch(int status);

        [OperationContract]
        string AddJobDispatch(int jobId, int companyId);

        [OperationContract]
        string EditJobDispatch(int id, int jobId, int companyId);

        [OperationContract]
        string DeleteJobDispatch(int id);

        [OperationContract]
        string DeleteJobsDispatch(int[] ids);

        [OperationContract]
        string AssignJobDispatch(int id, string driverUsername);


        /*
        GPS tracking functions
        */
        string GetLastTrackingPos(string driverUsername);
        string GetLastTrackingPosbyList(string[] driverUsername);

        /*
        Master admin functions
        */
        // --------- countries ------
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
        string AddCompany(string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm);

        [OperationContract]
        string EditCompany(int companyId, string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm);

        [OperationContract]
        string RemoveCompany(int companyId);

        [OperationContract]
        string RemoveCompanies(int[] companyIds);


        // --------- user management ------
        [OperationContract]
        string EnableUser(int userId, bool enabled);

        [OperationContract]
        string AddUser(string username, string displayName, int[] permissions, int companyId, string identityCard);

        [OperationContract]
        string EditUser(string username, string displayName, int[] permissions, int companyId, string identityCard);

        string GetPermissions();

        /*
        Client app functions
        */
        string GetPickUpErrorsEle();
        string GetDeliveryErrorsEle();
        
        string UpdateJobStatus(int jobId, Constants.EJobDispatchStatus jobStatus);
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
            string pickupPostcode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPostcode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        string AssignJob(int jobId, int deliveryCompanyId);
    }
}
