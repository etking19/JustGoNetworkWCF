using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Dao;
using WcfService.Model;

namespace WcfService.Controller
{
    public class UserController: BaseController
    {
        public Response Login(string username, string password)
        {
            // verify username and password
            User user = userDao.GetUserByUsername(username);

            if(user == null ||
                user.password.CompareTo(password) != 0)
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


            // 2. update the token info
            string newToken = Guid.NewGuid().ToString();
            string newValidity = Utils.GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

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
            response.payload = tokenPayload;
            return response;
        }

        public Response AddUser(Model.User user)
        {
            response.payload = new Model.Token() { userId = "1234" };
            return response;
        }

        public Response EditUser(string userId, Model.User user)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteUser(string userId)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetUser(string userId)
        {
            response.payload = userDao.GetUserById(userId);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetUserList(string number, string skip)
        {
            response.payload = userDao.GetAllUsers(number, skip);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdatePassword(string userId, string oldPw, string newPw)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response ForgotPassword(string userId)
        {
            response.success = true;
            response.errorCode = Constant.ErrorCode.ESuccess;
            response.errorMessage = "Your temporary password was sent to your registered mobile phone.";

            return response;
        }

        public Response UpdateDeviceIdentifier(string userId, string newIdentifier)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public bool UpdateUserToken(string userId)
        {
            string newToken = Guid.NewGuid().ToString();
            string newValidity = Utils.GetCurrentUtcTime(Configuration.TOKEN_VALID_HOURS);

            return userDao.UpdateToken(userId, newToken, newValidity);
        }
    }
}