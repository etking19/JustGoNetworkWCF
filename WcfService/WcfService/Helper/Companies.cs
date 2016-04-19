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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string EditCompany(int companyId, string name, string address1, string address2, string poscode, int stateId, int countryId, string ssm)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public string RemoveCompanies(int[] companyIds)
        {
            throw new NotImplementedException();
        }
    }
}