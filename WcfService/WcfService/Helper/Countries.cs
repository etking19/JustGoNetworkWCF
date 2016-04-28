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
            try
            {
                List<Constants.Country> countriesList = new List<Constants.Country>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while(reader.Read())
                        {
                            countriesList.Add(new Constants.Country()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"]
                            });
                        }
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = countriesList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }

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
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);

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

        public string EditCountry(int countryId, string name)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", countryId.ToString());

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

        public string DeleteCountry(int countryId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", countryId.ToString());

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

        public string DeleteCountries(int[] countryIds)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                foreach(int countryId in countryIds)
                {
                    removeParams.Add("id", countryId.ToString());
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