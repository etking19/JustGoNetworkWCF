using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class JobStatusDao : BaseDao
    {
        private readonly string TABLE_NAME = "job_status";

        public List<Model.JobStatus> Get()
        {
            List<Model.JobStatus> jobStatusList = new List<Model.JobStatus>();

            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                mySqlCmd = GenerateQueryCmd(TABLE_NAME);
                reader = mySqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    jobStatusList.Add(new Model.JobStatus()
                    {
                        jobStatusId = (string)reader["id"],
                        name = (string)reader["name"]
                    });
                }
            }
            catch(Exception e)
            {
                DBLogger.Log(DBLogger.ESeverity.Error, e.Message);
            }
            finally
            {
                CleanUp(reader, mySqlCmd);
            }

            return jobStatusList;
        }
    }
}