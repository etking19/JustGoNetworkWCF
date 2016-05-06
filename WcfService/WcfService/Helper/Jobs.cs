using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class Jobs
    {
        private static string sDatabaseName = "jobs";

        public long AddJob(int companyId, string customerName, string customerContact, Constants.AddressInfo pickupInfo, Constants.AddressInfo deliveryInfo,
            string deliveryDatetime, float amount, int cashOnDelivery, int workerAssistance, string remarks)
        {
            Dictionary<string, string> insertParams = new Dictionary<string, string>();
            insertParams.Add("owner_company_id", companyId.ToString());
            insertParams.Add("customer_name", customerName);
            insertParams.Add("customer_contact", customerContact);

            insertParams.Add("pickup_add_1", pickupInfo.Address1);
            insertParams.Add("pickup_add_2", pickupInfo.Address2);
            insertParams.Add("pickup_postcode", pickupInfo.Postcode);
            insertParams.Add("pickup_state", pickupInfo.State.Id.ToString());
            insertParams.Add("pickup_country", pickupInfo.Country.Id.ToString());
            insertParams.Add("pickup_gps_latitude", pickupInfo.Latitude.ToString());
            insertParams.Add("pickup_gps_longitude", pickupInfo.Longitude.ToString());
            insertParams.Add("pick_up_customer_name", pickupInfo.Name);
            insertParams.Add("pick_up_customer_contact", pickupInfo.Contact);

            insertParams.Add("deliver_add_1", deliveryInfo.Address1);
            insertParams.Add("deliver_add_2", deliveryInfo.Address2);
            insertParams.Add("deliver_postcode", deliveryInfo.Postcode);
            insertParams.Add("deliver_state", deliveryInfo.State.Id.ToString());
            insertParams.Add("deliver_country", deliveryInfo.Country.Id.ToString());
            insertParams.Add("deliver_gps_latitude", deliveryInfo.Latitude.ToString());
            insertParams.Add("deliver_gps_longitude", deliveryInfo.Longitude.ToString());
            insertParams.Add("deliver_customer_name", deliveryInfo.Name);
            insertParams.Add("deliver_customer_contact", deliveryInfo.Contact);

            insertParams.Add("delivery_datetime", deliveryDatetime);
            insertParams.Add("job_remarks", remarks);
            insertParams.Add("amount", amount.ToString());
            insertParams.Add("cash_on_delivery", cashOnDelivery.ToString());
            insertParams.Add("workers_assistance", workerAssistance.ToString());
            insertParams.Add("creation_date", Utils.GetCurrentUtcTime(0));

            using (MySqlCommand command = Utils.GenerateAddCmd(sDatabaseName, insertParams))
            {
                Utils.PerformSqlNonQuery(command);

                return command.LastInsertedId;
            }
        }

        public string AddJob(int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {
            Constants.AddressInfo pickupInfo = new Constants.AddressInfo()
            {
                Address1 = pickupAdd1,
                Address2 = pickupAdd2,
                Postcode = pickupPoscode,
                State = new States().GetState(pickupStateId),
                Country = new Countries().GetCountry(pickupCountryId),
                Longitude = pickupLongitude,
                Latitude = pickupLatitude,
                Name = pickupCustomerName,
                Contact = pickupCustomerContact
            };

            Constants.AddressInfo deliveryInfo = new Constants.AddressInfo()
            {
                Address1 = deliverAdd1,
                Address2 = deliverAdd2,
                Postcode = deliverPoscode,
                State = new States().GetState(deliverStateId),
                Country = new Countries().GetCountry(deliverCountryId),
                Longitude = deliverLongitude,
                Latitude = deliverLatitude,
                Name = deliverCustomerName,
                Contact = deliverCustomerContact
            };

            long result = AddJob(companyId, customerName, customerContact, pickupInfo, deliveryInfo,
                deliveryDateTime, amount, cashOnDelivery?1:0, workerAssistance, remarks);

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = result });
        }

        public string EditJob(int id, int companyId, string customerName, string customerContact,
            string pickupCustomerName, string pickupCustomerContact, string pickupAdd1, string pickupAdd2,
            string pickupPoscode, int pickupStateId, int pickupCountryId, float pickupLongitude, float pickupLatitude,
            string deliverCustomerName, string deliverCustomerContact, string deliverAdd1, string deliverAdd2,
            string deliverPoscode, int deliverStateId, int deliverCountryId, float deliverLongitude, float deliverLatitude,
            string deliveryDateTime, float amount, bool cashOnDelivery, int workerAssistance, string remarks)
        {

            try
            {
                Dictionary<string, string> updateParams = new Dictionary<string, string>();
                updateParams.Add("owner_company_id", companyId.ToString());
                updateParams.Add("customer_name", customerName);
                updateParams.Add("customer_contact", customerContact);

                updateParams.Add("pickup_add_1", pickupAdd1);
                updateParams.Add("pickup_add_2", pickupAdd2);
                updateParams.Add("pickup_postcode", pickupPoscode);
                updateParams.Add("pickup_state", pickupStateId.ToString());
                updateParams.Add("pickup_country", pickupCountryId.ToString());
                updateParams.Add("pickup_gps_latitude", pickupLatitude.ToString());
                updateParams.Add("pickup_gps_longitude", pickupLongitude.ToString());
                updateParams.Add("pick_up_customer_name", pickupCustomerName);
                updateParams.Add("pick_up_customer_contact", pickupCustomerContact);

                updateParams.Add("deliver_add_1", deliverAdd1);
                updateParams.Add("deliver_add_2", deliverAdd2);
                updateParams.Add("deliver_postcode", deliverPoscode);
                updateParams.Add("deliver_state", deliverStateId.ToString());
                updateParams.Add("deliver_country", deliverCountryId.ToString());
                updateParams.Add("deliver_gps_latitude", deliverLatitude.ToString());
                updateParams.Add("deliver_gps_longitude", deliverLongitude.ToString());
                updateParams.Add("deliver_customer_name", deliverCustomerName);
                updateParams.Add("deliver_customer_contact", deliverCustomerContact);

                updateParams.Add("delivery_datetime", deliveryDateTime);
                updateParams.Add("job_remarks", remarks);
                updateParams.Add("amount", amount.ToString());
                updateParams.Add("cash_on_delivery", cashOnDelivery.ToString());
                updateParams.Add("workers_assistance", workerAssistance.ToString());
                updateParams.Add("creation_date", Utils.GetCurrentUtcTime(0));


                Dictionary<string, string> destinationParams = new Dictionary<string, string>();
                destinationParams.Add("id", id.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, updateParams, destinationParams))
                {
                    Utils.PerformSqlNonQuery(command);
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }

            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
        }

        public string RemoveJob(int jobId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", jobId.ToString());

                using (MySqlCommand command = Utils.GenerateRemoveCmd(sDatabaseName, removeParams))
                {
                    Utils.PerformSqlNonQuery(command);
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }


            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
        }

        public Constants.Job GetJobById(int jobId)
        {
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("id", jobId.ToString());

                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        if (reader.Read())
                        {
                            Constants.AddressInfo pickupInfo = new Constants.AddressInfo()
                            {
                                Name = (string)reader["pick_up_customer_name"],
                                Contact = (string)reader["pick_up_customer_contact"],
                                Address1 = (string)reader["pickup_add_1"],
                                Address2 = (string)reader["pickup_add_2"],
                                Postcode = (string)reader["pickup_postcode"],
                                State = new States().GetState((int)reader["pickup_state"]),
                                Country = new Countries().GetCountry((int)reader["pickup_country"]),
                                Longitude = (float)reader["pickup_gps_longitude"],
                                Latitude = (float)reader["pickup_gps_latitude"]
                            };

                            Constants.AddressInfo deliveryInfo = new Constants.AddressInfo()
                            {
                                Name = (string)reader["deliver_customer_nam"],
                                Contact = (string)reader["deliver_customer_contact"],
                                Address1 = (string)reader["deliver_add_1"],
                                Address2 = (string)reader["deliver_add_2"],
                                Postcode = (string)reader["deliver_postcode"],
                                State = new States().GetState((int)reader["deliver_state"]),
                                Country = new Countries().GetCountry((int)reader["deliver_country"]),
                                Longitude = (float)reader["deliver_gps_longitude"],
                                Latitude = (float)reader["deliver_gps_latitude"]
                            };

                            return new Constants.Job()
                            {
                                Id = (long)reader["id"],
                                CompanyId = (int)reader["owner_company_id"],
                                Company = new Companies().GetCompany((int)reader["owner_company_id"]),
                                Enabled = (int)reader["enabled"] == 0 ? false : true,
                                CustomerName = (string)reader["customer_name"],
                                CustomerContact = (string)reader["customer_contact"],
                                PickUpInfo = pickupInfo,
                                DeliveryInfo = deliveryInfo,
                                DeliveryDateTime = reader["delivery_datetime"].ToString(),
                                Amount = (float)reader["amount"],
                                CashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                                WorkerAssistance = (int)reader["workers_assistance"],
                                Remarks = (string)reader["job_remarks"]
                            };
                        }
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }


            return null;
        }

        public List<Constants.Job> GetJobListByCompanyId(int companyId)
        {

            List<Constants.Job> jobList = new List<Constants.Job>();
            try
            {
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                queryParams.Add("owner_company_id", companyId.ToString());

                using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName, queryParams))
                {
                    using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                    {
                        while (reader.Read())
                        {
                            Constants.AddressInfo pickupInfo = new Constants.AddressInfo()
                            {
                                Name = (string)reader["pick_up_customer_name"],
                                Contact = (string)reader["pick_up_customer_contact"],
                                Address1 = (string)reader["pickup_add_1"],
                                Address2 = (string)reader["pickup_add_2"],
                                Postcode = (string)reader["pickup_postcode"],
                                State = new States().GetState((int)reader["pickup_state"]),
                                Country = new Countries().GetCountry((int)reader["pickup_country"]),
                                Longitude = (float)reader["pickup_gps_longitude"],
                                Latitude = (float)reader["pickup_gps_latitude"]
                            };

                            Constants.AddressInfo deliveryInfo = new Constants.AddressInfo()
                            {
                                Name = (string)reader["deliver_customer_nam"],
                                Contact = (string)reader["deliver_customer_contact"],
                                Address1 = (string)reader["deliver_add_1"],
                                Address2 = (string)reader["deliver_add_2"],
                                Postcode = (string)reader["deliver_postcode"],
                                State = new States().GetState((int)reader["deliver_state"]),
                                Country = new Countries().GetCountry((int)reader["deliver_country"]),
                                Longitude = (float)reader["deliver_gps_longitude"],
                                Latitude = (float)reader["deliver_gps_latitude"]
                            };

                            jobList.Add( new Constants.Job()
                            {
                                Id = (long)reader["id"],
                                CompanyId = (int)reader["owner_company_id"],
                                Company = new Companies().GetCompany((int)reader["owner_company_id"]),
                                Enabled = (int)reader["enabled"] == 0 ? false : true,
                                CustomerName = (string)reader["customer_name"],
                                CustomerContact = (string)reader["customer_contact"],
                                PickUpInfo = pickupInfo,
                                DeliveryInfo = deliveryInfo,
                                DeliveryDateTime = reader["delivery_datetime"].ToString(),
                                Amount = (float)reader["amount"],
                                CashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                                WorkerAssistance = (int)reader["workers_assistance"],
                                Remarks = (string)reader["job_remarks"]
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


            return jobList;
        }
    }
}