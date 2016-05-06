using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Helper
{
    public class JobsDispatched
    {
        private static string sDatabaseName = "jobs_dispatched";

        public string GetJobs(Constants.EJobDispatchStatus status)
        {
            return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true, Payload = GetJobDispatchListByStatus(status) }); ;
        }

        public string AddJobDispatch(int jobId, int companyId)
        {
            try
            {
                Dictionary<string, string> addParams = new Dictionary<string, string>();
                addParams.Add("job_id", jobId.ToString());
                addParams.Add("company_id", companyId.ToString());
                addParams.Add("last_status_update_date", Utils.GetCurrentUtcTime(0));
                addParams.Add("status", ((int)Constants.EJobDispatchStatus.OrderReceived).ToString());

                using (MySqlCommand command = Utils.GenerateAddCmd(sDatabaseName, addParams))
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

        public string EditJobDispatch(int jobDispatchId, int companyId)
        {
            try
            {
                Dictionary<string, string> editParams = new Dictionary<string, string>();
                editParams.Add("company_id", companyId.ToString());

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", jobDispatchId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, editParams, destParams))
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

        public string DeleteJobDispatch(int jobDispatchId)
        {
            try
            {
                Dictionary<string, string> removeParams = new Dictionary<string, string>();
                removeParams.Add("id", jobDispatchId.ToString());

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

        public string DeleteJobsDispatch(int[] jobsDispatchId)
        {
            try
            {
                foreach(int jobDispatchId in jobsDispatchId)
                {
                    Dictionary<string, string> removeParams = new Dictionary<string, string>();
                    removeParams.Add("id", jobDispatchId.ToString());

                    using (MySqlCommand command = Utils.GenerateRemoveCmd(sDatabaseName, removeParams))
                    {
                        Utils.PerformSqlNonQuery(command);
                    }
                }

                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = true });
            }
            catch (Exception ex)
            {
                return Constants.sJavaScriptSerializer.Serialize(new Constants.Result() { Success = false, ErrorCode = ErrorCodes.EGeneralError, ErrorMessage = ex.Message });
            }
        }

        public string AssignDriver(int jobDispatchId, string driverUsername)
        {
            try
            {
                Dictionary<string, string> editParams = new Dictionary<string, string>();
                editParams.Add("driver_id", driverUsername);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", jobDispatchId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, editParams, destParams))
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

        public string UpdatePickUpErrorStatus(int jobDispatchId, int pickUpError)
        {
            try
            {
                Dictionary<string, string> editParams = new Dictionary<string, string>();
                editParams.Add("driver_id", pickUpError.ToString());

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", jobDispatchId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, editParams, destParams))
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

        public string UpdateDeliveryErrorStatus(int jobDispatchId, int deliveryError, string remarks)
        {
            try
            {
                Dictionary<string, string> editParams = new Dictionary<string, string>();
                editParams.Add("delivery_error", deliveryError.ToString());
                editParams.Add("status_remarks", remarks);

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", jobDispatchId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, editParams, destParams))
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

        public string UpdateJobDispatchStatus(int jobDispatchId, int status)
        {
            try
            {
                Dictionary<string, string> editParams = new Dictionary<string, string>();
                editParams.Add("status", status.ToString());

                Dictionary<string, string> destParams = new Dictionary<string, string>();
                destParams.Add("id", jobDispatchId.ToString());

                using (MySqlCommand command = Utils.GenerateEditCmd(sDatabaseName, editParams, destParams))
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

        public List<Constants.JobDispatch> GetJobDispatchListByStatus(Constants.EJobDispatchStatus status)
        {
            List<Constants.JobDispatch> jobsDispatchList = new List<Constants.JobDispatch>();

            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("status", ((int)status).ToString());

            using (MySqlCommand command = Utils.GenerateQueryCmd(sDatabaseName))
            {
                using (MySqlDataReader reader = Utils.PerformSqlQuery(command))
                {
                    while (reader.Read())
                    {
                        jobsDispatchList.Add(new Constants.JobDispatch()
                        {
                            Id = (int)reader["id"],
                            JobId = (int)reader["job_id"],
                            Job = new Jobs().GetJobById((int)reader["job_id"]),
                            CompanyId = (int)reader["company_id"],
                            DriverId = (int)reader["driver_id"],
                            Driver = new Drivers().GetDriverByUserId((int)reader["driver_id"]),
                            Rating = (float)reader["rating"],
                            JobStatus = (Constants.EJobDispatchStatus)reader["status"],
                            PickUpError = new PickUpErrors().GetPickUpErrorById((int)reader["pick_up_error"]),
                            DeliveryError = new DeliveryErrors().GetDeliveryErrorById((int)reader["delivery_error"]),
                            StatusRemarks = (string)reader["status_remarks"]
                        });
                    }
                }
            }

            return jobsDispatchList;
        }

    }
}