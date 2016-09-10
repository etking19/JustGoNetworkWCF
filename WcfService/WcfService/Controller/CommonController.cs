using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class CommonController : BaseController
    {
        public Response Test()
        {
            response.payload = javaScriptSerializer.Serialize(commonDao.Test());
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetPickupError()
        {
            response.payload = pickupErrDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetPickupError(string id)
        {
            response.payload = pickupErrDao.Get(id);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetDeliveryError()
        {
            response.payload = deliveryErrDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetDeliveryError(string id)
        {
            response.payload = deliveryErrDao.Get(id);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetFleetType()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var fleetTypeId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["fleetTypeId"];
            if(fleetTypeId != null)
            {
                var result = fleetTypeDao.Get(fleetTypeId);
                if(result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EResourceNotFoundError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = fleetTypeDao.Get();
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

        public Response GetPermission()
        {
            response.payload = permissionDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetState()
        {
            response.payload = stateDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetState(string id)
        {
            response.payload = stateDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetCountry()
        {
            response.payload = countryDao.GetCountris();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetCountry(string id)
        {
            response.payload = countryDao.GetCountry(id);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}