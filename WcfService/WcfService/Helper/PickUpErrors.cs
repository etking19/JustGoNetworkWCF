using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class PickUpErrors
    {
        private static string sDatabaseName = "pick_up_error";

        public Constants.PickUpError GetPickUpErrorById(int id)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", id.ToString());

                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        if (reader.Read())
                        {
                            return new Constants.PickUpError()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"]
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


            return null;
        }

        public List<Constants.PickUpError> GetPickUpErrorsList()
        {
            List<Constants.PickUpError> pickUpErrorList = new List<Constants.PickUpError>();

            using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    while (reader.Read())
                    {
                        pickUpErrorList.Add( new Constants.PickUpError()
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"]
                        });
                    }
                }
            }

            return pickUpErrorList;
        }

        public string GetPickUpErrors()
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = GetPickUpErrorsList() });
        }
    }
}