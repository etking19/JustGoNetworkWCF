using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WcfService.Helper;

namespace WcfService
{
    public class Utils
    {
        public static string ValidateTokenNPermission(string adminId, string token, int expectedPermission)
        {
            int permission;
            int result = ValidateToken(adminId, token, out permission);
            if (result != ErrorCodes.ESuccess)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = result, ErrorMessage = "Fail to login." });
            }

            if ((permission & (int)expectedPermission) != (int)expectedPermission)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPermissionDenied, ErrorMessage = "Permission denied" });
            }

            return String.Empty;
        }

        public static int ValidateToken(string adminUsername, string token, out int permission)
        {
            permission = 0;
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("username", adminUsername);
                MySqlCommand command = Utils.GenerateQueryCmd("admins", queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);

                int errorCode = 0;
                if (!reader.Read())
                {
                    errorCode = ErrorCodes.ELoginCredential;
                }

                if((int)reader["enabled"] == 0)
                {
                    errorCode = ErrorCodes.ELoginAccSuspended;
                }

                if(String.Compare((string)reader["token"], token) != 0)
                {
                    errorCode = ErrorCodes.ELoginExpired;
                }

                int result = DateTime.UtcNow.CompareTo(reader["validity"]);
                if (result > 0)
                {
                    errorCode = ErrorCodes.ELoginExpired;
                }

                if(errorCode != 0)
                {
                    return errorCode;
                }

                // add validity time for token
                RefreshToken(adminUsername);

                // get the permission
                permission = new Roles().GetRole((int)reader["role_id"]).Permission;
                Utils.CleanUp(reader, command);

                return ErrorCodes.ESuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ErrorCodes.EGeneralError;
            }
        }

        public static string GetCurrentUtcTime(int hourOffset)
        {
            return DateTime.UtcNow.AddHours(hourOffset).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int RefreshToken(string adminUsername, string newtoken, string newValidity)
        {
            // refresh for another hour
            MySqlCommand tokenCommand = new MySqlCommand("UPDATE admins SET validity=@0 and token=@1 where username=@1");
            tokenCommand.Parameters.AddWithValue("@0", newValidity);
            tokenCommand.Parameters.AddWithValue("@1", newtoken);
            tokenCommand.Parameters.AddWithValue("@2", adminUsername);

            return PerformSqlNonQuery(tokenCommand);
        }

        public static int RefreshToken(string adminUsername)
        {
            string newValidity = GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

            // refresh for another hour
            MySqlCommand tokenCommand = new MySqlCommand("UPDATE admins SET validity=@0 where username=@1");
            tokenCommand.Parameters.AddWithValue("@0", newValidity);
            tokenCommand.Parameters.AddWithValue("@1", adminUsername);

            return PerformSqlNonQuery(tokenCommand);
        }

        public static int PerformSqlNonQuery(MySqlCommand command)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(Constants.sConnectionString))
            {
                conn.Open();
                command.Connection = conn;
                result = command.ExecuteNonQuery();
                conn.Close();
            }

            return result;
        }

        public static MySqlDataReader PerformSqlQuery(MySqlCommand command)
        {
            MySqlConnection conn = new MySqlConnection(Constants.sConnectionString);
            conn.Open();
            command.Connection = conn;
            return command.ExecuteReader();
        }

        public static void CleanUp(MySqlDataReader reader, MySqlCommand command)
        {
            reader.Close();
            command.Connection.Close();
        }

        public static MySqlCommand GenerateQueryCmd(string tableName, Dictionary<string, string> queryParam)
        {
            // "SELECT * FROM masteradmins where username=@0 and password=@1"
            string query = string.Format("SELECT * FROM {0} where ", tableName);

            MySqlCommand command = new MySqlCommand();

            int count = 0;
            foreach (KeyValuePair<string, string> entry in queryParam)
            {
                if(count != 0)
                {
                    query += "and ";
                }
                query += string.Format("{0}=@{1} ", entry.Key, count);
                command.Parameters.AddWithValue(string.Format("@{0}", count), entry.Value);

                count++;
            }

            command.CommandText = query;
            return command;
        }

        public static MySqlCommand GenerateAddCmd(string tableName, Dictionary<string, string> insertParam)
        {
            MySqlCommand command = new MySqlCommand();

            string parameters = "";
            string values = "";
            int count = 0;
            foreach(KeyValuePair < string, string> entry in insertParam)
            {
                if(count != 0)
                {
                    parameters += ",";
                    values += ",";
                }

                parameters += entry.Key;
                values += string.Format("@{0}", count);
                command.Parameters.AddWithValue(string.Format("@{0}", count), entry.Value);

                count++;
            }


            string query = string.Format("INSERT into {0} ({1}) values ({2})", tableName, parameters, values);
            command.CommandText = query;

            return command;
        }

        public static MySqlCommand GenerateEditCmd(string tableName, Dictionary<string, string> updateParam, Dictionary<string, string> destinationParam)
        {
            MySqlCommand command = new MySqlCommand();

            string query = string.Format("UPDATE {0} SET ", tableName);

            int count = 0;
            foreach (KeyValuePair<string, string> entry in updateParam)
            {
                if(count != 0)
                {
                    query += ",";
                }
                query += string.Format("{0}=@{1}", entry.Key, count);
                command.Parameters.AddWithValue(string.Format("@{0}", count), entry.Value);

                count++;
            }

            query += " where ";
            int nextcount = 100;
            foreach(KeyValuePair<string, string> entry in destinationParam)
            {
                if(nextcount != 100)
                {
                    query += " and ";
                }
                query += string.Format("{0}=@{1}", entry.Key, nextcount);
                command.Parameters.AddWithValue(string.Format("@{0}", nextcount), entry.Value);

                nextcount++;
            }

            command.CommandText = query;

            return command;
        }

        public static MySqlCommand GenerateRemoveCmd(string tableName, Dictionary<string, string> removeParam)
        {
            string query = string.Format("DELETE from {0} where ", tableName);

            MySqlCommand command = new MySqlCommand();

            int count = 0;
            foreach(KeyValuePair<string, string> entry in removeParam)
            {
                if (count != 0)
                {
                    query += " and ";
                }

                query += string.Format("{0}=@{1}", entry.Key, count);
                command.Parameters.AddWithValue(string.Format("@{0}", count), entry.Value);
                count++;
            }

            command.CommandText = query;

            return command;
        }
    }
}