using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Companies
    {
        private static string sDatabaseName = "companies";

        public string GetCompanies()
        {
            try
            {
                List<Constants.Company> companyList = new List<Constants.Company>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            companyList.Add(new Constants.Company()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Address1 = (string)reader["address_1"],
                                Address2 = (string)reader["address_2"],
                                Postcode = (string)reader["postcode"],
                                SSM = (string)reader["registration_number"],
                                Country = new Countries().GetCountry((int)reader["country_id"]),
                                State = new States().GetState((int)reader["state_id"]),
                                Enabled = (int)reader["enabled"] > 0 ? true : false
                            });
                        }
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = companyList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public Constants.Company GetCompany(int companyId)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", companyId.ToString());

                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    throw new Exception();
                }

                int countryId = (int)reader["country_id"];
                int stateId = (int)reader["state_id"];
                Constants.Company companyObj = new Constants.Company()
                {
                    Id = companyId,
                    Name = (string)reader["name"],
                    Address1 = (string)reader["address_1"],
                    Address2 = (string)reader["address_2"],
                    Postcode = (string)reader["postcode"],
                    SSM = (string)reader["registration_number"],
                    Country = new Countries().GetCountry((int)reader["country_id"]),
                    State = new States().GetState((int)reader["state_id"]),
                    Enabled = (int)reader["enabled"] > 0 ? true : false
                };

                Utils.CleanUp(reader, command);
                return companyObj;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddCompany(string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);
                insertParams.Add("address_1", address1);
                insertParams.Add("address_2", address2);
                insertParams.Add("postcode", poscode);
                insertParams.Add("stateId", stateId.ToString());
                insertParams.Add("countryId", countryId.ToString());
                insertParams.Add("registration_number", ssm);

                using (MySqlCommand command = Utils.GenerateAddCmd(sDatabaseName, insertParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string EnableCompany(int companyId, bool enabled)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("enabled", enabled? "1" : "0");

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", companyId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string EditCompany(int companyId, string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);
                insertParams.Add("address_1", address1);
                insertParams.Add("address_2", address2);
                insertParams.Add("postcode", poscode);
                insertParams.Add("stateId", stateId.ToString());
                insertParams.Add("countryId", countryId.ToString());
                insertParams.Add("registration_number", ssm);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", companyId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, insertParams, destParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string RemoveCompany(int companyId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", companyId.ToString());

                using (MySqlCommand command = Utils.GenerateRemoveCmd(sDatabaseName, removeParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string RemoveCompanies(int[] companyIds)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                foreach (int companyId in companyIds)
                {
                    removeParams.Add("id", companyId.ToString());
                }

                using (MySqlCommand command = Utils.GenerateRemoveCmd(sDatabaseName, removeParams))
                {
                    Utils.PerformSqlNonQuery(command);
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }
    }
}