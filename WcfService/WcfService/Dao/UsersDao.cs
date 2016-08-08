using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class UsersDao : BaseDao
    {
        public Model.User GetUserById(string userId)
        {
            Model.User user = new Model.User()
            {
                userId = userId,
                username = "username",
                password = "password",
                displayName = "displayName"
            };

            return user;
        }

        public Model.User GetUserByUsername(string username)
        {
            Model.User user = new Model.User()
            {
                userId = "1",
                username = username,
                password = "password",
                displayName = "displayName"
            };

            return user;
        }

        public List<Model.User> GetAllUsers(string number, string skip)
        {
            List<Model.User> userList = new List<Model.User>();
            for(int i = 0; i < int.Parse(number); i++ )
            {
                userList.Add(new Model.User()
                {
                    userId = (i+int.Parse(skip)).ToString(),
                    username = "username" + i,
                    password = "password",
                    displayName = "displayName" + i
                });
            }

            return userList;
        }

        public List<Model.User> GetAllUsersByCompany(string companyId)
        {
            return null;
        }

        public List<Model.User> GetAllUsersByRole(string roleId)
        {
            return null;
        }

        public List<Model.User> GetAllUsers(string[] companyIds, string[] roleIds)
        {
            return null;
        }

        public bool UpdateUser(Model.User user)
        {
            return true;
        }

        public bool DeleteUser(string userId)
        {
            return true;
        }

        public string AddUser(Model.User user)
        {
            return "1";
        }

        public bool UpdateToken(string userId, string newToken, string newValidity)
        {
            return true;
        }
    }
}