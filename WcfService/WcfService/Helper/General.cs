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

                // update token and validity
                Utils.RefreshToken(username, newToken, newValidity);

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

                // send notification to user's phone
                UtilNotification.SendMessage(username, "Password Changed", "Your password has changed. Please contact administrator if you did not perform the changes.");

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }

        }

        public string ResetPassword(string username)
        {
            try
            {
                // change the new password
                Dictionary<string, string> destParam = new Dictionary<string, string>();
                destParam.Add("username", username);

                string newPassword = new Random().Next(100000, 999999).ToString();
                Dictionary<string, string> updateParams = new Dictionary<string, string>();
                updateParams.Add("password", newPassword);

                MySqlCommand command = Utils.GenerateEditCmd("admins", updateParams, destParam);
                Utils.PerformSqlNonQuery(command);

                // send the updated password to user's phone
                UtilSms.SendSms(username, "Your new password is " + newPassword + ". Please change the password after login.");

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, ErrorCode = ErrorCodes.ESuccess });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }
    }
}