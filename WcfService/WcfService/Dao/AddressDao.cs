using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class AddressDao : BaseDao
    {
        public enum EType
        {
            From = 0,
            To = 1
        }

        private readonly string TABLE_ADD = "addresses";
        private readonly string TABLE_JOB_FROM = "job_from";
        private readonly string TABLE_JOB_TO = "job_to";

        public string Add(Model.Address payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.Address> Get(string userId, string limit, string skip, EType type)
        {
            return null;
        }

        public Model.Address Get(string id)
        {
            return null;
        }

        public bool Update(string id, Model.Address payload)
        {
            return true;
        }

        public string Add(Model.Address payload, string jobId, string customerName, string contact, EType type)
        {
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                var destinationTable = (type == EType.From ? TABLE_JOB_FROM : TABLE_JOB_TO);
                string query = string.Format("INSERT into {0} (add_1, add_2, add_3, state_id, country_id, postcode, gps_longitude, gps_latitude, create_by)" +
                    "VALUES (@add_1, @add_2, @add_3, @state_id, @country_id, @postcode, @gps_longitude, @gps_latitude, @create_by); " +
                    "INSERT into {1} (address_id, job_id, customer_name, customer_contact) " +
                    "VALUES (LAST_INSERT_ID(), @job_id, @customer_name, @customer_contact)",
                    TABLE_ADD, destinationTable);

                command = new MySqlCommand(query);
                command.Parameters.AddWithValue("@add_1", payload.address1);
                command.Parameters.AddWithValue("@add_2", payload.address2);
                command.Parameters.AddWithValue("@add_3", payload.address3);
                command.Parameters.AddWithValue("@state_id", payload.stateId);
                command.Parameters.AddWithValue("@country_id", payload.countryId);
                command.Parameters.AddWithValue("@postcode", payload.postcode);
                command.Parameters.AddWithValue("@gps_longitude", payload.gpsLongitude);
                command.Parameters.AddWithValue("@gps_latitude", payload.gpsLatitude);
                command.Parameters.AddWithValue("@create_by", payload.createdBy);

                command.Parameters.AddWithValue("@job_id", jobId);
                command.Parameters.AddWithValue("@customer_name", customerName);
                command.Parameters.AddWithValue("@customer_contact", contact);

                return PerformSqlNonQuery(command).ToString();
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, command);
            }

            return null;
        }
    }
}