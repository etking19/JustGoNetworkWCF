using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class JobTypeDao : BaseDao
    {
        private readonly string TABLE_JOBTYPE = "job_type";

        public List<Model.JobType> Get()
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                mySqlCmd = GenerateQueryCmd(TABLE_JOBTYPE);
                reader = PerformSqlQuery(mySqlCmd);

                List<Model.JobType> jobTypeList = new List<Model.JobType>();
                while (reader.Read())
                {
                    jobTypeList.Add(new Model.JobType()
                    {
                        jobTypeId = reader["id"].ToString(),
                        name = reader["name"].ToString()
                    });
                }

                return jobTypeList;
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
    }
}