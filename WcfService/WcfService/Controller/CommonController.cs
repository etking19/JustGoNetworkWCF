using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class CommonController : BaseController
    {
        public string Test()
        {
            return commonDao.Test();
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
            response.payload = fleetTypeDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetFleetType(string id)
        {
            response.payload = fleetTypeDao.Get(id);
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