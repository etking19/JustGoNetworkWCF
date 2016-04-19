using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.ServiceModel.Web;
using WcfService.Helper;

namespace WcfService
{
    public class AjaxService : IAjaxService
    {
        public string AddAdmin(string username, string displayName, int companyId, int roleId)
        {
            throw new NotImplementedException();
        }

        public string AddCompany(string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            throw new NotImplementedException();
        }

        public string AddCountry(string name)
        {
            throw new NotImplementedException();
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
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            WebHeaderCollection headers = request.Headers;

            string[] authentication = headers["Authorization"].Split(':');
            if(authentication.Length != 2)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EAuthenticationFormatErr });
            }

            int permission;
            int result = Utils.ValidateToken(authentication[0], authentication[1], out permission);
            if (result != ErrorCodes.ESuccess)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = result });
            }

            return new General().ChangePassword(authentication[0], oldPassword, newPassword);
        }

        public string DeleteAdmin(string username)
        {
            throw new NotImplementedException();
        }

        public string DeleteAdmins(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public string DeleteCountries(int[] countryIds)
        {
            throw new NotImplementedException();
        }

        public string DeleteCountry(int countryId)
        {
            throw new NotImplementedException();
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

        public string EditCountry(int countryId)
        {
            throw new NotImplementedException();
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

        public string GetAdmins()
        {
            throw new NotImplementedException();
        }

        public string GetCompanies()
        {
            throw new NotImplementedException();
        }

        public string GetCountries()
        {
            throw new NotImplementedException();
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

        public string ResetPassword(string username)
        {
            throw new NotImplementedException();
        }

        public string ResetRating(string username)
        {
            throw new NotImplementedException();
        }

        public string Test()
        {
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
        /*
       public string Login(string username, string password)
       {
           try
           {
               MySqlCommand command = new MySqlCommand("SELECT * FROM masteradmins where username=@0 and password=@1");
               command.Parameters.AddWithValue("@0", username);
               command.Parameters.AddWithValue("@1", password);

               MySqlDataReader reader = Utils.PerformSqlQuery(command);
               if (reader.Read())
               {
                   int adminId = (int)reader["id"];
                   int permission = (int)reader["permission"];
                   string usernameRet = (string)reader["username"];

                   // generate the token for this user
                   string newToken = Guid.NewGuid().ToString();
                   string newValidity = Utils.GetCurrentUtcTime(1);

                   // update token and validity
                   MySqlCommand tokenCommand = new MySqlCommand("UPDATE masteradmins SET validity=@0, token=@1 where id=@2");
                   tokenCommand.Parameters.AddWithValue("@0", newValidity);
                   tokenCommand.Parameters.AddWithValue("@1", newToken);
                   tokenCommand.Parameters.AddWithValue("@2", adminId);
                   Utils.PerformSqlNonQuery(tokenCommand);

                   Utils.CleanUp(reader, command);
                   return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess,
                       Payload = new Constants.AdminInfo() {
                           AdminId = adminId,
                           Permission = permission,
                           Token = newToken,
                           Username = usernameRet
                       }
                   });

               }
           }
           catch (Exception ex)
           {
               return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage=ex.Message });
           }

           return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginCredential, ErrorMessage = "Failed to login" });
       }

       public string UpdateLocation(string userId, float longitude, float latitude, float speed)
       {
           //TODO: get the asset id own by this driver today
           // -1 if no asset assigned
           try
           {
               MySqlCommand command = new MySqlCommand("INSERT into tracking_data (driver_id, longitude, latitude, speed, asset_id, creation_date) values (@driver_id, @longitude, @latitude, @speed, @asset_id, @creation_date)");
               command.Parameters.AddWithValue("@driver_id", userId);
               command.Parameters.AddWithValue("@longitude", longitude);
               command.Parameters.AddWithValue("@latitude", latitude);
               command.Parameters.AddWithValue("@speed", speed);
               command.Parameters.AddWithValue("@asset_id", 888888);        // TODO:
               command.Parameters.AddWithValue("@creation_date", Utils.GetCurrentUtcTime(0));

               Utils.PerformSqlNonQuery(command);
           }
           catch (Exception ex)
           {
               return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
           }

           return Constants.sJavaScriptSerializer.Serialize(new Constants.Result()
           {
               Success = true,
               ErrorCode = ErrorCodes.ESuccess
           });
       }
       */
    }
}
