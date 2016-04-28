using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Helper
{
    public class General
    {
        public string Login(string username, string password)
        {
            try
            {
                string passwordOut;
                Constants.Admin adminObj = new Admins().GetAdmin(username, out passwordOut);
                if(adminObj == null)
                {
                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginCredential });
                }

                if(String.Compare(passwordOut, password) != 0)
                {
                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginCredential });
                }

                if(adminObj.Enabled == false)
                {
                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended });
                }

                if(adminObj.Company.Enabled == false)
                {
                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginAccSuspended });
                }

                // generate the token for this user
                string newToken = Guid.NewGuid().ToString();
                string newValidity = Utils.GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

                // update the last login date
                Dictionary<string, string> destParam = new Dictionary<string, string>();
                destParam.Add("username", username);

                Dictionary<string, string> updateParams = new Dictionary<string, string>();
                updateParams.Add("last_login_date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                updateParams.Add("token", newToken);
                updateParams.Add("validity", DateTime.UtcNow.AddHours(Configuration.TOKEN_VALID_HOURS).ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlCommand command = Utils.GenerateEditCmd("admins", updateParams, destParam);
                Utils.PerformSqlNonQuery(command);

                // update the object
                adminObj.Token = newToken;
                adminObj.TokenValidity = newValidity;
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess, Payload = adminObj });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                // verify the old password
                string passwordOut;
                Constants.Admin adminObj = new Admins().GetAdmin(username, out passwordOut);
                if(String.Compare(passwordOut, oldPassword) != 0)
                {
                    return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginOldPWNotMatched });
                }

                // change the new password
                Dictionary<string, string> destParam = new Dictionary<string, string>();
                destParam.Add("username", username);

                Dictionary<string, string> updateParams = new Dictionary<string, string>();
                updateParams.Add("password", newPassword);

                MySqlCommand command = Utils.GenerateEditCmd("admins", updateParams, destParam);
                Utils.PerformSqlNonQuery(command);

                // send notification
                UtilSms.SendSms(username, "Just Logistic Berhad.%0A" + "Your password has changed. Please contact administrator if you did not perform this action.");

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }

        }

        public string ForgotPassword(string username)
        {
            try
            {
                // check if the last request is within time limit
                Dictionary<string, string> queryParam = new Dictionary<string, string>();
                queryParam.Add("username", username);

                MySqlCommand command = Utils.GenerateQueryCmd("admins", queryParam);
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    TimeSpan ts = DateTime.UtcNow - DateTime.Parse(reader["last_pw_request"].ToString());
                    if (ts.TotalMinutes < 20)
                    {
                        return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.ELoginPasswordRequestErr, ErrorMessage = "Please request after 20 minutes." });
                    }
                }

                // change the new password
                Dictionary<string, string> destParam = new Dictionary<string, string>();
                destParam.Add("username", username);

                string newPassword = new Random().Next(100000, 999999).ToString();
                Dictionary<string, string> updateParams = new Dictionary<string, string>();
                updateParams.Add("password", newPassword);
                updateParams.Add("last_pw_request", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                command = Utils.GenerateEditCmd("admins", updateParams, destParam);
                Utils.PerformSqlNonQuery(command);

                // send the updated password to user's phone
                UtilSms.SendSms(username, "Just Logistic Berhad.%0A" + "Your new password is: " + newPassword);

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }
    }
}