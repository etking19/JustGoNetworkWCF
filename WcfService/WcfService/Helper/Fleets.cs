using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Fleets
    {
        private static string sDatabaseName = "fleets";

        public string GetFleetsByCompanyId(int companyId)
        {
            try
            {
                List<Constants.Fleet> fleetList = new List<Constants.Fleet>();

                Dictionary<string, string> destinationParams = new Dictionary<string, string>();
                destinationParams.Add("company_id", companyId.ToString());

                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, destinationParams))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            fleetList.Add(new Constants.Fleet()
                            {
                                Identifier = (string)reader["identifier"],
                                Company = new Companies().GetCompany((int)reader["company_id"]),
                                FleetType = new FleetTypes().GetFleetTypeById((int)reader["fleet_type_id"]),
                                Remarks = (string)reader["remarks"],
                                RoadTaxExpiry = reader["road_tax_expiry"].ToString(),
                                ServiceDueDate = reader["service_due_date"].ToString()
                            });
                        }
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = fleetList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string GetFleets()
        {
            try
            {
                List<Constants.Fleet> fleetList = new List<Constants.Fleet>();
                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            fleetList.Add(new Constants.Fleet()
                            {
                                Identifier = (string)reader["identifier"],
                                Company = new Companies().GetCompany((int)reader["company_id"]),
                                FleetType = new FleetTypes().GetFleetTypeById((int)reader["fleet_type_id"]),
                                Remarks = (string)reader["remarks"],
                                RoadTaxExpiry = reader["road_tax_expiry"].ToString(),
                                ServiceDueDate = reader["service_due_date"].ToString()
                            });
                        }
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = fleetList });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string AddFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("identifier", identifier);
                insertParams.Add("companyId", companyId.ToString());
                insertParams.Add("fleet_type_id", fleetTypeId.ToString());
                insertParams.Add("remarks", remarks);
                insertParams.Add("road_tax_expiry", roadTaxExpiry);
                insertParams.Add("service_due_date", serviceDueDate);
                insertParams.Add("creation_date", Utils.GetCurrentUtcTime(0));

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

        public string EditFleet(string identifier, int companyId, string remarks, int fleetTypeId, string roadTaxExpiry, string serviceDueDate)
        {
            try
            {
                Dictionary<string, string> insertParams = new Dictionary<string, string>();
                insertParams.Add("company_id", companyId.ToString());
                insertParams.Add("fleet_type_id", fleetTypeId.ToString());
                insertParams.Add("road_tax_expiry", roadTaxExpiry);
                insertParams.Add("service_due_date", serviceDueDate);
                insertParams.Add("remarks", remarks);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("identifier", identifier);

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

        public string DeleteFleet(string identifier)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("identifier", identifier);

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

        public string DeleteFleets(string[] identifier)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                foreach (string fleetId in identifier)
                {
                    removeParams.Add("identifier", fleetId);
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