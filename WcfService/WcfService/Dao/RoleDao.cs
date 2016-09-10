using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;
using WcfService.Utility;

namespace WcfService.Dao
{
    public class RoleDao : BaseDao
    {
        private readonly string TABLE_ROLES = "roles";

        public Role GetRoleById(string roleId)
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                Dictionary<string, string> queryParam = new Dictionary<string, string>();
                queryParam.Add("id", roleId);

                mySqlCmd = GenerateQueryCmd(TABLE_ROLES, queryParam);
                reader = PerformSqlQuery(mySqlCmd);
                if(reader.Read())
                {
                    return new Role()
                    {
                        roleId = reader["id"].ToString(),
                        name = reader["name"].ToString()
                    };
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

        public List<Role> GetRoles()
        {
            MySqlCommand mySqlCmd = null;
            MySqlDataReader reader = null;
            try
            {
                mySqlCmd = GenerateQueryCmd(TABLE_ROLES);
                reader = PerformSqlQuery(mySqlCmd);

                List<Role> roleList = new List<Role>();
                while (reader.Read())
                {
                    roleList.Add(new Role()
                    {
                        roleId = reader["id"].ToString(),
                        name = reader["name"].ToString()
                    });
                }

                return roleList;
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