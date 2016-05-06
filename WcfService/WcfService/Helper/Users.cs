using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Helper
{
    public class Users
    {
        private static string sDatabaseName = "users";

        public string GetRoles()
        {
            // follow EPermission enum
            List<Constants.Role> roleList = new List<Constants.Role>();

            roleList.Add(new Constants.Role()
            {
                Name = "Users",
                Permission = Constants.ERolePermission.Users
            });

            roleList.Add(new Constants.Role()
            {
                Name = "LorryPartners",
                Permission = Constants.ERolePermission.LorryPartners
            });

            roleList.Add(new Constants.Role()
            {
                Name = "Drivers",
                Permission = Constants.ERolePermission.Drivers
            });

            roleList.Add(new Constants.Role()
            {
                Name = "CorporatePartners",
                Permission = Constants.ERolePermission.CorporatePartners
            });

            roleList.Add(new Constants.Role()
            {
                Name = "MasterAdmins",
                Permission = Constants.ERolePermission.MasterAdmins
            });

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = roleList });
        }

        public string GetUsers()
        {
            try
            {
                var userList = GetUsersList();
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = userList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string EnableUser(int userId, bool enabled)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("enabled", enabled ? "1" : "0");

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", userId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string AddUser(string username, string displayName, int[] permissions, int companyId, string identityCard)
        {
            try
            {
                // 1. add the user to user DB

                // generate new password
                int generatedPw = new Random().Next(1000, 9999);

                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("username", username);
                insertParams.Add("displayName", displayName);
                insertParams.Add("password", generatedPw.ToString());

                using (MySqlCommand command = Utils.GenerateAddCmd(sDatabaseName, insertParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                // 2. Push the user to appropriate admin DB
                var newlyInserted = GetUsersList().Find(x => x.Username == username);

                bool adminRight = false;
                if(permissions.Contains((int)Constants.ERolePermission.MasterAdmins))
                {
                    new MasterAdmins().AddMasterAdmin(newlyInserted.Id);
                    adminRight = true;
                }

                if (permissions.Contains((int)Constants.ERolePermission.CorporatePartners))
                {
                    new CorporatePartners().AddCorporatePartner(newlyInserted.Id, companyId);
                    adminRight = true;
                }

                if (permissions.Contains((int)Constants.ERolePermission.Drivers))
                {
                    new Drivers().AddDriver(newlyInserted.Id, companyId, identityCard);
                    UtilSms.SendSms(newlyInserted.Username, string.Format("Just Logistic Berhad.%0A%0ANew account created. Download GoLorry app at {2}%0AUsername: {0}%0APassword: {1}", 
                        username, generatedPw, System.Configuration.ConfigurationManager.AppSettings["GoLorryAppUrl"]));
                }

                if (permissions.Contains((int)Constants.ERolePermission.LorryPartners))
                {
                    new LorryPartners().AddLorryPartner(newlyInserted.Id, companyId);
                    adminRight = true;
                }

                // 3. Send notification for new password
                if(adminRight)
                {
                    UtilSms.SendSms(newlyInserted.Username, string.Format("Just Logistic Berhad.%0A%0ANew account created. You may login to our system  {2}%0AUsername: {0}%0APassword: {1}",
                        username, generatedPw, System.Configuration.ConfigurationManager.AppSettings["AdminPageUrl"]));
                }
                

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string EditUser(string username, string displayName, int[] permissions, int companyId, string identityCard)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("displayName", displayName);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("username", username);

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                // check if user has the permission
                var newlyInserted = GetUsersList().Find(x => x.Username == username);
                if (permissions.Contains((int)Constants.ERolePermission.MasterAdmins))
                {
                    new MasterAdmins().AddMasterAdmin(newlyInserted.Id);
                }
                else
                {
                    new MasterAdmins().RemoveMasterAdmin(newlyInserted.Id);
                }


                if (permissions.Contains((int)Constants.ERolePermission.CorporatePartners))
                {
                    var obj = new CorporatePartners().GetCorporatePartnerByUserId(newlyInserted.Id);
                    if (obj != null)
                    {
                        // exisiting
                        new CorporatePartners().EditCorporatePartner(obj.Id, newlyInserted.Id, companyId);
                    }
                    else
                    {
                        // no entry previously
                        new CorporatePartners().AddCorporatePartner(newlyInserted.Id, companyId);
                    }
                    
                }
                else
                {
                    new CorporatePartners().RemoveCorporatePartner(newlyInserted.Id);
                }


                if (permissions.Contains((int)Constants.ERolePermission.Drivers))
                {
                    var obj = new Drivers().GetDriverByUserId(newlyInserted.Id);
                    if(obj != null)
                    {
                        new Drivers().EditDriver(obj.Id, newlyInserted.Id, companyId, obj.Rating, identityCard);
                    }
                    else
                    {
                        new Drivers().AddDriver(newlyInserted.Id, companyId, identityCard);
                    }
                    
                }
                else
                {
                    new Drivers().RemoveDriver(newlyInserted.Id);
                }


                if (permissions.Contains((int)Constants.ERolePermission.LorryPartners))
                {
                    var obj = new LorryPartners().GetLorryPartnerByUserId(newlyInserted.Id);
                    if(obj != null)
                    {
                        new LorryPartners().EditLorryPartner(obj.Id, newlyInserted.Id, companyId);
                    }
                    else
                    {
                        new LorryPartners().AddLorryPartner(newlyInserted.Id, companyId);
                    }
                    
                }

                else
                {
                    new LorryPartners().RemoveLorryPartner(newlyInserted.Id);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public List<Constants.User> GetUsersList()
        {
            List<Constants.User> userList = new List<Constants.User>();
            using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    while (reader.Read())
                    {
                        userList.Add(new Constants.User()
                        {
                            Id = (int)reader["id"],
                            Username = (string)reader["username"],
                            Password = (string)reader["password"],
                            DisplayName = (string)reader["display_name"],
                            Token = (string)reader["token"],
                            Enabled = (int)reader["enabled"] > 0 ? true : false,
                            TokenValidity = reader["validity"].ToString(),
                            LastLogin = reader["last_login_date"].ToString(),
                            PushIdentifier = (string)reader["push_identifier"]
                        });
                    }
                }
            }

            return userList;
        }

        public Constants.User GetUser(int userId)
        {
            return GetUsersList().Find(x => x.Id == userId);
        }
    }
}