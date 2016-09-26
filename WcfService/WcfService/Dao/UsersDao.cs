﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class UsersDao : BaseDao
    {
        private readonly string TABLE_USERS = "users";
        private readonly string TABLE_USER_COMPANY = "user_company";
        private readonly string TABLE_USER_DEVICE = "user_device";
        private readonly string TABLE_USER_ROLE = "user_role";
        private readonly string TABLE_SESSION = "user_session";

        public Model.User GetUserById(string userId)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> commandDic = new Dictionary<string, string>();
                commandDic.Add("id", userId);

                command = GenerateQueryCmd(TABLE_USERS, commandDic);
                reader = PerformSqlQuery(command);
                if(false == reader.Read())
                {
                    return null;
                }

                return generateUserObj(reader);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }

        public Model.User GetUserByUsername(string username, bool password=false)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> commandDic = new Dictionary<string, string>();
                commandDic.Add("username", username);

                command = GenerateQueryCmd(TABLE_USERS, commandDic);
                reader = PerformSqlQuery(command);
                if (false == reader.Read())
                {
                    return null;
                }

                return generateUserObj(reader, password);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }

        public List<Model.User> GetCompanyUserByRole(string companyId, string roleId)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.* FROM {0} " +
                    "INNER JOIN {1} ON {1}.user_id={0}.id " +
                    "INNER JOIN {3} ON {3}.user_id={0}.id " +
                    "WHERE company_id={2} and deleted=0 and enabled=1 and role_id={4} ORDER BY creation_date DESC;",
                    TABLE_USERS, TABLE_USER_COMPANY, companyId, TABLE_USER_ROLE, roleId);
                command = new MySqlCommand(query);
                reader = PerformSqlQuery(command);

                List<Model.User> userList = new List<Model.User>();
                while (reader.Read())
                {
                    userList.Add(generateUserObj(reader));
                }

                return userList;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }

        public List<Model.User> GetUserByCompanyId(string companyId, string limit, string skip)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.* FROM {0} " +
                    "INNER JOIN {1} ON {1}.company_id={2} AND {1}.user_id={0}.id " +
                    "WHERE deleted=0 ORDER BY creation_date DESC ",
                    TABLE_USERS, TABLE_USER_COMPANY, companyId);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                command = new MySqlCommand(query);
                reader = PerformSqlQuery(command);

                List<Model.User> userList = new List<Model.User>();
                while (reader.Read())
                {
                    userList.Add(generateUserObj(reader));
                }

                return userList;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }

        public List<Model.User> GetAllUsers(string limit, string skip)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM {0} WHERE deleted=0 ORDER BY creation_date DESC ", TABLE_USERS);
                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                command = new MySqlCommand(query);
                reader = PerformSqlQuery(command);

                List<Model.User> userList = new List<Model.User>();
                while (reader.Read())
                {
                    userList.Add(generateUserObj(reader));
                }

                return userList;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }

        private Model.User generateUserObj(MySqlDataReader reader, bool password=false)
        {
            Model.User user = new Model.User();

            user.userId = reader["id"].ToString();
            user.username = (string)reader["username"];
            if (password)
            {
                user.password = (string)reader["password"];
            }

            user.displayName = (string)reader["display_name"];
            user.identityCard = (string)reader["identity_card"];
            user.image = (string)reader["image"];
            user.contactNumber = (string)reader["contact"];
            user.email = (string)reader["email"];
            user.enabled = (int)reader["enabled"] == 0 ? false : true;
            user.deleted = (int)reader["deleted"] == 0 ? false : true;
            user.creationDate = reader["creation_date"].ToString();
            user.lastModifiedDate = reader["last_modified_date"].ToString();

            return user;
        }

        /// <summary>
        /// Does not support change company, and change contact number
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(string userId, Model.User user)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> updateParam = new Dictionary<string, string>();
                updateParam.Add("display_name", user.displayName);
                updateParam.Add("identity_card", user.identityCard);
                updateParam.Add("image", user.image);
                updateParam.Add("email", user.email);

                Dictionary<string, string> destinationParam = new Dictionary<string, string>();
                destinationParam.Add("id", userId);

                mySqlCmd = GenerateEditCmd(TABLE_USERS, updateParam, destinationParam);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return false;
        }

        public bool UpdatePassword(string userId, string newPassword)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> updateParam = new Dictionary<string, string>();
                updateParam.Add("password", newPassword);

                Dictionary<string, string> destinationParam = new Dictionary<string, string>();
                destinationParam.Add("id", userId);

                mySqlCmd = GenerateEditCmd(TABLE_USERS, updateParam, destinationParam);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return false;
        }

        /// <summary>
        /// Soft delete
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(string userId)
        {
            MySqlCommand mySqlCmd = null;
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", userId);

                mySqlCmd = GenerateSoftDelete(TABLE_USERS, removeParams);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(null, mySqlCmd);
            }

            return false;
        }

        public string AddUser(Model.User user)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                // add to user table
                string query = string.Format("INSERT INTO {0} (username, password, contact, display_name, identity_card, image, email) VALUES (@username, @password, @contact, @display_name, @identity_card, @image, @email) " +
                    "ON DUPLICATE KEY UPDATE id= LAST_INSERT_ID(id), contact=@contact, display_name=@display_name, email=@email;",
                    TABLE_USERS);

                mySqlCmd = new MySqlCommand(query);
                mySqlCmd.Parameters.AddWithValue("@username", user.username);
                mySqlCmd.Parameters.AddWithValue("@password", user.password == null? "": user.password);
                mySqlCmd.Parameters.AddWithValue("@contact", user.contactNumber);
                mySqlCmd.Parameters.AddWithValue("@display_name", user.displayName);
                mySqlCmd.Parameters.AddWithValue("@identity_card", user.identityCard == null ? "": user.identityCard);
                mySqlCmd.Parameters.AddWithValue("@image", user.image == null?"": user.image);
                mySqlCmd.Parameters.AddWithValue("@email", user.email);

                PerformSqlNonQuery(mySqlCmd);
                var userId = mySqlCmd.LastInsertedId.ToString();

                // add to user company table
                Dictionary<string, string> addParam = new Dictionary<string, string>();
                if (user.companyId != null)
                {
                    CleanUp(reader, mySqlCmd);

                    addParam.Clear();
                    addParam.Add("user_id", userId);
                    addParam.Add("company_id", user.companyId);
                    mySqlCmd = GenerateAddCmd(TABLE_USER_COMPANY, addParam);
                    PerformSqlNonQuery(mySqlCmd);
                }

                if (user.roleId != null)
                {
                    CleanUp(reader, mySqlCmd);

                    // add to user role table
                    addParam.Clear();
                    addParam.Add("user_id", userId);
                    addParam.Add("role_id", user.roleId);
                    mySqlCmd = GenerateAddCmd(TABLE_USER_ROLE, addParam);
                    PerformSqlNonQuery(mySqlCmd);
                }

                return userId;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public bool InsertOrUpdateToken(string userId, string newToken, string newValidity)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("INSERT INTO {0} (user_id, token, valid_till) VALUES (@userId, @newToken, @newValidity) " +
                    "ON DUPLICATE KEY UPDATE token=@newToken, valid_till=@newValidity;",
                    TABLE_SESSION);

                mySqlCmd = new MySqlCommand(query);
                mySqlCmd.Parameters.AddWithValue("@userId", userId);
                mySqlCmd.Parameters.AddWithValue("@newToken", newToken);
                mySqlCmd.Parameters.AddWithValue("@newValidity", newValidity);

                return (0 != PerformSqlNonQuery(mySqlCmd));
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return false;
        }

        public bool InsertOrUpdateDevice(string userId, string identifier)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("INSERT INTO {0} (user_id, identifier) VALUES (@userId, @identifier) " +
                    "ON DUPLICATE KEY UPDATE user_id=@userId;",
                    TABLE_USER_DEVICE);

                mySqlCmd = new MySqlCommand(query);
                mySqlCmd.Parameters.AddWithValue("@userId", userId);
                mySqlCmd.Parameters.AddWithValue("@identifier", identifier);

                return (0 != PerformSqlNonQuery(mySqlCmd));
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return false;
        }
    }
}