using System;
using WcfService.Constant;
using WcfService.Controller;
using WcfService.Model;

namespace WcfService
{
    public class AjaxService : IAjaxService
    {
        private static CommonController commonController = new CommonController();
        private static UserController userController = new UserController();
        private static CompanyController companyController = new CompanyController();
        private static RoleController roleController = new RoleController();
        private static FleetController fleetController = new FleetController();
        private static JobController jobController = new JobController();

        public Response CompanyAddProfile(Company company)
        {
            return companyController.AddCompany(company);
        }

        public Response CompanyDeleteProfile(string companyId)
        {
            return companyController.DeleteCompany(companyId);
        }

        public Response CompanyGetAllProfile(string number, string skip)
        {
            return companyController.GetCompanies(number, skip);
        }

        public Response CompanyGetProfile(string companyId)
        {
            return companyController.GetCompany(companyId);
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

        public Response FleetTypeGetAll(string fleetTypeId)
        {
            return commonController.GetFleetType(fleetTypeId);
        }

        public Response FleetUpdate(string fleetId, Fleet fleet)
        {
            throw new NotImplementedException();
        }

        public Response FleetUpdateDriver(string fleetId, string userId)
        {
            throw new NotImplementedException();
        }

        public Response PermissionGet()
        {
            return commonController.GetPermission();
        }

        public Response RoleAdd(Role role)
        {
            return roleController.AddRole(role);
        }

        public Response RoleDelete(string roleId)
        {
            return roleController.DeleteRole(roleId);
        }

        public Response RoleGet()
        {
            return roleController.GetRoles();
        }

        public Response RoleGetAll(string roleId)
        {
            return roleController.GetRole(roleId);
        }

        public Response RoleUpdate(string roleId, Role role)
        {
            return roleController.EditRole(roleId, role);
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
            Model.Response response = new Response()
            {
                errorCode = ErrorCode.ESuccess,
                errorMessage = "test success",
                success = true
            };

            return response;
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

        public Response UserGetAllProfile(string number, string skip)
        {
            return userController.GetUserList(number, skip);
        }

        public Response UserGetProfile(string userId)
        {
            return userController.GetUser(userId);
        }

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

        public Response JobDetailsGetAll()
        {
            return jobController.GetJobDetails();
        }

        public Response JobDetailsGet(string jobId)
        {
            return jobController.GetJobDetails(jobId);
        }

        public Response JobDetailsGetByCompany(string companyId)
        {
            return jobController.GetJobDetailsByCompanyId(companyId);
        }

        public Response JobDetailsAdd(JobDetails jobDetails)
        {
            return jobController.AddJobDetails(jobDetails);
        }

        public Response JobDetailsUpdate(string jobId, JobDetails jobDetails)
        {
            return jobController.UpdateJobDetails(jobId, jobDetails);
        }

        public Response JobDetailsDelete(string jobId)
        {
            return jobController.DeleteJobDetails(jobId);
        }

        public Response JobAddressFromAdd(string userId, Address jobAddress)
        {
            return jobController.AddAddressFrom(userId, jobAddress);
        }

        public Response JobAddressFromGet(string userId)
        {
            return jobController.GetAddressesFrom(userId);
        }

        public Response JobAddressToAdd(string userId, Address jobAddress)
        {
            return jobController.AddAddressTo(userId, jobAddress);
        }

        public Response JobAddressToGet(string userId)
        {
            return jobController.GetAddressesTo(userId);
        }

        public Response JobDeliveryGetAll()
        {
            return jobController.GetJobDelivery();
        }

        public Response JobDeliveryGetById(string jobId)
        {
            return jobController.GetJobDelivery(jobId);
        }

        public Response JobDeliveryGetByCompany(string companyId)
        {
            return jobController.GetJobDeliveryByCompany(companyId);
        }

        public Response JobDeliveryGetByDriver(string userId)
        {
            return jobController.GetJobDeliveryByDriver(userId);
        }

        public Response JobDeliveryGetByStatus(string statusId)
        {
            return jobController.GetJobDeliveryByStatus(statusId);
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
            return commonController.GetJobStatus();
        }

        public Response JobDeliveryGetOpenJobs()
        {
            return jobController.GetOpenJobs();
        }

        public Response JobDeliveryStatusGetAll()
        {
            return jobController.GetJobDelivery();
        }

        public Response JobDeliveryStatusGet(string uniqueId)
        {
            return jobController.GetJobDeliveryByUniqueId(uniqueId);
        }

        public Response JobDeliveryStatusGetByCompany(string companyId)
        {
            return jobController.GetJobDeliveryByCompany(companyId);
        }

        public Response JobDeliveryStatusGetByDriver(string driverId)
        {
            return jobController.GetJobDeliveryByDriver(driverId);
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
    }
}
