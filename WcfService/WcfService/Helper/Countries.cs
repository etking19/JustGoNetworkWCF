using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Countries
    {
        private static string sDatabaseName = "countries";

        public string GetCountries()
        {
            throw new NotImplementedException();
        }

        public Constants.Country GetCountry(int countryId)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", countryId.ToString());

                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    throw new Exception();
                }

                Constants.Country countryObj = new Constants.Country()
                {
                    Id = countryId,
                    Name = (string)reader["name"]
                };

                Utils.CleanUp(reader, command);
                return countryObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddCountry(string name)
        {
            throw new NotImplementedException();
        }

        public string EditCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public string DeleteCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public string DeleteCountries(int[] countryIds)
        {
            throw new NotImplementedException();
        }
    }
}