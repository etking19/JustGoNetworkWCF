using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class States
    {
        private static string sDatabaseName = "states";

        public string GetStates(int countryId)
        {
            throw new NotImplementedException();
        }

        public Constants.State GetState(int stateId)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", stateId.ToString());
                MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams);
                MySqlDataReader reader = Utils.PerformSqlQuery(command);
                if (!reader.Read())
                {
                    Utils.CleanUp(reader, command);
                    throw new Exception();
                }

                Constants.State stateObj = new Constants.State()
                {
                    Id = stateId,
                    CountryId = (int)reader["country_id"],
                    Name = (string)reader["name"]
                };

                Utils.CleanUp(reader, command);
                return stateObj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddState(int countryId, string name)
        {
            throw new NotImplementedException();
        }

        public string EditState(int stateId, int countryId, string name)
        {
            throw new NotImplementedException();
        }

        public string DeleteState(int stateId)
        {
            throw new NotImplementedException();
        }

        public string DeleteStates(int[] stateIds)
        {
            throw new NotImplementedException();
        }
    }
}