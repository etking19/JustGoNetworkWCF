using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Roles
    {
        private static string sDatabaseName = "roles";

        public string GetRoles()
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }

        public Constants.Role GetRole(int roleId)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", roleId.ToString());

                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    return null;
                }

                Utils.CleanUp(reader, command);
                return new Constants.Role()
                {
                    Id = roleId,
                    Name = (string)reader["name"],
                    Permission = (int)reader["permission"]
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPermissions()
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }

        public string AddRole(string name)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }

        public string EditRole(int roleId, string name)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }

        public string RemoveRole(int roleId)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }

        public string RemoveRoles(int[] roleIds)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError });
        }
    }
}