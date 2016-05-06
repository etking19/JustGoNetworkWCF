using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class DeliveryErrors
    {
        private static string sDatabaseName = "delivery_error";

        public Constants.DeliveryError GetDeliveryErrorById(int id)
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
                            return new Constants.DeliveryError()
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

        public List<Constants.DeliveryError> GetDeliveryErrorsList()
        {
            List<Constants.DeliveryError> pickUpErrorList = new List<Constants.DeliveryError>();

            using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    while (reader.Read())
                    {
                        pickUpErrorList.Add(new Constants.DeliveryError()
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"]
                        });
                    }
                }
            }

            return pickUpErrorList;
        }

        public string GetDeliveryErrors()
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = GetDeliveryErrorsList() });
        }
    }
}