using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class MasterAdmins
    {
        private static string sDatabaseName = "master_admins";

        public List<Constants.MasterAdmin> GetMasterAdminsList()
        {
            try
            {
                List<Constants.MasterAdmin> adminList = new List<Constants.MasterAdmin>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            adminList.Add(new Constants.MasterAdmin()
                            {
                                Id = (int)reader["id"],
                                UserId = (int)reader["user_id"],
                                UserObj = new Users().GetUser((int)reader["user_id"])
                            });
                        }
                    }
                }

                return adminList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Constants.MasterAdmin GetMasterAdmin(int userId)
        {
            return GetMasterAdminsList().Find(x => x.Id == userId);
        }

        public void AddMasterAdmin(int userId)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("user_id", userId.ToString());

                using (MySqlCommand command = Utils.GenerateAddCmd(sDatabaseName, insertParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }
            }
            catch (Exception)
            {
                // ignore error as it might happen without checking
            }
        }

        public void RemoveMasterAdmin(int userId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("user_id", userId.ToString());

                using (MySqlCommand command = Utils.GenerateRemoveCmd(sDatabaseName, removeParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }
            }
            catch (Exception)
            {
                // ignore error as it might happen without checking
            }
        }
    }
}