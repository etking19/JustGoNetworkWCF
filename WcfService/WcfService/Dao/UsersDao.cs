using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class UsersDao : BaseDao
    {
        private static string sDatabaseName = "users";

        public Model.User GetUserById(string userId)
        {
            Model.User user = new Model.User();

            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> commandDic = new Dictionary<string, string>();
                commandDic.Add("id", userId);
                command = GenerateQueryCmd(sDatabaseName, commandDic);
                reader = PerformSqlQuery(command);
                if(false == reader.Read())
                {
                    return null;
                }

                user.userId = (string)reader["id"];
                user.username = (string)reader["username"];
                user.password = (string)reader["password"];
                user.displayName = (string)reader["display_name"];
                user.identityCard = (string)reader["identity_card"];
                user.image = (string)reader["image"];
                user.contactNumber = (string)reader["contact"];
                user.email = (string)reader["email"];
                user.enabled = (int)reader["enabled"] == 0 ? false: true;
                user.deleted = (int)reader["deleted"] == 0 ? false: true;
                user.creationDate = reader["creation_date"].ToString();
                user.lastModifiedDate = reader["last_modified_date"].ToString();
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, command);
            }

            return user;
        }

        public Model.User GetUserByUsername(string username)
        {
            Model.User user = new Model.User();

            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> commandDic = new Dictionary<string, string>();
                commandDic.Add("username", username);
                command = GenerateQueryCmd(sDatabaseName, commandDic);
                reader = PerformSqlQuery(command);
                if (false == reader.Read())
                {
                    return null;
                }

                user.userId = (string)reader["id"];
                user.username = (string)reader["username"];
                user.password = (string)reader["password"];
                user.displayName = (string)reader["display_name"];
                user.identityCard = (string)reader["identity_card"];
                user.image = (string)reader["image"];
                user.contactNumber = (string)reader["contact"];
                user.email = (string)reader["email"];
                user.enabled = (int)reader["enabled"] == 0 ? false : true;
                user.deleted = (int)reader["deleted"] == 0 ? false : true;
                user.creationDate = reader["creation_date"].ToString();
                user.lastModifiedDate = reader["last_modified_date"].ToString();
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);
                return null;
            }
            finally
            {
                CleanUp(reader, command);
            }

            return user;
        }

        public List<Model.User> GetAllUsers(string number, string skip)
        {
            List<Model.User> userList = new List<Model.User>();

            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                command = GenerateQueryCmdWithLimit(sDatabaseName, number, skip);
                reader = PerformSqlQuery(command);
                while (reader.Read())
                {
                    Model.User user = new Model.User();

                    user.userId = (string)reader["id"];
                    user.username = (string)reader["username"];
                    user.password = (string)reader["password"];
                    user.displayName = (string)reader["display_name"];
                    user.identityCard = (string)reader["identity_card"];
                    user.image = (string)reader["image"];
                    user.contactNumber = (string)reader["contact"];
                    user.email = (string)reader["email"];
                    user.enabled = (int)reader["enabled"] == 0 ? false : true;
                    user.deleted = (int)reader["deleted"] == 0 ? false : true;
                    user.creationDate = reader["creation_date"].ToString();
                    user.lastModifiedDate = reader["last_modified_date"].ToString();

                    userList.Add(user);
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, command);
            }

            return userList;
        }

        public bool UpdateUser(Model.User user)
        {
            return true;
        }

        public bool UpdatePassword(string userId, string newPassword)
        {
            return true;
        }

        public bool DeleteUser(string userId)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newUserId"></param>
        /// <returns>0 indicate success, else error code</returns>
        public int AddUser(Model.User user, out string newUserId)
        {
            newUserId = "1";
            return 0;
        }

        public bool UpdateToken(string userId, string newToken, string newValidity)
        {
            return true;
        }

        public bool AddOrUpdateDevice(string userId, string identifier)
        {
            return true;
        }
    }
}