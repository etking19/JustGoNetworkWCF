using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class FleetTypes
    {
        private static string sDatabaseName = "fleet_types";

        public string GetFleetTypes()
        {
            try
            {
                List<Constants.FleetType> fleetTypeList = new List<Constants.FleetType>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            fleetTypeList.Add(new Constants.FleetType()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Capacity = (int)reader["capacity"],
                                Type = (string)reader["type"]
                            });
                        }
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = fleetTypeList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string AddFleetType(string name, int capacity, string design)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);
                insertParams.Add("capacity", capacity.ToString());
                insertParams.Add("type", design);

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

        public string EditFleetType(int fleetId, string name, int capacity, string design)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("name", name);
                insertParams.Add("capacity", capacity.ToString());
                insertParams.Add("design", design);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", fleetId.ToString());

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

        public string DeleteFleetType(int fleetId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", fleetId.ToString());

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

        public string DeleteFleetTypes(int[] fleetIds)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                foreach (int fleetId in fleetIds)
                {
                    removeParams.Add("id", fleetId.ToString());
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

        public Constants.FleetType GetFleetTypeById(int fleetTypeId)
        {
            try
            {
                Constants.FleetType fleetType = new Constants.FleetType();

                Dictionary<string, string> queryParam = new Dictionary<string, string>();
                queryParam.Add("id", fleetTypeId.ToString());

                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParam))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        if (reader.Read())
                        {
                            fleetType.Id = (int)reader["id"];
                            fleetType.Name = (string)reader["name"];
                            fleetType.Capacity = (int)reader["capacity"];
                            fleetType.Type = (string)reader["type"];
                        }
                    }
                }

                return fleetType;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}