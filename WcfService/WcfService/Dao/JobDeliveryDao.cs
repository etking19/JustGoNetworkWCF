﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class JobDeliveryDao : BaseDao
    {
        private readonly string TABLE_NAME = "job_delivery";
        private readonly string TABLE_ORDER_STATUS = "job_order_status";
        private readonly string TABLE_JOBS = "jobs";

        public string Add(string jobId, string companyId, string driverId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> insertParam = new Dictionary<string, string>();
                insertParam.Add("job_id", jobId);
                insertParam.Add("company_id", companyId);
                insertParam.Add("driver_user_id", driverId);

                mySqlCmd = GenerateAddCmd(TABLE_NAME, insertParam);
                PerformSqlNonQuery(mySqlCmd);

                return mySqlCmd.LastInsertedId.ToString();
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

        public bool Delete(string id)
        {
            MySqlCommand mySqlCmd = null;
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", id);

                mySqlCmd = GenerateRemoveCmd(TABLE_NAME, removeParams);
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

        public Model.JobDelivery Get(string id)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT * FROM {0} " +
                    "LEFT JOIN {1} ON {0}.job_id={1}.job_id " +
                    "INNER JOIN {3} ON {0}.job_id={3}.id " +
                    "WHERE {0}.id={2} AND {3}.deleted=0 AND {3}.enabled=1",
                    TABLE_NAME, TABLE_ORDER_STATUS, id, TABLE_JOBS);

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);

                if (reader.Read())
                {
                    return constructObj(reader);
                }
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

        public List<Model.JobDelivery> Get(string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.*, {1}.id as Jid, {1}.job_status_id as JSid FROM {0} " +
                    "LEFT JOIN {1} ON {0}.job_id={1}.job_id " +
                    "INNER JOIN {2} ON {0}.job_id={2}.id " +
                    "WHERE {2}.deleted=0 AND {2}.enabled=1 ORDER BY {0}.last_modified_date DESC ",
                    TABLE_NAME, TABLE_ORDER_STATUS, TABLE_JOBS);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);

                List<Model.JobDelivery> deliveryList = new List<Model.JobDelivery>();
                while (reader.Read())
                {
                    var jobId = reader["job_id"].ToString();
                    var previousResult = deliveryList.Find(t => t.jobId.CompareTo(jobId) == 0);
                    if (previousResult == null)
                    {
                        deliveryList.Add(constructObj(reader));
                    }
                    else
                    {
                        previousResult.orderStatusList.Add(constructJobOrder(reader));
                    }
                }

                return deliveryList;
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

        public List<Model.JobDelivery> GetByDeliverCompany(string companyId, string statusId, string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.*, {1}.id as Jid, {1}.job_status_id as JSid FROM {0} " +
                    "LEFT JOIN {1} ON {0}.job_id={1}.job_id " +
                    "INNER JOIN {3} ON {0}.job_id={3}.id " +
                    "WHERE {0}.company_id=@company_id AND {3}.deleted=0 AND {3}.enabled=1 ",
                    TABLE_NAME, TABLE_ORDER_STATUS, companyId, TABLE_JOBS);

                if (statusId != null)
                {
                    query += string.Format("AND {0}.job_status_id=@job_status_id ", 
                        TABLE_ORDER_STATUS);
                }

                query += string.Format("ORDER BY {0}.last_modified_date DESC ",
                    TABLE_NAME);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                mySqlCmd = new MySqlCommand(query);
                mySqlCmd.Parameters.AddWithValue("@company_id", companyId);
                mySqlCmd.Parameters.AddWithValue("@job_status_id", statusId);

                reader = PerformSqlQuery(mySqlCmd);

                List<Model.JobDelivery> deliveryList = new List<Model.JobDelivery>();
                while (reader.Read())
                {
                    var jobId = reader["job_id"].ToString();
                    var previousResult = deliveryList.Find(t => t.jobId.CompareTo(jobId) == 0);
                    if(previousResult == null)
                    {
                        deliveryList.Add(constructObj(reader));
                    }
                    else
                    {
                        previousResult.orderStatusList.Add(constructJobOrder(reader));
                    }
                }

                return deliveryList;
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

        public List<Model.JobDelivery> GetByDriver(string driverId, string statusId, string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.*, {1}.id as Jid, {1}.job_status_id as JSid FROM {0} " +
                    "LEFT JOIN {1} ON {0}.job_id={1}.job_id " +
                    "INNER JOIN {3} ON {0}.job_id={3}.id " +
                    "WHERE {0}.driver_user_id=@driver_user_id AND {3}.deleted=0 AND {3}.enabled=1 ",
                    TABLE_NAME, TABLE_ORDER_STATUS, driverId, TABLE_JOBS);

                if (statusId != null)
                {
                    query += string.Format("AND {0}.job_status_id=@job_status_id ",
                        TABLE_ORDER_STATUS);
                }

                query += string.Format("ORDER BY {0}.last_modified_date DESC ",
                    TABLE_NAME);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                mySqlCmd = new MySqlCommand(query);
                mySqlCmd.Parameters.AddWithValue("@driver_user_id", driverId);
                mySqlCmd.Parameters.AddWithValue("@job_status_id", statusId);

                reader = PerformSqlQuery(mySqlCmd);

                List<Model.JobDelivery> deliveryList = new List<Model.JobDelivery>();
                while (reader.Read())
                {
                    var jobId = reader["job_id"].ToString();
                    var previousResult = deliveryList.Find(t => t.jobId.CompareTo(jobId) == 0);
                    if (previousResult == null)
                    {
                        deliveryList.Add(constructObj(reader));
                    }
                    else
                    {
                        previousResult.orderStatusList.Add(constructJobOrder(reader));
                    }
                }

                return deliveryList;
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

        public List<Model.JobDelivery> GetByStatus(string statusId, string limit, string skip)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT {0}.*, {1}.id as Jid, {1}.job_status_id as JSid FROM {0} " +
                    "LEFT JOIN {1} ON {0}.job_id={1}.job_id " +
                    "INNER JOIN {3} ON {0}.job_id={3}.id " +
                    "WHERE {1}.job_status_id={2} AND {3}.deleted=0 AND {3}.enabled=1 ORDER BY {0}.last_modified_date DESC ",
                    TABLE_NAME, TABLE_ORDER_STATUS, statusId, TABLE_JOBS);

                if (limit != null)
                {
                    query += string.Format("LIMIT {0} ", limit);
                }

                if (skip != null)
                {
                    query += string.Format("OFFSET {0} ", skip);
                }

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);

                List<Model.JobDelivery> deliveryList = new List<Model.JobDelivery>();
                while (reader.Read())
                {
                    var jobId = reader["job_id"].ToString();
                    var previousResult = deliveryList.Find(t => t.jobId.CompareTo(jobId) == 0);
                    if (previousResult == null)
                    {
                        deliveryList.Add(constructObj(reader));
                    }
                    else
                    {
                        previousResult.orderStatusList.Add(constructJobOrder(reader));
                    }
                }

                return deliveryList;
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

        public bool Update(string jobId, string companyId, string driverId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> updateParam = new Dictionary<string, string>();
                updateParam.Add("company_id", companyId);
                updateParam.Add("driver_user_id", driverId);

                Dictionary<string, string> destinationParam = new Dictionary<string, string>();
                destinationParam.Add("job_id", jobId);

                mySqlCmd = GenerateEditCmd(TABLE_NAME, updateParam, destinationParam);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
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

            return false;
        }

        public bool UpdateRating(string jobId, float rating)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> updateParam = new Dictionary<string, string>();
                updateParam.Add("rating", rating.ToString());

                Dictionary<string, string> destinationParam = new Dictionary<string, string>();
                destinationParam.Add("job_id", jobId);

                mySqlCmd = GenerateEditCmd(TABLE_NAME, updateParam, destinationParam);
                return (PerformSqlNonQuery(mySqlCmd) != 0);
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

            return false;
        }

        public bool UpdateJobStatus(string jobId, string statusId, string pickupErrId = null, string deliverErrId = null)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> insertParam = new Dictionary<string, string>();
                insertParam.Add("job_status_id", statusId);
                insertParam.Add("job_id", jobId);

                mySqlCmd = GenerateAddCmd(TABLE_ORDER_STATUS, insertParam);
                if (PerformSqlNonQuery(mySqlCmd) == 0)
                {
                    return false;
                }

                if(pickupErrId != null ||
                    deliverErrId != null)
                {
                    CleanUp(reader, mySqlCmd);

                    Dictionary<string, string> updateParam = new Dictionary<string, string>();
                    if(pickupErrId != null)
                    {
                        updateParam.Add("pickup_error_id", pickupErrId);
                    }

                    if(deliverErrId != null)
                    {
                        updateParam.Add("delivery_error_id", deliverErrId);
                    }

                    Dictionary<string, string> destParam = new Dictionary<string, string>();
                    destParam.Add("job_id", jobId);

                    mySqlCmd = GenerateEditCmd(TABLE_NAME, updateParam, destParam);
                    return (PerformSqlNonQuery(mySqlCmd) != 0);
                }
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

            return false;
        }

        private Model.JobDelivery constructObj(MySqlDataReader reader)
        {
            return new Model.JobDelivery()
            {
                jobDeliveryId = reader["id"].ToString(),
                jobId = reader["job_id"].ToString(),
                companyId = reader["company_id"].ToString(),
                driverUserId = reader["driver_user_id"].ToString(),
                rating = reader.GetFloat("rating"),
                pickupErr = reader["pickup_error_id"].ToString(),
                deliverErr = reader["delivery_error_id"].ToString(),
                lastModifiedDate = reader["last_modified_date"].ToString(),
                orderStatusList = new List<Model.JobOrderStatus>()
                {
                    new Model.JobOrderStatus()
                    {
                        id = reader["Jid"].ToString(),
                        job_id = reader["job_id"].ToString(),
                        job_status_id = reader["JSid"].ToString()
                    }

                }
            };
        }

        private Model.JobOrderStatus constructJobOrder(MySqlDataReader reader)
        {
            return new Model.JobOrderStatus()
            {
                id = reader["Jid"].ToString(),
                job_id = reader["job_id"].ToString(),
                job_status_id = reader["JSid"].ToString()
            };
        }

        public float GetRatingByCompany(string companyId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT AVG(rating) as rating FROM {0} WHERE company_id={1};", 
                    TABLE_NAME, companyId);

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                if(reader.Read())
                {
                    return reader.GetFloat("rating");
                }
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

            return 0;
        }

        public float GetRatingByUser(string userId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string query = string.Format("SELECT AVG(rating) as rating FROM {0} WHERE driver_user_id={1};",
                    TABLE_NAME, userId);

                mySqlCmd = new MySqlCommand(query);
                reader = PerformSqlQuery(mySqlCmd);
                if (reader.Read())
                {
                    return reader.GetFloat("rating");
                }
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

            return 0;
        }
    }
}