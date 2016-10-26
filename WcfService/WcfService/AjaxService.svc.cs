using System;
using System.ServiceModel.Activation;
using System.Web;
using WcfService.Constant;
using WcfService.Controller;
using WcfService.Model;
using WcfService.Model.BillPlz;
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
        private static UserAuthHttpModule userAuthModule = new UserAuthHttpModule();
        private static PaymentController paymentController = new PaymentController();
        private static StatisticController statisticController = new StatisticController();

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

        public Response JobDeliveryDriverGet()
        {
            return jobController.GetJobDeliveryDriver();
        }

        public Response JobDeliveryAdd(string jobId, string companyId, string driverId, string fleetId)
        {
            return jobController.AddJobDelivery(jobId, companyId, driverId, fleetId);
        }

        public Response JobDeliveryDecline(string jobId, string companyId)
        {
            return jobController.DeclineJobDelivery(jobId, companyId);
        }

        public Response JobDeliveryUpdate(string jobId, string companyId, string driverId, string fleetId)
        {
            return jobController.UpdateJobDelivery(jobId, companyId, driverId, fleetId);
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
            return jobController.UpdateJobDeliveryStatus(jobId, statusId, pickupErrId, deliverErrId);
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

        public Response DeliveryPostcodeValidation(string deliverFrom, string deliverTo)
        {
            return commonController.CheckPostcodeValidation(deliverFrom, deliverTo);
        }

        public Response PriceGenerate()
        {
            return commonController.GeneratePrice();
        }

        public Response PriceGenerateDisposal()
        {
            return commonController.GeneratePriceDisposal();
        }

        public Response ValidateVoucher(string promoCode)
        {
            return commonController.ValidateVoucher(promoCode);
        }

        public void PaymentCallback(Bill bill)
        {
            paymentController.PaymentCallback(bill);
        }

        public Response PaymentMake(string uniqueId)
        {
            return paymentController.RequestPayment(uniqueId);
        }

        public Response StatisticDriverLocation()
        {
            return statisticController.GetDriverLocation();
        }
    }
}
