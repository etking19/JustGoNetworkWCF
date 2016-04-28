using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using WcfService.Helper;

namespace WcfService
{
    public class AjaxService : IAjaxService
    {
        public string AddCompany(string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            throw new NotImplementedException();
        }

        public string AddCountry(string name)
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().AddCountry(name);
        }

        public string AddDriver(string username, string displayName, string mykad)
        {
            throw new NotImplementedException();
        }

        public string AddFleet(string identifier, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            throw new NotImplementedException();
        }

        public string AddFleetType(string name, int capacity, string design)
        {
            throw new NotImplementedException();
        }

        public string AddJob(string customerName, string customerContact, string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2, string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude, string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2, string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude, string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            throw new NotImplementedException();
        }

        public string AddJobPartner(string customerName, string customerContact, string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2, string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude, string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2, string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude, string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            throw new NotImplementedException();
        }

        public string AddRating(int jobId, float score)
        {
            throw new NotImplementedException();
        }

        public string AddRole(string name)
        {
            throw new NotImplementedException();
        }

        public string AddState(int countryId, string name)
        {
            throw new NotImplementedException();
        }

        public string AssignJob(int jobId, int deliveryCompanyId)
        {
            throw new NotImplementedException();
        }

        public string AssignJobPartner(int jobId, string driverUsername)
        {
            throw new NotImplementedException();
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.Users))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new General().ChangePassword(getUsernameFromAuth(), oldPassword, newPassword);
        }

        public string DeleteAdmin(string username)
        {
            throw new NotImplementedException();
        }

        public string DeleteCountries(int[] countryIds)
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().DeleteCountries(countryIds);
        }

        public string DeleteCountry(int countryId)
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().DeleteCountry(countryId);
        }

        public string DeleteFleet(string identifier)
        {
            throw new NotImplementedException();
        }

        public string DeleteFleets(string[] identifier)
        {
            throw new NotImplementedException();
        }

        public string DeleteFleetType(int fleetId)
        {
            throw new NotImplementedException();
        }

        public string DeleteFleetTypes(int[] fleetIds)
        {
            throw new NotImplementedException();
        }

        public string DeleteJob(int jobId)
        {
            throw new NotImplementedException();
        }

        public string DeleteJobs(int[] jobIds)
        {
            throw new NotImplementedException();
        }

        public string DeleteState(int stateId)
        {
            throw new NotImplementedException();
        }

        public string DeleteStates(int[] stateIds)
        {
            throw new NotImplementedException();
        }

        public string EditAdmin(string username, string displayName, int companyId, int roleId)
        {
            throw new NotImplementedException();
        }

        public string EditCompany(int companyId, string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            throw new NotImplementedException();
        }

        public string EditCountry(int countryId, string name)
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().EditCountry(countryId, name);
        }

        public string EditDriver(string username, string displayName, string mykad)
        {
            throw new NotImplementedException();
        }

        public string EditFleet(string identifier, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            throw new NotImplementedException();
        }

        public string EditFleetType(int fleetId, string name, int capacity, string design)
        {
            throw new NotImplementedException();
        }

        public string EditJob(int jobId, string customerName, string customerContact, string pickupCustomerName, 
            string pickupCustomerContact, string pickupAdd1, string pickupAdd2, string pickupPoscode, int pickupStateId, 
            int pickupCountryId, float pickupLongitude, float pickupLatitude, string deliverCustomerName, string deliverCustomerContact, 
            string deliverAdd1, string deliverAdd2, string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, 
            float deliverLatitude, string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            throw new NotImplementedException();
        }

        public string EditRole(int roleId, string name)
        {
            throw new NotImplementedException();
        }

        public string EditState(int stateId, int countryId, string name)
        {
            throw new NotImplementedException();
        }

        public string GetCompanies()
        {
            throw new NotImplementedException();
        }

        public string GetCountries()
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().GetCountries();
        }

        public string GetDeliveryErrorsEle()
        {
            throw new NotImplementedException();
        }

        public string GetDrivers()
        {
            throw new NotImplementedException();
        }

        public string GetFleets()
        {
            throw new NotImplementedException();
        }

        public string GetFleetTypes()
        {
            throw new NotImplementedException();
        }

        public string GetJobsPartner(Constants.EJobStatus status)
        {
            throw new NotImplementedException();
        }

        public string GetLastTrackingPos(string driverUsername)
        {
            throw new NotImplementedException();
        }

        public string GetLastTrackingPosbyList(string[] driverUsername)
        {
            throw new NotImplementedException();
        }

        public string GetPermissions()
        {
            throw new NotImplementedException();
        }

        public string GetPickUpErrorsEle()
        {
            throw new NotImplementedException();
        }

        public string GetRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public string GetRoles()
        {
            throw new NotImplementedException();
        }

        public string GetStates(int countryId)
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            return new General().Login(username, password);
        }

        public string RemoveCompanies(int[] companyIds)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public string RemoveDriver(string username)
        {
            throw new NotImplementedException();
        }

        public string RemoveDrivers(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public string RemoveRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public string RemoveRoles(int[] roleIds)
        {
            throw new NotImplementedException();
        }

        public string ForgotPassword(string username)
        {
            return new General().ForgotPassword(username);
        }

        public string ResetRating(string username)
        {
            throw new NotImplementedException();
        }

        public string Test()
        {
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                (checkAuthentication() < Constants.EPermission.MasterAdmins))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Constants.sConnectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { ErrorCode= ErrorCodes.EGeneralError, ErrorMessage=ex.Message, Success=false });
            }

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success=true, ErrorCode = ErrorCodes.ESuccess });
        }

        public string UpdateDeliveryOrder(int jobId, string base64DO)
        {
            throw new NotImplementedException();
        }

        public string UpdateJobDeliveryError(int jobId, Constants.DeliveryError deliveryError)
        {
            throw new NotImplementedException();
        }

        public string UpdateJobPickUpError(int jobId, Constants.PickUpError pickupError)
        {
            throw new NotImplementedException();
        }

        public string UpdateJobStatus(int jobId, Constants.EJobStatus jobStatus)
        {
            throw new NotImplementedException();
        }

        public string UpdateJobStatusRemarks(int jobId, string remarks)
        {
            throw new NotImplementedException();
        }

        public string UpdateLocation(string username, float longitude, float latitude, float speed)
        {
            throw new NotImplementedException();
        }

        private string getUsernameFromAuth()
        {
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            WebHeaderCollection headers = request.Headers;

            string[] authentication = headers["Authorization"].Split(':');
            return authentication[0];
        }

        private Constants.EPermission checkAuthentication()
        {
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            WebHeaderCollection headers = request.Headers;

            string[] authentication = headers["Authorization"].Split(':');
            if (authentication.Length != 2)
            {
                throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EAuthenticationFormatErr, ErrorMessage = "Expected header Authentication in correct format {username}:{token}" }));
            }

            string username = authentication[0];
            string token = authentication[1];

            // validate the username and token
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("username", username);
            queryParams.Add("token", token);

            MySqlCommand command = Utils.GenerateQueryCmd("users", queryParams);
            int userId = 0;
            using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
            {
                if(!reader.Read())
                {
                    return Constants.EPermission.NoRole;
                }

                if((int)reader["enabled"] == 0)
                {
                    throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Account disabled" }));
                }

                int result = DateTime.UtcNow.CompareTo(reader["validity"]);
                if (result > 0)
                {
                    // token expired
                    throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginExpired, ErrorMessage = "Login expired" }));
                }

                userId = (int)reader["id"];
            }

            // refresh the token validity
            Utils.RefreshToken(username);

            // check if user from master admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userId.ToString());
            command = Utils.GenerateQueryCmd("master_admins", queryParams);
            using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
            {
                if (reader.Read())
                {
                    return Constants.EPermission.MasterAdmins;
                }
            }

            // check if user from lorry partners
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userId.ToString());
            command = Utils.GenerateQueryCmd("lorry_partners", queryParams);
            using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
            {
                if (reader.Read())
                {
                    var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                    if(companyObj.Enabled == false)
                    {
                        throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Company disabled" }));
                    }

                    return Constants.EPermission.LorryPartners;
                }
            }

            // check if user from master admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userId.ToString());
            command = Utils.GenerateQueryCmd("drivers", queryParams);
            using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
            {
                if (reader.Read())
                {
                    var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                    if (companyObj.Enabled == false)
                    {
                        throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Company disabled" }));
                    }

                    return Constants.EPermission.Drivers;
                }
            }

            // check if user from master admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userId.ToString());
            command = Utils.GenerateQueryCmd("corporate_partners", queryParams);
            using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
            {
                if (reader.Read())
                {
                    var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                    if (companyObj.Enabled == false)
                    {
                        throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Company disabled" }));
                    }

                    return Constants.EPermission.CorporatePartners;
                }
            }

            return Constants.EPermission.Users;
        }

        public string EnableCompany(int companyId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public string GetUsers()
        {
            throw new NotImplementedException();
        }

        public string EnableUser(int userId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public string AddUser(string username, string displayName, int[] permissions, int companyId)
        {
            throw new NotImplementedException();
        }

        public string EditUser(int userId, string displayName, int[] permissions, int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
