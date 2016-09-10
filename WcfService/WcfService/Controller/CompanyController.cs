using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Dao;
using WcfService.Model;

namespace WcfService.Controller
{
    public class CompanyController : BaseController
    {
        public Response GetCompanies()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var limit = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["limit"];
            var skip = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["skip"];
            var companyId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["companyId"];

            if(companyId != null)
            {
                var result = companyDao.GetCompanyById(companyId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.ECompanyNotFound);
                    return response;
                }

                if (result.deleted)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.ECompanyDeleted);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = companyDao.GetCompanies(limit, skip);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateCompany(string companyId, Model.Company company)
        {
            var result = companyDao.UpdateCompany(companyId, company);
            if(false == result)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteCompany(string companyId)
        {
            var result = companyDao.DeleteCompany(companyId);
            if (false == result)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response AddCompany(Model.Company company)
        {
            var result = companyDao.AddCompany(company);
            if(result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}