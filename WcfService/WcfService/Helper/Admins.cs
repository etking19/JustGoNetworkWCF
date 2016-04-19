using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Admins
    {
        private static string sDatabaseName = "admins";

        public string GetAdmins()
        {
            throw new NotImplementedException();
        }


        public Constants.Admin GetAdmin(string username, out string password)
        {
            try
            {
                password = String.Empty;

                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("username", username);

                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    return null;
                }

                password = (string)reader["password"];
                Constants.Admin adminObj = new Constants.Admin()
                {
                    Username = username,
                    DisplayName = (string)reader["display_name"],
                    Token = (string)reader["token"],
                    TokenValidity = reader["validity"].ToString(),
                    Company = new Companies().GetCompany((int)reader["company_id"]),
                    Role = new Roles().GetRole((int)reader["role_id"]),
                    Enabled = (int)reader["enabled"] > 0? true : false
                };
                Utils.CleanUp(reader, command);

                return adminObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Constants.Admin GetAdmin(string username)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("username", username);

                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    return null;
                }

                Constants.Admin adminObj = new Constants.Admin()
                {
                    Username = username,
                    DisplayName = (string)reader["display_name"],
                    Token = (string)reader["token"],
                    TokenValidity = reader["validity"].ToString(),
                    Company = new Companies().GetCompany((int)reader["company_id"]),
                    Role = new Roles().GetRole((int)reader["role_id"]),
                    Enabled = (int)reader["enabled"] > 0 ? true : false
                };
                Utils.CleanUp(reader, command);

                return adminObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddAdmin(string username, string displayName, int companyId, int roleId)
        {
            throw new NotImplementedException();
        }

        public string EditAdmin(string username, string displayName, int companyId, int roleId)
        {
            throw new NotImplementedException();
        }

        public string DeleteAdmin(string username)
        {
            throw new NotImplementedException();
        }

        public string DeleteAdmins(string[] usernames)
        {
            throw new NotImplementedException();
        }
    }
}