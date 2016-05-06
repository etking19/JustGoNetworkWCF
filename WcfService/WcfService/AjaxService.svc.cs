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
        public string AddCompany(string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CompanyManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Companies().AddCompany(name, address1, address2, postcode, stateId, countryId, ssm);
        }

        public string AddCountry(string name)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CountryManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().AddCountry(name);
        }

        public string AddFleetType(string name, int capacity, string design)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetTypeManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new FleetTypes().AddFleetType(name, capacity, design);
        }

        public string AssignJob(int jobId, int deliveryCompanyId)
        {
            throw new NotImplementedException();
        }

        public string AddRating(int jobId, float score)
        {
            throw new NotImplementedException();
        }

        public string AddState(int countryId, string name)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.StateManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new States().AddState(countryId, name);
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            return new General().ChangePassword(getUsernameFromAuth(), oldPassword, newPassword);
        }

        public string DeleteCountries(int[] countryIds)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CountryManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().DeleteCountries(countryIds);
        }

        public string DeleteCountry(int countryId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CountryManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().DeleteCountry(countryId);
        }

        public string DeleteFleet(string identifier)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Fleets().DeleteFleet(identifier);
        }

        public string DeleteFleets(string[] identifier)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Fleets().DeleteFleets(identifier);
        }

        public string DeleteFleetType(int fleetId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetTypeManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new FleetTypes().DeleteFleetType(fleetId);
        }

        public string DeleteFleetTypes(int[] fleetIds)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetTypeManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new FleetTypes().DeleteFleetTypes(fleetIds);
        }

        public string DeleteState(int stateId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.StateManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new States().DeleteState(stateId);
        }

        public string DeleteStates(int[] stateIds)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.StateManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new States().DeleteStates(stateIds);
        }

        public string EditAdmin(string username, string displayName, int companyId, int roleId)
        {
            throw new NotImplementedException();
        }

        public string EditCompany(int companyId, string name, string address1, string address2, string postcode, int stateId, int countryId, string ssm)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CompanyManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Companies().EditCompany(companyId, name, address1, address2, postcode, stateId, countryId, ssm);
        }

        public string EditCountry(int countryId, string name)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CountryManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Countries().EditCountry(countryId, name);
        }

        public string EditFleet(string identifier, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            throw new NotImplementedException();
        }

        public string EditFleetType(int fleetId, string name, int capacity, string design)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetTypeManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new FleetTypes().EditFleetType(fleetId, name, capacity, design);
        }

        public string EditRole(int roleId, string name)
        {
            throw new NotImplementedException();
        }

        public string EditState(int stateId, int countryId, string name)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.StateManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new States().EditState(stateId, countryId, name);
        }

        public string GetCompanies()
        {
            return new Companies().GetCompanies();
        }

        public string GetCountries()
        {
            return new Countries().GetCountries();
        }

        public string GetDeliveryErrorsEle()
        {
            throw new NotImplementedException();
        }

        public string GetDrivers()
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.Driversmanagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = new Drivers().GetDriversList() });
        }

        public string GetDriversByCompanyId(int companyId)
        {
            var list = new Drivers().GetDriversList().FindAll(x => x.CompanyId == companyId);

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = list });
        }

        public string GetFleets()
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetTypeManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Fleets().GetFleets();
        }

        public string GetFleetsByCompanyId(int companyId)
        {
            return new Fleets().GetFleetsByCompanyId(companyId);
        }

        public string GetFleetTypes()
        {
            return new FleetTypes().GetFleetTypes();
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
            return new Users().GetRoles();
        }

        public string GetStates(int countryId)
        {
            return new States().GetStates(countryId);
        }

        public string Login(string username, string password)
        {
            return new General().Login(username, password);
        }

        public string RemoveCompanies(int[] companyIds)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CompanyManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Companies().RemoveCompanies(companyIds);
        }

        public string RemoveCompany(int companyId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CompanyManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Companies().RemoveCompany(companyId);
        }

        public string RemoveDriver(int userId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.Driversmanagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            new Drivers().RemoveDriver(userId);

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
        }

        public string RemoveDrivers(int[] userIds)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.Driversmanagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            Drivers drivers = new Drivers();
            foreach (int userId in userIds)
            {
                drivers.RemoveDriver(userId);
            }
            
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
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
            Constants.User user = new Constants.User();
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.TrackingManagement, out user))
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

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success=true, ErrorCode = ErrorCodes.ESuccess, Payload = user });
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

        public string UpdateJobStatus(int jobId, Constants.EJobDispatchStatus jobStatus)
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

        private bool checkAuthentication(Constants.EPermission expectedPermission, out Constants.User userObj)
        {
            userObj = new Constants.User();

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

            using (MySqlCommand command = Utils.GenerateQueryCmd("users", queryParams))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    if ((int)reader["enabled"] == 0)
                    {
                        return false;
                    }

                    int result = DateTime.UtcNow.CompareTo(reader["validity"]);
                    if (result > 0)
                    {
                        return false;
                    }

                    // refresh the token validity
                    Utils.RefreshToken(username);

                    userObj = new Constants.User()
                    {
                        Id = (int)reader["id"],
                        Username = (string)reader["username"],
                        DisplayName = (string)reader["display_name"],
                        Enabled = (int)reader["enabled"] == 0 ? true : false,
                        PushIdentifier = (string)reader["push_identifier"],
                        LastLogin = reader["last_login_date"].ToString()
                    };

                    if (((int)expectedPermission & (int)Constants.ERolePermission.Users) == (int)expectedPermission)
                    {
                        return true;
                    }
                }
            }

            // check if user from master admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userObj.Id.ToString());
            using (MySqlCommand command = Utils.GenerateQueryCmd("master_admins", queryParams))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    if (reader.Read())
                    {
                        if (((int)expectedPermission & (int)Constants.ERolePermission.MasterAdmins) == (int)expectedPermission)
                        {
                            return true;
                        }
                    }
                }
            }

            // check if user from lorry partners
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userObj.Id.ToString());
            using (MySqlCommand command = Utils.GenerateQueryCmd("lorry_partners", queryParams))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    if (reader.Read())
                    {
                        var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                        if (companyObj.Enabled == false)
                        {
                            return false;
                        }

                        if (((int)expectedPermission & (int)Constants.ERolePermission.LorryPartners) == (int)expectedPermission)
                        {
                            return true;
                        }
                    }
                }
            }


            // check if user from drivers admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userObj.Id.ToString());
            using (MySqlCommand command = Utils.GenerateQueryCmd("drivers", queryParams))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    if (reader.Read())
                    {
                        var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                        if (companyObj.Enabled == false)
                        {
                            throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Company disabled" }));
                        }

                        if (((int)expectedPermission & (int)Constants.ERolePermission.Drivers) == (int)expectedPermission)
                        {
                            return true;
                        }
                    }
                }
            }


            // check if user from corporate admin
            queryParams = new Dictionary<string, string>();
            queryParams.Add("user_id", userObj.Id.ToString());
            using (MySqlCommand command = Utils.GenerateQueryCmd("corporate_partners", queryParams))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    if (reader.Read())
                    {
                        var companyObj = new Companies().GetCompany((int)reader["company_id"]);
                        if (companyObj.Enabled == false)
                        {
                            throw new Exception(Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended, ErrorMessage = "Company disabled" }));
                        }

                        if (((int)expectedPermission & (int)Constants.ERolePermission.CorporatePartners) == (int)expectedPermission)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public string EnableCompany(int companyId, bool enabled)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.CompanyManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Companies().EnableCompany(companyId, enabled);
        }

        public string EnableUser(int userId, bool enabled)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.UserManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Users().EnableUser(userId, enabled);
        }

        public string AddUser(string username, string displayName, int[] permissions, int companyId, string identityCard)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.UserManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Users().AddUser(username, displayName, permissions, companyId, identityCard);
        }

        public string EditUser(string username, string displayName, int[] permissions, int companyId, string identityCard)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.UserManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Users().EditUser(username, displayName, permissions, companyId, identityCard);
        }

        public string AddFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Fleets().AddFleet(identifier, companyId, remarks, fleetTypeId, roadTaxExpiry, serviceDueDate);
        }

        public string EditFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.FleetManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Fleets().EditFleet(identifier, companyId, remarks, fleetTypeId, roadTaxExpiry, serviceDueDate);
        }

        public string GetJobsDispatch(int status)
        {
        }

        public string AddJobDispatch(int jobId, int companyId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsDispatchManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new JobsDispatched().AddJobDispatch(jobId, companyId);
        }

        public string EditJobDispatch(int id, int jobId, int companyId)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsDispatchManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new JobsDispatched().EditJobDispatch(id, companyId);
        }

        public string DeleteJobDispatch(int id)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsDispatchManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new JobsDispatched().DeleteJobDispatch(id);
        }

        public string DeleteJobsDispatch(int[] ids)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsDispatchManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new JobsDispatched().DeleteJobsDispatch(ids);
        }

        public string AssignJobDispatch(int id, string driverUsername)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsDispatchManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new JobsDispatched().AssignDriver(id, driverUsername);
        }


        public string AddJob(int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Jobs().AddJob(companyId, customerName, customerContact,
                pickupCustomerName, pickupCustomerContact, pickupAdd1, pickupAdd2,
                pickupPoscode, pickupStateId, pickupCountryId, pickupLongitude, pickupLatitude,
                deliverCustomerName, deliverCustomerContact, deliverAdd1, deliverAdd2,
                deliverPoscode, deliverStateId, deliverCountryId, deliverLongitude, deliverLatitude,
                deliveryDateTime, amount, cashOnDelivery, workerAssistance, remarks);
        }

        public string EditJob(int id, int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Jobs().EditJob(id, companyId, customerName, customerContact,
                pickupCustomerName, pickupCustomerContact, pickupAdd1, pickupAdd2,
                pickupPoscode, pickupStateId, pickupCountryId, pickupLongitude, pickupLatitude,
                deliverCustomerName, deliverCustomerContact, deliverAdd1, deliverAdd2,
                deliverPoscode, deliverStateId, deliverCountryId, deliverLongitude, deliverLatitude,
                deliveryDateTime, amount, cashOnDelivery, workerAssistance, remarks);
        }

        public string RemoveJob(int id)
        {
            Constants.User user;
            if ((System.Configuration.ConfigurationManager.AppSettings["bypassAuthentication"] == "0") &&
                checkAuthentication(Constants.EPermission.JobsManagement, out user))
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied });
            }

            return new Jobs().RemoveJob(id);
        }

        public string GetOpenJobs()
        {
        }

    }
}
