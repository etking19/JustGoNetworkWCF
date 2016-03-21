using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    [ServiceContract]
    public interface IAjaxService
    {
        [OperationContract]
        string Test();

        /*
        General functions
        */
        [OperationContract]
        string Login(string username, string password);
        [OperationContract]
        string ChangePassword(string adminId, string token, string oldPassword, string newPassword);

        /*
        Asset management functions
        */
        [OperationContract]
        string GetAssets(string adminId, string token);
        string AddAsset(string adminId, string token, string assetName, string regNum, string capacity, string roadTax);
        string EditAsset(string adminId, string token, string assetId, string assetName, string regNum, string capacity, string roadTax);
        string RemoveAsset(string adminId, string token, string assetId);
        string RemoveAssets(string adminId, string token, string[] assetIds);

        /*
        User/Driver management functions
        */
        string GetUsers(string adminId, string token);
        string AddUser(string adminId, string token, string firstName, string lastName,
            string position, int gender, string contactNum, string dob, int assetId);
        string EditUser(string adminId, string token, string userId, string firstName, string lastName,
            string position, int gender, string contactNum, string dob, int assetId);
        string RemoveUser(string adminId, string token, string userId);
        string RemoveUsers(string adminId, string token, string[] userIds);

        /*
        Job management functions
        */
        string GetJobs(string adminId, string token);
        string AddJob(string adminId, string token, string customerName, string contactNum, string description,
            string destLongitude, string destLatitude, string add1, string add2, string add3, string poscode, int state,
            string deliveryDate, string deliveryTime, float quotation, bool cashOnDelivery, int assistance, int userId);
        string EditJob(string adminId, string token, string jobId, string customerName, string contactNum, string description,
            string destLongitude, string destLatitude, string add1, string add2, string add3, string poscode, int state,
            string deliveryDate, string deliveryTime, float quotation, bool cashOnDelivery, int assistance, int userId);
        string RemoveJob(string adminId, string token, string jobId);
        string RemoveJobs(string adminId, string token, string[] jobIds);

        /*
        GPS tracking functions
        */
        string GetLastTrackingPos(string adminId, string token, string userId);
        string GetLastTrackingPosbyList(string adminId, string token, string[] userIds);

        /*
        Master admin functions
        */
        string GetCompanies(string adminId, string token);
        string AddCompany(string adminId, string token, string name, string contact, string website, string description);
        string EditCompany(string adminId, string token, string companyId, string name, string contact, string website, string description);
        string RemoveCompany(string adminId, string token, string companyId);
        string RemoveCompanies(string adminId, string token, string[] companyIds);

        string AddCompanyAdmin(string adminId, string token, string companyId, string username, string password, string email);
        string RemoveCompanyAdmin(string adminId, string token, string companyAdminId);
        string RemoveCompanyAdmins(string adminId, string token, string[] companyAdminIds);

        string ResetPassword(string adminId, string token, string username);

        /*
        Client app functions
        */
        string GetTasks(string userId, string token);
        string UpdateTask(string userId, string token, string taskId, int status);
        string UpdateDevOrder(string userId, string token, string taskId, string orderBase64);

        string UpdateLocation(string userId, string longitude, string latitude);
    }
}
