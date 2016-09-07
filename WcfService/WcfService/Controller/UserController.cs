using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Dao;
using WcfService.Model;
using WcfService.Utility;

namespace WcfService.Controller
{
    public class UserController: BaseController
    {
        public Response Login(string username, string password)
        {
            // verify username and password
            User user = userDao.GetUserByUsername(username);

            if(user == null)
            {
                // user not found
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EUserNotFound);
                return response;
            }

            if(user.password.CompareTo(password) != 0)
            {
                // invalid user
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.ECredentialError);
                return response;
            }

            if(user.enabled)
            {
                // account disabled
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EAccountDisabled);
                return response;
            }

            if(user.deleted)
            {
                // account deleted
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EAccountDeleted);
                return response;
            }

            // get the company details
            var companyDetails = companyDao.GetCompany(user.companyId);
            if(companyDetails == null ||
                companyDetails.enabled == false ||
                companyDetails.deleted)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EAccountDeleted);
                return response;
            }


            // 2. update the token info
            string newToken = Guid.NewGuid().ToString();
            string newValidity = commonDao.GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

            if (false == userDao.UpdateToken(user.userId, newToken, newValidity))
            {
                // invalid error
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EUnknownError);
                return response;
            }

            // return user details
            Model.Token tokenPayload = new Token()
            {
                userId = user.userId,
                token = newToken,
                validTill = newValidity
            };

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            response.payload = javaScriptSerializer.Serialize(tokenPayload);
            return response;
        }

        public Response AddUser(Model.User user)
        {
            var userId = "";
            var result = userDao.AddUser(user, out userId);
            if(result != 0)
            {
                // invalid operation
                response = Utility.Utils.SetResponse(response, false, result);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(new Model.Token() { userId = userId });
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response EditUser(string userId, Model.User user)
        {
            user.userId = userId;
            if (false == userDao.UpdateUser(user))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteUser(string userId)
        {
            if (false == userDao.DeleteUser(userId))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetUser(string userId)
        {
            var result = userDao.GetUserById(userId);
            if(result == null)
            {
                response.payload = null;
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetUserList(string number, string skip)
        {
            var result = userDao.GetAllUsers(number, skip);
            if(result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdatePassword(string userId, string oldPw, string newPw)
        {
            var userData = userDao.GetUserById(userId);
            if(userData.password.CompareTo(oldPw) != 0)
            {
                // old password not match
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EUserPasswordError);
                return response;
            }

            if (false ==  userDao.UpdatePassword(userId, newPw))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdatePwWithOtp(string userId, string otp, string newPw)
        {
            var lastOtp = otpDao.Get(userId);
            if(lastOtp == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EInvalidOtp);
                return response;
            }

            if(lastOtp.otpCode.CompareTo(otp) != 0)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EInvalidOtp);
                return response;
            }

            TimeSpan ts = DateTime.UtcNow - DateTime.Parse(lastOtp.creationDate);
            if (ts.Minutes > 5)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EOtpExpired);
                return response;
            }

            // update the password
            if (false == userDao.UpdatePassword(userId, newPw))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // disable the pin
            otpDao.DisableAll(userId);

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdatePwWithRefNum(string userId, string refNum, string newPw)
        {
            var lastOtp = otpDao.Get(userId);
            if (lastOtp == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EInvalidOtp);
                return response;
            }

            if (lastOtp.refNumber.CompareTo(refNum) != 0)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EInvalidRefNum);
                return response;
            }

            TimeSpan ts = DateTime.UtcNow - DateTime.Parse(lastOtp.creationDate);
            if (ts.Minutes > 5)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EOtpExpired);
                return response;
            }

            // update the password
            if (false == userDao.UpdatePassword(userId, newPw))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // disable the pin
            otpDao.DisableAll(userId);

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response ForgotPassword(string userId)
        {
            // check if the previous OTP is within time limit
            var lastOtp = otpDao.Get(userId);
            if(lastOtp != null)
            {
                TimeSpan ts = DateTime.UtcNow - DateTime.Parse(lastOtp.creationDate);
                if(ts.TotalMinutes < 5)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.ERetryTime);
                    return response;
                }
            }

            // disable all the previous OTP
            if(false == otpDao.DisableAll(userId))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // add new otp to system
            string newOtp = new Random().Next(100000, 999999).ToString();
            string newRefNum = Guid.NewGuid().ToString();
            if (false == otpDao.Add(userId, newOtp, newRefNum))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            // send to user's handphone
            var userData = userDao.GetUserById(userId);
            var responseMsg = string.Format("Just Supply Chain Berhad.%0AYour OTP is: {0}. This OTP valid for 5 minutes.", newOtp);
            UtilSms.SendSms(userData.contactNumber, responseMsg);

            // TODO: generate email and send to user's email

            response.success = true;
            response.errorCode = Constant.ErrorCode.ESuccess;
            response.errorMessage = "Your temporary password was sent to your registered mobile phone.";

            return response;
        }

        public Response UpdateDeviceIdentifier(string userId, string newIdentifier)
        {
            if(false == userDao.AddOrUpdateDevice(userId, newIdentifier))
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public bool UpdateUserToken(string userId)
        {
            string newToken = Guid.NewGuid().ToString();
            string newValidity = commonDao.GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

            return userDao.UpdateToken(userId, newToken, newValidity);
        }
    }
}