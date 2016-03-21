using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class Utils
    {
        public static string ValidateTokenNPermission(int adminId, string token, int expectedPermission)
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

        public static int ValidateToken(int adminId, string token, out int permission)
        {
            permission = 0;
            try
            {
                MySqlCommand command = new MySqlCommand(string.Format("SELECT * FROM masteradmins where id=@0 and token=@1", adminId, token));
                command.Parameters.AddWithValue("@0", adminId);
                command.Parameters.AddWithValue("@1", token);

                MySqlDataReader reader = PerformSqlQuery(command);
                if (reader.Read())
                {
                    // check if the time now still validate
                    // https://msdn.microsoft.com/en-us/library/system.datetime.compare(v=vs.110).aspx
                    int result = DateTime.UtcNow.CompareTo(reader["validity"]);
                    if (result < 0)
                    {
                        RefreshToken(adminId);
                        permission = (int)reader["permission"];
                        return ErrorCodes.ESuccess;
                    }
                    else
                    {
                        return ErrorCodes.ELoginExpired;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ErrorCodes.ELoginCredential;
        }

        public static string GetCurrentUtcTime(int hourOffset)
        {
            return DateTime.UtcNow.AddHours(hourOffset).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int RefreshToken(int adminId)
        {
            // refresh for another hour
            MySqlCommand tokenCommand = new MySqlCommand("UPDATE masteradmins SET validity=@0 where id=@1");
            tokenCommand.Parameters.AddWithValue("@0", GetCurrentUtcTime(1));
            tokenCommand.Parameters.AddWithValue("@1", adminId);

            return PerformSqlNonQuery(tokenCommand);
        }

        public static int PerformSqlNonQuery(MySqlCommand command)
        {
            MySqlConnection conn = new MySqlConnection(Constants.sConnectionString);
            conn.Open();
            command.Connection = conn;
            return command.ExecuteNonQuery();
        }

        public static MySqlDataReader PerformSqlQuery(MySqlCommand command)
        {
            MySqlConnection conn = new MySqlConnection(Constants.sConnectionString);
            conn.Open();
            command.Connection = conn;
            return command.ExecuteReader();
        }
    }
}