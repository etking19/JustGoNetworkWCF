using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class CorporatePartners
    {
        private static string sDatabaseName = "corporate_partners";

        public List<Constants.CorporatePartnerAdmin> GetCorporatePartnersList()
        {
            try
            {
                List<Constants.CorporatePartnerAdmin> adminList = new List<Constants.CorporatePartnerAdmin>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            adminList.Add(new Constants.CorporatePartnerAdmin()
                            {
                                Id = (int)reader["id"],
                                UserId = (int)reader["user_id"],
                                UserObj = new Users().GetUser((int)reader["user_id"]),
                                CompanyId = (int)reader["company_id"],
                                CompanyObj = new Companies().GetCompany((int)reader["company_id"])
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

        public Constants.CorporatePartnerAdmin GetCorporatePartnerByUserId(int userId)
        {
            return GetCorporatePartnersList().Find(x => x.Id == userId);
        }

        public Constants.CorporatePartnerAdmin GetCorporatePartnerByCompanyId(int companyId)
        {
            return GetCorporatePartnersList().Find(x => x.Id == companyId);
        }

        public void AddCorporatePartner(int userId, int companyId)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("user_id", userId.ToString());
                insertParams.Add("company_id", companyId.ToString());

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

        public void RemoveCorporatePartner(int userId)
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

        public void EditCorporatePartner(int id, int userId, int companyId)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("user_id", userId.ToString());
                insertParams.Add("company_id", companyId.ToString());

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", id.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}