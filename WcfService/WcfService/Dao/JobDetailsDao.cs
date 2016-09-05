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
                insertParam.Add("cash_on_delivery", payload.cashOnDelivery.ToString());
                insertParam.Add("worker_assistance", payload.workerAsistance.ToString());
                insertParam.Add("remarks", payload.remarks.ToString());
                insertParam.Add("created_by", payload.createdBy);

                mySqlCmd = GenerateAddCmd(TABLE_NAME_JOB, insertParam);
                reader = mySqlCmd.ExecuteReader();
                if (reader.Read() == false)
                {
                    return null;
                }
                var jobId = (string)reader["id"];

                CleanUp(reader, mySqlCmd);
                reader = null;

                // add to job order status
                insertParam = new Dictionary<string, string>();
                insertParam.Add("job_id", jobId);
                insertParam.Add("modify_by", payload.ownerUserId);
                mySqlCmd = GenerateAddCmd(TABLE_NAME_JOBORDERSTATUS, insertParam);
                mySqlCmd.ExecuteNonQuery();

                return jobId;
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        /// <summary>
        /// Soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            MySqlCommand mySqlCmd = null;
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", id);

                mySqlCmd = GenerateSoftDelete(TABLE_NAME_JOB, removeParams);
                return (mySqlCmd.ExecuteNonQuery() != 0);
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);
            } 
            finally
            {
                CleanUp(null, mySqlCmd);
            }

            return false;
        }

        public List<Model.JobDetails> Get(string limit, string skip)
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();
            
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{1}.address_id as addFrom,{2}.address_id as addTo FROM {0}" + 
                    "INNER JOIN {1} ON {0}.id={1}.job_id " + 
                    "INNER JOIN {2} ON {0}.id={2}.job_id " +
                    "INNER JOIN {3} ON {0}.id={3}.job_id " +
                    "WHERE {0}.deleted=0;", 
                    TABLE_NAME_JOB, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, TABLE_NAME_JOBORDERSTATUS);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                while(reader.Read())
                {
                    jobDetailsList.Add(new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0? false:true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        createdBy = (string)reader["created_by"],
                        creationDate = reader["creation_date"].ToString(),
                        modifiedBy = (string)reader["modify_by"],
                        lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    });
                }

            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }


            return jobDetailsList;
        }

        public List<Model.JobDetails> GetByOwner(string ownerId)
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();

            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{1}.address_id as addFrom,{2}.address_id as addTo FROM {0} INNER JOIN {1} ON {0}.id={1}.job_id INNER JOIN {2} ON {0}.id={2}.job_id WHERE {0}.deleted=0 AND {0}.owner_id={3};",
                    TABLE_NAME_JOB, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, ownerId);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    jobDetailsList.Add(new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        createdBy = (string)reader["created_by"],
                        creationDate = reader["creation_date"].ToString(),
                        modifiedBy = (string)reader["modify_by"],
                        lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    });
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return jobDetailsList;
        }

        public List<Model.JobDetails> GetByOwner(string ownerId, string limit, string skip)
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();

            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{1}.address_id as addFrom,{2}.address_id as addTo FROM {0} INNER JOIN {1} ON {0}.id={1}.job_id INNER JOIN {2} ON {0}.id={2}.job_id WHERE deleted=0 AND {0}.owner_id={3} LIMIT {4} OFFSET {5};",
                    TABLE_NAME_JOB, TABLE_NAME_JOBDELIVERY, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, ownerId, limit, skip);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    jobDetailsList.Add(new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        createdBy = (string)reader["created_by"],
                        creationDate = reader["creation_date"].ToString(),
                        modifiedBy = (string)reader["modify_by"],
                        lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    });
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return jobDetailsList;
        }

        public List<Model.JobDetails> GetByDeliveryCompany(string companyId)
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();

            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{2}.address_id as addFrom,{3}.address_id as addTo FROM {0} INNER JOIN {1} ON {0}.id={1}.job_id INNER JOIN {2} ON {0}.id={2}.job_id INNER JOIN {3} ON {0}.id={3}.job_id WHERE {0}.deleted=0 AND {1}.company_id={4};",
                    TABLE_NAME_JOB, TABLE_NAME_JOBDELIVERY, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, companyId);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    jobDetailsList.Add(new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        createdBy = (string)reader["created_by"],
                        creationDate = reader["creation_date"].ToString(),
                        modifiedBy = (string)reader["modify_by"],
                        lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    });
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return jobDetailsList;
        }

        public Model.JobDetails Get(string jobId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{2}.address_id as addFrom,{3}.address_id as addTo FROM {0} INNER JOIN {1} ON {0}.id={1}.job_id INNER JOIN {2} ON {0}.id={2}.job_id WHERE {0}.id={3};",
                    TABLE_NAME_JOB, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, jobId);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        createdBy = (string)reader["created_by"],
                        creationDate = reader["creation_date"].ToString(),
                        modifiedBy = (string)reader["modify_by"],
                        lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    };
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }

        public List<Model.JobDetails> GetOpenJobs()
        {
            List<Model.JobDetails> jobDetailsList = new List<Model.JobDetails>();

            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT *,{2}.address_id as addFrom,{3}.address_id as addTo FROM {0} INNER JOIN {2} ON {0}.id={2}.job_id INNER JOIN {3} ON {0}.id={3}.job_id WHERE {0}.deleted=0 AND {0}.enabled=1 AND {0}.job_status_id={4} AND {0}.id NOT IN (SELECT job_id FROM {1});",
                    TABLE_NAME_JOB, TABLE_NAME_JOBDELIVERY, TABLE_NAME_ADDFROM, TABLE_NAME_ADDTO, Configuration.JOB_STATUS_PAID);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    jobDetailsList.Add(new Model.JobDetails()
                    {
                        jobId = (string)reader["id"],
                        //ownerUserId = (string)reader["owner_id"],
                        jobTypeId = (string)reader["job_type_id"],
                        amount = (float)reader["amount"],
                        //amountPaid = (float)reader["amount_paid"],
                        cashOnDelivery = (int)reader["cash_on_delivery"] == 0 ? false : true,
                        workerAsistance = (int)reader["worker_assistance"],
                        remarks = (string)reader["remarks"],
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        //createdBy = (string)reader["created_by"],
                        //creationDate = reader["creation_date"].ToString(),
                        //modifiedBy = (string)reader["modify_by"],
                        //lastModifiedDate = reader["last_modified_date"].ToString(),
                        addressFromId = (string)reader["addFrom"],
                        addressToId = (string)reader["addTo"],
                        jobStatusId = (string)reader["job_status_id"]
                    });
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);

                return null;
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return jobDetailsList;
        }

        /// <summary>
        /// Consumer query status
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public Model.JobDetails GetJobStatus(string jobId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT job_status_id,deleted,enabled FROM {0} WHERE {0}.id={1};",
                    TABLE_NAME_JOB, jobId);

                mySqlCmd = new MySqlCommand(query);
                reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Model.JobDetails()
                    {
                        deleted = (int)reader["deleted"] == 0 ? false : true,
                        enabled = (int)reader["enabled"] == 0 ? false : true,
                        jobStatusId = (string)reader["status_id"]
                    };
                }
            }
            catch (Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
                DBLogger.Log(DBLogger.ESeverity.Info, e.StackTrace);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return null;
        }
    }
}