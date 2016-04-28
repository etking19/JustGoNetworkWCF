using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Drivers
    {
        private static string sDatabaseName = "drivers";

        public List<Constants.DriverAdmin> GetDriversList()
        {
            try
            {
                List<Constants.DriverAdmin> adminList = new List<Constants.DriverAdmin>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            adminList.Add(new Constants.DriverAdmin()
                            {
                                Id = (int)reader["id"],
                                UserId = (int)reader["user_id"],
                                UserObj = new Users().GetUser((int)reader["user_id"]),
                                CompanyId = (int)reader["company_id"],
                                CompanyObj = new Companies().GetCompany((int)reader["company_id"]),
                                Rating = (float)reader["rating"],
                                IdentityCard = (string)reader["identity_card"]
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


        public Constants.DriverAdmin GetDriverByUserId(int userId)
        {
            return GetDriversList().Find(x => x.Id == userId);
        }

        public Constants.DriverAdmin GetDriverByCompanyId(int companyId)
        {
            return GetDriversList().Find(x => x.Id == companyId);
        }

        public void AddDriver(int userId, int companyId, string identityCard)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("user_id", userId.ToString());
                insertParams.Add("company_id", companyId.ToString());
                insertParams.Add("identity_card", identityCard);

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

        public void RemoveDriver(int userId)
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

        public void EditDriver(int id, int userId, int companyId, float rating, string identityCard)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("user_id", userId.ToString());
                insertParams.Add("company_id", companyId.ToString());
                insertParams.Add("rating", rating.ToString());
                insertParams.Add("identity_card", identityCard);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", id.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }
            }
            catch (Exception )
            {
                
            }
        }
    }
}