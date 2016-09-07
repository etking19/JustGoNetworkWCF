using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfService
{
    [ServiceContract]
    public interface IAjaxService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/test", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response Test();



        // user

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/user/login", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserLogin(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/user/token/{userId}/{token}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response TokenCheck(string userId, string token);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/user/password/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserUpdatePassword(string userId, string oldPw, string newPw);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/user/password/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserForgotPassword(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/user/profile/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserGetProfile(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/user/profile/{number}/{skip}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserGetAllProfile(string number, string skip);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/user/profile/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserDeleteProfile(string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/user/profile", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserAddProfile(Model.User user);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/user/profile/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserUpdateProfile(string userId, Model.User user);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/user/device/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserUpdateDevice(string userId, string identifier);



        // company

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/company/profile/{companyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyGetProfile(string companyId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/company/profile/{number}/{skip}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyGetAllProfile(string number, string skip);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/company/profile", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyAddProfile(Model.Company company);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/company/profile/{companyId}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyUpdateProfile(string companyId, Model.Company company);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/company/profile/{companyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyDeleteProfile(string companyId);


        // roles

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/role/details", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleGet();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/role/details/{roleId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleGetAll(string roleId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/role/details", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleAdd(Model.Role role);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/role/details/{roleId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleDelete(string roleId);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/role/details/{roleId}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleUpdate(string roleId, Model.Role role);


        // fleet type

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/type", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetTypeGet();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/type/{fleetTypeId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetTypeGetAll(string fleetTypeId);


        // permission

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/permission", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PermissionGet();


        // fleet

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/lorry/{fleetId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetGet(string fleetId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/lorry/company/{companyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetGetByCompany(string companyId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fleet/lorry", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetAdd(Model.Fleet fleet);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/fleet/lorry/{fleetId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetUpdate(string fleetId, Model.Fleet fleet);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/fleet/lorry/{fleetId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetDelete(string fleetId);


        // country and states

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/country/info", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CountryGetAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/country/info/{countryId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CountryGet(string countryId);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/state/info", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response StateGetAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/state/info/{stateId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response StateGet(string stateId);


        // pick up error

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/pickupErr/info", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PickupErrGetAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/pickupErr/info/{pickupErrId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PickupErrGet(string pickupErrId);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/deliverErr/info", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response DeliveryErrGetAll();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/deliverErr/info/{deliveryErrId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response DeliveryErrGet(string deliveryErrId);



        // job details
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobAdd(Model.JobDetails jobDetails);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobGet();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/open?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobOpenGet();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/status?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobStatusGet();

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/job/details/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDetailsDelete(string jobId);


        

        // job address

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/address?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobAddressGet();

        // job delivery

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery?limit={limit}&skip={skip}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetAll(string limit, string skip);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetById(string jobId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery/company/{companyId}?limit={limit}&skip={skip}&status={status}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetByCompany(string companyId, string limit, string skip, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery/driver/{userId}?limit={limit}&skip={skip}&status={status}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetByDriver(string userId, string limit, string skip, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery/status/{statusId}?limit={limit}&skip={skip}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetByStatus(string statusId, string limit, string skip);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job/delivery", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryAdd(string jobId, string companyId, string driverId);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryUpdate(string jobId, string companyId, string driverId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryDelete(string jobId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/details?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGetStatus();


        // Job Delivery Status

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/status/{uniqueId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusGet(string uniqueId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/status/company/{companyId}?limit={limit}&skip={skip}&status={status}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusGetByCompany(string companyId, string limit, string skip, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/status/driver/{driverId}?limit={limit}&skip={skip}&status={status}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusGetByDriver(string driverId, string limit, string skip, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/status/job/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusGetByJobId(string jobId);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/status/{uniqueId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusSetRating(string uniqueId, float rating);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/status/job/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusUpdate(string jobId, string statusId);





































        ///*
        //General functions
        //*/
        //[OperationContract]
        //string Login(string username, string password);

        //[OperationContract]
        //string ChangePassword(string oldPassword, string newPassword);

        //[OperationContract]
        //string ForgotPassword(string username);

        ///*
        //Fleet management functions
        //*/
        //[OperationContract]
        //string GetFleets();

        //[OperationContract]
        //string GetFleetsByCompanyId(int companyId);

        //[OperationContract]
        //string AddFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);

        //[OperationContract]
        //string EditFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate);

        //[OperationContract]
        //string DeleteFleet(string identifier);

        //[OperationContract]
        //string DeleteFleets(string[] identifier);

        ///*
        //User/Driver management functions
        //*/
        //[OperationContract]
        //string GetDrivers();

        //[OperationContract]
        //string GetDriversByCompanyId(int companyId);

        //[OperationContract]
        //string RemoveDriver(int userId);

        //[OperationContract]
        //string RemoveDrivers(int[] userIds);

        ///*
        //Job management functions
        //*/
        //[OperationContract]
        //string AddJob(int companyId, string customerName, string customerContact,
        //    string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
        //    string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
        //    string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
        //    string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
        //    string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        //[OperationContract]
        //string EditJob(int id, int companyId, string customerName, string customerContact,
        //    string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
        //    string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
        //    string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
        //    string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
        //    string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        //[OperationContract]
        //string RemoveJob(int id);

        //[OperationContract]
        //string GetOpenJobs();

        ///*
        //Job dispatch management functions
        //*/
        //[OperationContract]
        //string GetJobsDispatch(int status);

        //[OperationContract]
        //string AddJobDispatch(int jobId, int companyId);

        //[OperationContract]
        //string EditJobDispatch(int id, int jobId, int companyId);

        //[OperationContract]
        //string DeleteJobDispatch(int id);

        //[OperationContract]
        //string DeleteJobsDispatch(int[] ids);

        //[OperationContract]
        //string AssignJobDispatch(int id, string driverUsername);


        ///*
        //GPS tracking functions
        //*/
        //string GetLastTrackingPos(string driverUsername);
        //string GetLastTrackingPosbyList(string[] driverUsername);

        ///*
        //Master admin functions
        //*/
        //// --------- countries ------
        //[OperationContract]
        //string GetCountries();

        //[OperationContract]
        //string AddCountry(string name);

        //[OperationContract]
        //string EditCountry(int countryId, string name);

        //[OperationContract]
        //string DeleteCountry(int countryId);

        //[OperationContract]
        //string DeleteCountries(int[] countryIds);

        //// --------- states ------
        //[OperationContract]
        //string GetStates(int countryId);

        //[OperationContract]
        //string AddState(int countryId, string name);

        //[OperationContract]
        //string EditState(int stateId, int countryId, string name);

        //[OperationContract]
        //string DeleteState(int stateId);

        //[OperationContract]
        //string DeleteStates(int[] stateIds);

        //// --------- fleet types ------
        //[OperationContract]
        //string GetFleetTypes();

        //[OperationContract]
        //string AddFleetType(string name, int capacity, string design);

        //[OperationContract]
        //string EditFleetType(int fleetId, string name, int capacity, string design);

        //[OperationContract]
        //string DeleteFleetType(int fleetId);

        //[OperationContract]
        //string DeleteFleetTypes(int[] fleetIds);

        //// --------- companies ------
        //[OperationContract]
        //string GetCompanies();

        //[OperationContract]
        //string EnableCompany(int companyId, bool enabled);

        //[OperationContract]
        //string AddCompany(string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm);

        //[OperationContract]
        //string EditCompany(int companyId, string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm);

        //[OperationContract]
        //string RemoveCompany(int companyId);

        //[OperationContract]
        //string RemoveCompanies(int[] companyIds);


        //// --------- user management ------
        //[OperationContract]
        //string EnableUser(int userId, bool enabled);

        //[OperationContract]
        //string AddUser(string username, string displayName, int[] permissions, int companyId, string identityCard);

        //[OperationContract]
        //string EditUser(string username, string displayName, int[] permissions, int companyId, string identityCard);

        //string GetPermissions();

        ///*
        //Client app functions
        //*/
        //string GetPickUpErrorsEle();
        //string GetDeliveryErrorsEle();

        //string UpdateJobStatus(int jobId, Constants.EJobDispatchStatus jobStatus);
        //string UpdateJobPickUpError(int jobId, Constants.PickUpError pickupError);
        //string UpdateJobDeliveryError(int jobId, Constants.DeliveryError deliveryError);
        //string UpdateJobStatusRemarks(int jobId, string remarks);
        //string UpdateDeliveryOrder(int jobId, string base64DO);
        //string AddRating(int jobId, float score);

        //string UpdateLocation(string username, float longitude, float latitude, float speed);

        ///*
        //For future customer / Master admin
        //*/
        //string AddJob(string customerName, string customerContact,
        //    string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
        //    string pickupPostcode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
        //    string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
        //    string deliverPostcode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
        //    string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks);

        //string AssignJob(int jobId, int deliveryCompanyId);
    }
}
