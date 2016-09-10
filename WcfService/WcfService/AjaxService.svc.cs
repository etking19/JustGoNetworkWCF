using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using WcfService.Constant;
using WcfService.Controller;
using WcfService.Model;
using WcfService.Utility;

namespace WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService : IAjaxService
    {

        private static CommonController commonController = new CommonController();
        private static UserController userController = new UserController();
        private static CompanyController companyController = new CompanyController();
        private static RoleController roleController = new RoleController();
        private static FleetController fleetController = new FleetController();
        private static JobController jobController = new JobController();

        public void GetOptions()
        {
            // empty function
            DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, "GetOptions");
        }

        public Response CompanyAddProfile(Company company)
        {
            return companyController.AddCompany(company);
        }

        public Response CompanyDeleteProfile(string companyId)
        {
            return companyController.DeleteCompany(companyId);
        }

        public Response CompanyGetAllProfile()
        {
            return companyController.GetCompanies();
        }

        public Response CompanyUpdateProfile(string companyId, Company company)
        {
            return companyController.UpdateCompany(companyId, company);
        }

        public Response CountryGet(string countryId)
        {
            return commonController.GetCountry(countryId);
        }

        public Response CountryGetAll()
        {
            return commonController.GetCountry();
        }

        public Response FleetAdd(Fleet fleet)
        {
            return fleetController.AddFleet(fleet);
        }

        public Response FleetDelete(string fleetId)
        {
            return fleetController.DeleteFleet(fleetId);
        }

        public Response FleetGet(string fleetId)
        {
            return fleetController.GetFleet(fleetId);
        }

        public Response FleetGetByCompany(string companyId)
        {
            return fleetController.GetFleets();
        }

        public Response FleetTypeGet()
        {
            return commonController.GetFleetType();
        }

        public Response FleetUpdate(string fleetId, Fleet fleet)
        {
            return fleetController.UpdateFleet(fleetId, fleet);
        }

        public Response PermissionGet()
        {
            return commonController.GetPermission();
        }

        public Response RoleGet()
        {
            return roleController.GetRoles();
        }

        public Response StateGet(string stateId)
        {
            return commonController.GetState(stateId);
        }

        public Response StateGetAll()
        {
            return commonController.GetState();
        }

        public Model.Response Test()
        {
            return commonController.Test();
        }

        public Model.Response TestPut()
        {
            return commonController.Test();
        }

        public Response TokenCheck(string userId, string token)
        {
            Model.Response response = new Response()
            {
                errorCode = ErrorCode.ESuccess,
                errorMessage = "Token valid",
                success = true
            };

            return response;
        }

        public Response UserAddProfile(User user)
        {
            return userController.AddUser(user);
        }

        public Response UserDeleteProfile(string userId)
        {
            return userController.DeleteUser(userId);
        }

        public Response UserForgotPassword(string userId)
        {
            return userController.ForgotPassword(userId);
        }

        public Model.Response UserGetProfile()
        {
            return userController.GetUser();
        }

        //public Response UserGetAllProfile(string number, string skip)
        //{
        //    return userController.GetUserList(number, skip);
        //}

        //public Response UserGetProfile(string userId)
        //{
        //    return userController.GetUser(userId);
        //}

        public Response UserLogin(string username, string password)
        {
            return userController.Login(username, password);
        }

        public Response UserUpdateDevice(string userId, string identifier)
        {
            return userController.UpdateDeviceIdentifier(userId, identifier);
        }

        public Response UserUpdatePassword(string userId, string oldPw, string newPw)
        {
            return userController.UpdatePassword(userId, oldPw, newPw);
        }

        public Response UserUpdateProfile(string userId, User user)
        {
            return userController.EditUser(userId, user);
        }

        public Response PickupErrGetAll()
        {
            return commonController.GetPickupError();
        }

        public Response PickupErrGet(string pickupErrId)
        {
            return commonController.GetPickupError(pickupErrId);
        }

        public Response DeliveryErrGetAll()
        {
            return commonController.GetDeliveryError();
        }

        public Response DeliveryErrGet(string deliveryErrId)
        {
            return commonController.GetDeliveryError(deliveryErrId);
        }


        public Response JobAdd(Model.JobDetails jobDetails)
        {
            return jobController.AddJob(jobDetails, jobDetails.addressFrom.ToArray(), jobDetails.addressTo.ToArray());
        }

        public Response JobGet()
        {
            return jobController.GetJob();
        }

        public Response JobDetailsUpdate(string jobId, Model.JobDetails jobDetails)
        {
            return jobController.UpdateJob(jobId, jobDetails);
        }

        public Response JobDetailsDelete(string jobId)
        {
            return jobController.DeleteJob(jobId);
        }

        public Response JobAddressGet()
        {
            return jobController.GetAddresses();
        }

        public Response JobDeliveryGetAll(string limit, string skip)
        {
            return jobController.GetJobDelivery(limit, skip);
        }

        public Response JobDeliveryGetById(string jobId)
        {
            return jobController.GetJobDelivery(jobId);
        }

        public Response JobDeliveryGetByCompany(string companyId, string limit, string skip, string status)
        {
            return jobController.GetJobDeliveryByCompany(companyId, limit, skip, status);
        }

        public Response JobDeliveryGetByDriver(string userId, string limit, string skip, string status)
        {
            return jobController.GetJobDeliveryByDriver(userId, limit, skip, status);
        }

        public Response JobDeliveryGetByStatus(string statusId, string limit, string skip)
        {
            return jobController.GetJobDeliveryByStatus(statusId, limit, skip);
        }

        public Response JobDeliveryAdd(string jobId, string companyId, string driverId)
        {
            return jobController.AddJobDelivery(jobId, companyId, driverId);
        }

        public Response JobDeliveryUpdate(string jobId, string companyId, string driverId)
        {
            return jobController.UpdateJobDelivery(jobId, companyId, driverId);
        }

        public Response JobDeliveryDelete(string jobId)
        {
            return jobController.DeleteJobDelivery(jobId);
        }

        public Response JobStatusGet()
        {
            return jobController.GetJobStatus();
        }

        public Response JobOpenGet()
        {
            return jobController.GetOpenJobs();
        }


        public Response JobDeliveryStatusGet(string uniqueId)
        {
            return jobController.GetJobDeliveryByUniqueId(uniqueId);
        }

        public Response JobDeliveryStatusGetByCompany(string companyId, string limit, string skip, string status)
        {
            return jobController.GetJobDeliveryByCompany(companyId, limit, skip, status);
        }

        public Response JobDeliveryStatusGetByDriver(string driverId, string limit, string skip, string status)
        {
            return jobController.GetJobDeliveryByDriver(driverId, limit, skip, status);
        }

        public Response JobDeliveryStatusGetByJobId(string jobId)
        {
            return jobController.GetJobDelivery(jobId);
        }

        public Response JobDeliveryStatusSetRating(string uniqueId, float rating)
        {
            return jobController.SetRating(uniqueId, rating);
        }

        public Response JobDeliveryStatusUpdate(string jobId, string statusId)
        {
            return jobController.UpdateDeliveryStatus(jobId, statusId);
        }

        public Response JobDeliveryGetStatus()
        {
            return jobController.GetJobStatus();
        }
    }
}
