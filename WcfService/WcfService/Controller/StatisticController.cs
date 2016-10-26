using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class StatisticController : BaseController
    {
        public Response GetDriverLocation()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            var jobStatusId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobStatusId"];
            var companyId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["companyId"];
            var driverId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["driverId"];

            if (driverId != null)
            {
                var result = statisticDao.GetByUserId(driverId);
                if (null == result)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if (companyId != null)
            {
                var result = statisticDao.GetByCompany(companyId);
                if (null == result)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }
            else if (jobStatusId != null)
            {
                var result = statisticDao.GetByJobStatus(jobStatusId);
                if (null == result)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = statisticDao.GetAll();
                if (null == result)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }




            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}