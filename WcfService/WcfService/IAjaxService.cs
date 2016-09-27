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

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/test", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response TestPut();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*", RequestFormat = WebMessageFormat.Xml)]
        void GetOptions();

        // states
        /// <summary>
        /// countryId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/state?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response StateGet();

        // countries
        /// <summary>
        /// countryId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/country?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CountryGet();

        // permission
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/permission", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PermissionGet();

        // activities
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/activity", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response ActivityGet();

        // roles
        /// <summary>
        /// roleId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/role/details?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response RoleGet();

        /// <summary>
        /// fleetTypeId (optional)
        /// </summary>
        /// <returns></returns>
        // fleet type
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/type?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetTypeGet();


        // pick up error
        /// <summary>
        /// pickupErrId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/pickupErr?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PickupErrGet();

        // deliver error
        /// <summary>
        /// deliverErrId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/deliverErr?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response DeliveryErrGet();

        // job status
        /// <summary>
        /// jobStatusId
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/jobStatus/type?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobStatusTypeGet();

        // job type
        /// <summary>
        /// jobTypeId (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/jobType?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobTypeGet();


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

        /// <summary>
        /// userId OR username OR companyId
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/user/profile?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response UserGetProfile();

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
        /// <summary>
        /// companyId (optional)
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/company/profile?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyProfileGet();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/company/profile", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyProfileAdd(Model.Company company);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/company/profile/{companyId}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyProfileUpdate(string companyId, Model.Company company);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/company/profile/{companyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response CompanyProfileDelete(string companyId);




        // fleet
        /// <summary>
        /// fleetId OR companyId (optional)
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fleet/lorry?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetLorryGet();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fleet/lorry", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetLorryAdd(Model.Fleet fleet);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/fleet/lorry/{fleetId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetLorryUpdate(string fleetId, Model.Fleet fleet);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/fleet/lorry/{fleetId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response FleetLorryDelete(string fleetId);



        // job details
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobAdd(Model.JobDetails jobDetails);

        /// <summary>
        /// jobId OR uniqueId OR ownerId (optional)
        /// limit
        /// skip
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job?", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobGet();

        /// <summary>
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/open?", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobOpenGet();

        /// <summary>
        /// uniqueId OR jobId
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/status?", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobStatusGet();

        /// <summary>
        /// uniqueId OR jobId
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/job/details/{jobId}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDetailsDelete(string jobId);

        /// <summary>
        /// Currently only support update general details, update address was not implemented yet
        /// </summary>
        /// <param name="jobDetails"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/job/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDetailsUpdate(string jobId, Model.JobDetails jobDetails);



        // job address
        /// <summary>
        /// userId
        /// fromAdd
        /// toAdd
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/address?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobAddressGet();








        // job delivery
        /// <summary>
        /// jobid OR companyId OR driverId OR statusId OR uniqueId
        /// limit (optional)
        /// skip (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/job/delivery?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryGet();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job/delivery", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryAdd(string jobId, string companyId, string driverId);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryUpdate(string jobId, string companyId, string driverId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryDelete(string jobId);




        // End User

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job/rating", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobSetRating(string uniqueId, float rating);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/delivery/postcode", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response DeliveryPostcodeValidation(string deliverFrom, string deliverTo);

        /// <summary>
        /// distance
        /// lorryType
        /// fromBuildingType (optional)
        /// toBuildingType (optional)
        /// labor (optional)
        /// assembleBed (optional)
        /// assemblyDining (optional)
        /// assemblyWardrobe (optional)
        /// assemblyTable (optional)
        /// promoCode (optional)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/delivery/price?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PriceGenerate();

        /// <summary>
        /// lorryType
        /// fromBuildingType
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/disposal/price?", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PriceGenerateDisposal();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/voucher", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response ValidateVoucher(string promoCode);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/payment", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PaymentMake(string uniqueId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/payment/callback", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response PaymentCallback();

        // Driver

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/job/delivery/{jobId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Model.Response JobDeliveryStatusUpdate(string jobId, string statusId, string pickupErrId, string deliverErrId);

    }
}
