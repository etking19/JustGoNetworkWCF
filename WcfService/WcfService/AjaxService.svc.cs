﻿using System;
using System.ServiceModel.Activation;
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

        public Response CompanyProfileAdd(Company company)
        {
            return companyController.AddCompany(company);
        }

        public Response CompanyProfileDelete(string companyId)
        {
            return companyController.DeleteCompany(companyId);
        }

        public Response CompanyProfileGet()
        {
            return companyController.GetCompanies();
        }

        public Response CompanyProfileUpdate(string companyId, Company company)
        {
            return companyController.UpdateCompany(companyId, company);
        }

        public Response CountryGet()
        {
            return commonController.GetCountry();
        }

        public Response FleetLorryAdd(Fleet fleet)
        {
            return fleetController.AddFleet(fleet);
        }

        public Response FleetLorryDelete(string fleetId)
        {
            return fleetController.DeleteFleet(fleetId);
        }

        public Response FleetLorryGet()
        {
            return fleetController.GetFleet();
        }

        public Response FleetTypeGet()
        {
            return commonController.GetFleetType();
        }

        public Response FleetLorryUpdate(string fleetId, Fleet fleet)
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

        public Response StateGet()
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

        public Response PickupErrGet()
        {
            return commonController.GetPickupError();
        }

        public Response DeliveryErrGet()
        {
            return commonController.GetDeliveryError();
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

        public Response JobDeliveryGet()
        {
            return jobController.GetJobDelivery();
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

        public Response JobSetRating(string uniqueId, float rating)
        {
            return jobController.SetRating(uniqueId, rating);
        }

        public Response JobDeliveryStatusUpdate(string jobId, string statusId, string pickupErrId, string deliverErrId)
        {
            throw new NotImplementedException();
        }

        public Response ActivityGet()
        {
            return commonController.GetActivity();
        }

        public Response JobStatusTypeGet()
        {
            return commonController.GetJobStatusType();
        }

        public Response JobTypeGet()
        {
            return commonController.GetJobTypes();
        }
    }
}
