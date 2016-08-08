using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Dao;
using WcfService.Model;

namespace WcfService.Controller
{
    public class CompanyController : BaseController
    {
        public Response GetCompany(string companyId)
        {
            response.payload = companyDao.GetCompany(companyId);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);

            return response;
        }

        public Response GetCompanies(string number, string skip)
        {
            response.payload = companyDao.GetAllCompanies(number, skip);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateCompany(string companyId, Model.Company company)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteCompany(string companyId)
        {
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddCompany(Model.Company company)
        {
            companyDao.AddCompany(company);

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}