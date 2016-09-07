using MySql.Data.MySqlClient;
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
                reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    return (string)reader["id"];
                }
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
                return (mySqlCmd.ExecuteNonQuery() != 0);
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

        public List<Model.JobDelivery> Get(string limit, string skip)
        {
            List<Model.JobDelivery> jobDetailsList = new List<Model.JobDelivery>();
            jobDetailsList.Add(new Model.JobDelivery() { jobDeliveryId = "1", companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" });
            jobDetailsList.Add(new Model.JobDelivery() { jobDeliveryId = "2", companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" });

            return jobDetailsList;
        }

        public Model.JobDelivery Get(string id)
        {
            return new Model.JobDelivery() { jobDeliveryId = id, companyId = "1", driverUserId = "1", jobId = "1", lastModifiedBy = "1", lastModifiedDate = "20160808 000000" };
        }

        public List<Model.JobDelivery> GetByDeliverCompany(string companyId, string limit, string skip, string status)
        {
            throw new NotImplementedException();
        }

        public List<Model.JobDelivery> GetByDriver(string driverId, string limit, string skip, string status)
        {
            throw new NotImplementedException();
        }

        public List<Model.JobDelivery> GetByStatus(string statusId, string limit, string skip)
        {
            throw new NotImplementedException();
        }

        public bool Update(string jobId, string companyId, string driverId)
        {
            return true;
        }

        public void UpdateRating(string jobId, float rating)
        {
            throw new NotImplementedException();
        }

        public float GetRating(string jobId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(string jobId, string statusId)
        {
            throw new NotImplementedException();
        }
    }
}