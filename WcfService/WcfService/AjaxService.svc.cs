using MySql.Data.MySqlClient;
using System;

namespace WcfService
{
    public class AjaxService : IAjaxService
    {
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

        public string AddAsset(string adminId, string token, string assetName, string regNum, string capacity, string roadTax)
        {
            throw new NotImplementedException();
        }

        public string AddCompany(string adminId, string token, string name, string contact, string website, string description)
        {
            throw new NotImplementedException();
        }

        public string AddCompanyAdmin(string adminId, string token, string companyId, string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public string AddJob(string adminId, string token, string customerName, string contactNum, string description, string destLongitude, string destLatitude, string add1, string add2, string add3, string poscode, int state, string deliveryDate, string deliveryTime, float quotation, bool cashOnDelivery, int assistance, int userId)
        {
            throw new NotImplementedException();
        }

        public string AddUser(string adminId, string token, string firstName, string lastName, string position, int gender, string contactNum, string dob, int assetId)
        {
            throw new NotImplementedException();
        }

        public string ChangePassword(string adminId, string token, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public string EditAsset(string adminId, string token, string assetId, string assetName, string regNum, string capacity, string roadTax)
        {
            throw new NotImplementedException();
        }

        public string EditCompany(string adminId, string token, string companyId, string name, string contact, string website, string description)
        {
            throw new NotImplementedException();
        }

        public string EditJob(string adminId, string token, string jobId, string customerName, string contactNum, string description, string destLongitude, string destLatitude, string add1, string add2, string add3, string poscode, int state, string deliveryDate, string deliveryTime, float quotation, bool cashOnDelivery, int assistance, int userId)
        {
            throw new NotImplementedException();
        }

        public string EditUser(string adminId, string token, string userId, string firstName, string lastName, string position, int gender, string contactNum, string dob, int assetId)
        {
            throw new NotImplementedException();
        }

        public string GetAssets(string adminId, string token)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success=true, ErrorCode = ErrorCodes.ESuccess, Payload = new Constants.Asset() { Name="test name", Capacity="10 tonne", RegNum="AAA123", RoadTaxExpiry="21 Mar 2016"} });
        }

        public string GetCompanies(string adminId, string token)
        {
            throw new NotImplementedException();
        }

        public string GetJobs(string adminId, string token)
        {
            throw new NotImplementedException();
        }

        public string GetLastTrackingPos(string adminId, string token, string userId)
        {
            throw new NotImplementedException();
        }

        public string GetLastTrackingPosbyList(string adminId, string token, string[] userIds)
        {
            throw new NotImplementedException();
        }

        public string GetTasks(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public string GetUsers(string adminId, string token)
        {
            throw new NotImplementedException();
        }

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


                    // generate the token for this user
                    string newToken = Guid.NewGuid().ToString();
                    string newValidity = Utils.GetCurrentUtcTime(1);

                    // update token and validity
                    MySqlCommand tokenCommand = new MySqlCommand("UPDATE masteradmins SET validity=@0, token=@1 where id=@2");
                    tokenCommand.Parameters.AddWithValue("@0", newValidity);
                    tokenCommand.Parameters.AddWithValue("@1", newToken);
                    tokenCommand.Parameters.AddWithValue("@2", adminId);
                    Utils.PerformSqlNonQuery(tokenCommand);

                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess,
                        Payload = new Constants.AdminInfo() {
                            AdminId = adminId,
                            Permission = (int)reader["permission"],
                            Token = newToken,
                            Username = (string)reader["username"]
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

        public string RemoveAsset(string adminId, string token, string assetId)
        {
            throw new NotImplementedException();
        }

        public string RemoveAssets(string adminId, string token, string[] assetIds)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompanies(string adminId, string token, string[] companyIds)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompany(string adminId, string token, string companyId)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompanyAdmin(string adminId, string token, string companyAdminId)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompanyAdmins(string adminId, string token, string[] companyAdminIds)
        {
            throw new NotImplementedException();
        }

        public string RemoveJob(string adminId, string token, string jobId)
        {
            throw new NotImplementedException();
        }

        public string RemoveJobs(string adminId, string token, string[] jobIds)
        {
            throw new NotImplementedException();
        }

        public string RemoveUser(string adminId, string token, string userId)
        {
            throw new NotImplementedException();
        }

        public string RemoveUsers(string adminId, string token, string[] userIds)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(string adminId, string token, string username)
        {
            throw new NotImplementedException();
        }

        public string UpdateDevOrder(string userId, string token, string taskId, string orderBase64)
        {
            throw new NotImplementedException();
        }

        public string UpdateLocation(string userId, string longitude, string latitude)
        {
            throw new NotImplementedException();
        }

        public string UpdateTask(string userId, string token, string taskId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
