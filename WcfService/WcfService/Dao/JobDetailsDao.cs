using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class JobDetailsDao : BaseDao
    {
        private readonly string TABLE_NAME_JOB = "jobs";
        private readonly string TABLE_NAME_JOBDELIVERY = "job_delivery";
        private readonly string TABLE_NAME_ADDFROM = "job_from";
        private readonly string TABLE_NAME_ADDTO = "job_to";
        private readonly string TABLE_NAME_JOBORDERSTATUS = "job_order_status";
        private readonly string TABLE_NAME_ADDRESS = "addresses";

        public string AddOrder(string jobId, string modifyBy)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                // add to job order status
                Dictionary<string, string>  insertParam = new Dictionary<string, string>();
                insertParam.Add("job_id", jobId);
                insertParam.Add("modify_by", modifyBy);
                mySqlCmd = GenerateAddCmd(TABLE_NAME_JOBORDERSTATUS, insertParam);
                PerformSqlNonQuery(mySqlCmd);
 
                return mySqlCmd.LastInsertedId.ToString();
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public string Add(Model.JobDetails payload)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> insertParam = new Dictionary<string, string>();
                insertParam.Add("owner_id", payload.ownerUserId);
                insertParam.Add("job_type_id", payload.jobTypeId);
                insertParam.Add("amount", payload.amount.ToString());
                insertParam.Add("worker_assistance", payload.workerAsistance.ToString());
                insertParam.Add("remarks", payload.remarks.ToString());
                insertParam.Add("created_by", payload.createdBy);
                insertParam.Add("modify_by", payload.createdBy);

                mySqlCmd = GenerateAddCmd(TABLE_NAME_JOB, insertParam);
                PerformSqlNonQuery(mySqlCmd);

                return mySqlCmd.LastInsertedId.ToString();
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public bool Delete(string id)
        {
            MySqlCommand mySqlCmd = null;
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", id);

                mySqlCmd = GenerateSoftDelete(TABLE_NAME_JOB, removeParams);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            } 
            finally
            {
                CleanUp(null, mySqlCmd);
            }

            return false;
        }

        public Model.JobDetails GetByJobId(string jobId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM {0} " +
                    "INNER JOIN (SELECT {3}.add_1 as add_from_1, {3}.add_2 as add_from_2, {3}.add_3 as add_from_3, {3}.state_id as state_from, {3}.country_id as country_from, {3}.postcode as postcode_from, {3}.gps_longitude as longitude_from, {3}.gps_latitude as latitude_from, {1}.*, {1}.address_id as addFromId, {1}.customer_name as customerFrom, {1}.customer_contact as contactFrom FROM {1} INNER JOIN {3} ON {1}.address_id={3}.id) addFrom ON {0}.id=addFrom.job_id " +
                    "INNER JOIN (SELECT {3}.add_1 as add_to_1, {3}.add_2 as add_to_2, {3}.add_3 as add_to_3, {3}.state_id as state_to, {3}.country_id as country_to, {3}.postcode as postcode_to, {3}.gps_longitude as longitude_to, {3}.gps_latitude as latitude_to, {2}.*, {2}.address_id as addToId, {2}.customer_name as customerTo, {2}.customer_contact as contactTo FROM {2} INNER JOIN {3} ON {2}.address_id={3}.id) addTo ON {0}.id=addTo.job_id " +
                    "INNER JOIN {5} ON {5}.job_id={0}.id " + 
                    "WHERE {0}.deleted=0 AND {0}.id={4};", 
                    TABLE_NAME_JOB, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, TABLE_NAME_ADDRESS, jobId, TABLE_NAME_JOBORDERSTATUS);

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                Model.JobDetails result = null;
                List<Model.Address> fromAddList = new List<Model.Address>();
                List<Model.Address> toAddList = new List<Model.Address>();
                while (reader.Read())
                {
                    if(result == null)
                    {
                        result = new Model.JobDetails()
                        {
                            jobId = reader["id"].ToString(),
                            ownerUserId = reader["owner_id"].ToString(),
                            jobTypeId = reader["job_type_id"].ToString(),
                            amount = (float)reader["amount"],
                            amountPaid = (float)reader["amount_paid"],
                            cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                            workerAsistance = (int)reader["worker_assistance"],
                            remarks = (string)reader["remarks"],
                            enabled = (int)reader["enabled"] == 0 ? false : true,
                            deleted = (int)reader["deleted"] == 0 ? false : true,
                            createdBy = reader["created_by"].ToString(),
                            creationDate = reader["creation_date"].ToString(),
                            modifiedBy = reader["modify_by"].ToString(),
                            lastModifiedDate = reader["last_modified_date"].ToString(),
                            jobStatusId = reader["job_status_id"].ToString(),
                            addressFrom = new List<Model.Address>(),
                            addressTo = new List<Model.Address>()
                        };
                    }

                    result.addressFrom.Add(new Model.Address()
                    {
                        addressId = reader["addFromId"].ToString(),
                        address1 = (string)reader["add_from_1"],
                        address2 = (string)reader["add_from_2"],
                        address3 = (string)reader["add_from_3"],
                        stateId = reader["state_from"].ToString(),
                        countryId = reader["country_from"].ToString(),
                        postcode = (string)reader["postcode_from"],
                        gpsLongitude = (float)reader["longitude_from"],
                        gpsLatitude = (float)reader["latitude_from"],
                        contactPerson = (string)reader["customerFrom"],
                        contact = (string)reader["contactFrom"]
                    });

                    result.addressTo.Add(new Model.Address()
                    {
                        addressId = reader["addToId"].ToString(),
                        address1 = (string)reader["add_to_1"],
                        address2 = (string)reader["add_to_2"],
                        address3 = (string)reader["add_to_3"],
                        stateId = reader["state_to"].ToString(),
                        countryId = reader["country_to"].ToString(),
                        postcode = (string)reader["postcode_to"],
                        gpsLongitude = (float)reader["longitude_to"],
                        gpsLatitude = (float)reader["latitude_to"],
                        contactPerson = (string)reader["customerTo"],
                        contact = (string)reader["contactTo"]
                    });
                }

                return result;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public List<Model.JobDetails> GetByOwnerId(string ownerId, string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM (SELECT * FROM {0} WHERE deleted=0 AND owner_id={1} ORDER BY creation_date DESC ", TABLE_NAME_JOB, ownerId);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                query += ") jobDetails ";

                // job_from
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_from_1, {1}.add_2 as add_from_2, {1}.add_3 as add_from_3, {1}.state_id as state_from, {1}.country_id as country_from, {1}.postcode as postcode_from, {1}.gps_longitude as longitude_from, {1}.gps_latitude as latitude_from, {0}.*, {0}.address_id as addFromId, {0}.customer_name as customerFrom, {0}.customer_contact as contactFrom FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addFrom ON jobDetails.id=addFrom.job_id ",
                    TABLE_NAME_ADDFROM, TABLE_NAME_ADDRESS);

                // job to
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_to_1, {1}.add_2 as add_to_2, {1}.add_3 as add_to_3, {1}.state_id as state_to, {1}.country_id as country_to, {1}.postcode as postcode_to, {1}.gps_longitude as longitude_to, {1}.gps_latitude as latitude_to, {0}.*, {0}.address_id as addToId, {0}.customer_name as customerTo, {0}.customer_contact as contactTo FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addTo ON jobDetails.id=addTo.job_id ",
                    TABLE_NAME_ADDTO, TABLE_NAME_ADDRESS);

                // job order status
                query += string.Format("INNER JOIN {0} ON {0}.job_id=jobDetails.id ", TABLE_NAME_JOBORDERSTATUS);

                // reverse order
                query += "ORDER BY creation_date DESC;";


                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                return getDetailsList(reader);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public List<Model.JobDetails> Get(string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM (SELECT * FROM {0} WHERE deleted=0 ORDER BY creation_date DESC ", TABLE_NAME_JOB);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                query += ") jobDetails ";

                // job_from
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_from_1, {1}.add_2 as add_from_2, {1}.add_3 as add_from_3, {1}.state_id as state_from, {1}.country_id as country_from, {1}.postcode as postcode_from, {1}.gps_longitude as longitude_from, {1}.gps_latitude as latitude_from, {0}.*, {0}.address_id as addFromId, {0}.customer_name as customerFrom, {0}.customer_contact as contactFrom FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addFrom ON jobDetails.id=addFrom.job_id ",
                    TABLE_NAME_ADDFROM, TABLE_NAME_ADDRESS);

                // job to
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_to_1, {1}.add_2 as add_to_2, {1}.add_3 as add_to_3, {1}.state_id as state_to, {1}.country_id as country_to, {1}.postcode as postcode_to, {1}.gps_longitude as longitude_to, {1}.gps_latitude as latitude_to, {0}.*, {0}.address_id as addToId, {0}.customer_name as customerTo, {0}.customer_contact as contactTo FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addTo ON jobDetails.id=addTo.job_id ",
                    TABLE_NAME_ADDTO, TABLE_NAME_ADDRESS);

                // job order status
                query += string.Format("INNER JOIN {0} ON {0}.job_id=jobDetails.id ", TABLE_NAME_JOBORDERSTATUS);

                // reverse order
                query += "ORDER BY creation_date DESC;";


                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                return getDetailsList(reader);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        private List<Model.JobDetails> getDetailsList(MySqlDataReader reader)
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();
            while (reader.Read())
            {
                var jobId = reader["id"].ToString();
                var existedResult = jobDetailsList.Find(x => x.jobId == jobId);
                if (existedResult != null)
                {
                    // already have same job id
                    existedResult.addressFrom.Add(new Model.Address()
                    {
                        addressId = reader["addFromId"].ToString(),
                        address1 = (string)reader["add_from_1"],
                        address2 = (string)reader["add_from_2"],
                        address3 = (string)reader["add_from_3"],
                        stateId = reader["state_from"].ToString(),
                        countryId = reader["country_from"].ToString(),
                        postcode = (string)reader["postcode_from"],
                        gpsLongitude = (float)reader["longitude_from"],
                        gpsLatitude = (float)reader["latitude_from"],
                        contactPerson = (string)reader["customerFrom"],
                        contact = (string)reader["contactFrom"]
                    });

                    existedResult.addressTo.Add(new Model.Address()
                    {
                        addressId = reader["addToId"].ToString(),
                        address1 = (string)reader["add_to_1"],
                        address2 = (string)reader["add_to_2"],
                        address3 = (string)reader["add_to_3"],
                        stateId = reader["state_to"].ToString(),
                        countryId = reader["country_to"].ToString(),
                        postcode = (string)reader["postcode_to"],
                        gpsLongitude = (float)reader["longitude_to"],
                        gpsLatitude = (float)reader["latitude_to"],
                        contactPerson = (string)reader["customerTo"],
                        contact = (string)reader["contactTo"]
                    });

                    continue;
                }

                // new job id
                jobDetailsList.Add(new Model.JobDetails()
                {
                    jobId = jobId,
                    ownerUserId = reader["owner_id"].ToString(),
                    jobTypeId = reader["job_type_id"].ToString(),
                    amount = (float)reader["amount"],
                    amountPaid = (float)reader["amount_paid"],
                    cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                    workerAsistance = (int)reader["worker_assistance"],
                    remarks = (string)reader["remarks"],
                    enabled = (int)reader["enabled"] == 0 ? false : true,
                    deleted = (int)reader["deleted"] == 0 ? false : true,
                    createdBy = reader["created_by"].ToString(),
                    creationDate = reader["creation_date"].ToString(),
                    modifiedBy = reader["modify_by"].ToString(),
                    lastModifiedDate = reader["last_modified_date"].ToString(),
                    jobStatusId = reader["job_status_id"].ToString(),
                    addressFrom = new List<Model.Address>()
                    {
                        new Model.Address()
                        {
                            addressId = reader["addFromId"].ToString(),
                            address1 = (string)reader["add_from_1"],
                            address2 = (string)reader["add_from_2"],
                            address3 = (string)reader["add_from_3"],
                            stateId = reader["state_from"].ToString(),
                            countryId = reader["country_from"].ToString(),
                            postcode = (string)reader["postcode_from"],
                            gpsLongitude = (float)reader["longitude_from"],
                            gpsLatitude = (float)reader["latitude_from"],
                            contactPerson = (string)reader["customerFrom"],
                            contact = (string)reader["contactFrom"]
                        }
                    },
                    addressTo = new List<Model.Address>()
                    {
                        new Model.Address()
                        {
                            addressId = reader["addToId"].ToString(),
                            address1 = (string)reader["add_to_1"],
                            address2 = (string)reader["add_to_2"],
                            address3 = (string)reader["add_to_3"],
                            stateId = reader["state_to"].ToString(),
                            countryId = reader["country_to"].ToString(),
                            postcode = (string)reader["postcode_to"],
                            gpsLongitude = (float)reader["longitude_to"],
                            gpsLatitude = (float)reader["latitude_to"],
                            contactPerson = (string)reader["customerTo"],
                            contact = (string)reader["contactTo"]
                        }
                    }
                });
            }

            return jobDetailsList;
        }

        public List<Model.JobDetails> GetOpenJobs(string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM (SELECT * FROM {0} WHERE id NOT IN (SELECT job_id FROM {1}) AND jobs.deleted=0 ORDER BY creation_date DESC ", TABLE_NAME_JOB, TABLE_NAME_JOBDELIVERY);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                query += ") jobDetails ";

                // job_from
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_from_1, {1}.add_2 as add_from_2, {1}.add_3 as add_from_3, {1}.state_id as state_from, {1}.country_id as country_from, {1}.postcode as postcode_from, {1}.gps_longitude as longitude_from, {1}.gps_latitude as latitude_from, {0}.*, {0}.address_id as addFromId, {0}.customer_name as customerFrom, {0}.customer_contact as contactFrom FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addFrom ON jobDetails.id=addFrom.job_id ",
                    TABLE_NAME_ADDFROM, TABLE_NAME_ADDRESS);

                // job to
                query += string.Format("INNER JOIN (SELECT {1}.add_1 as add_to_1, {1}.add_2 as add_to_2, {1}.add_3 as add_to_3, {1}.state_id as state_to, {1}.country_id as country_to, {1}.postcode as postcode_to, {1}.gps_longitude as longitude_to, {1}.gps_latitude as latitude_to, {0}.*, {0}.address_id as addToId, {0}.customer_name as customerTo, {0}.customer_contact as contactTo FROM {0} INNER JOIN {1} ON {0}.address_id={1}.id) addTo ON jobDetails.id=addTo.job_id ",
                    TABLE_NAME_ADDTO, TABLE_NAME_ADDRESS);

                // job order status
                query += string.Format("INNER JOIN {0} ON {0}.job_id=jobDetails.id ", TABLE_NAME_JOBORDERSTATUS);

                // reverse order
                query += "ORDER BY creation_date DESC;";


                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                return getDetailsList(reader);
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public Model.ResponseJobOrderStatus GetJobStatus(string jobId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM {0} " +
                    "INNER JOIN {1} ON {0}.id={1}.job_id " +
                    "WHERE {0}.id={2}",
                    TABLE_NAME_JOB, TABLE_NAME_JOBORDERSTATUS, jobId);

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);

                Model.ResponseJobOrderStatus result = null;
                List<Model.JobOrderStatus> jobOrderStatusList = new List<Model.JobOrderStatus>();
                while (reader.Read())
                {
                    if(result == null)
                    {
                        result = new Model.ResponseJobOrderStatus()
                        {
                            job_id = reader["id"].ToString(),
                            orderStatus = new List<Model.JobOrderStatus>(),
                            enabled = true,
                            deleted = true
                        };
                    }

                    result.enabled &= ((int)reader["enabled"] == 0 ? false : true);
                    result.deleted &= ((int)reader["deleted"] == 0 ? false : true);

                    result.orderStatus.Add(new Model.JobOrderStatus()
                    {
                        job_status_id = reader["job_status_id"].ToString(),
                        modify_by = reader["modify_by"].ToString(),
                        last_modified_date = reader["last_modified_date"].ToString()
                    });
                }

                return result;
            }
            catch (Exception e)
            {
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }
    }
}