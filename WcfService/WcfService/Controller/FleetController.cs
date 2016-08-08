using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Controller
{
    public class FleetController : BaseController
    {
        public Response AddFleet(Model.Fleet fleet)
        {
            response.payload = fleetDao.Add(fleet);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateFleet(string id, Model.Fleet fleet)
        {
            response.payload = fleetDao.Update(id, fleet);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetFleets()
        {
            response.payload = fleetDao.Get();
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetFleet(string id)
        {
            response.payload = fleetDao.Get(id);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response DeleteFleet(string id)
        {
            response.payload = fleetDao.Delete(id);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response UpdateFleetDriver(string fleetId, string userId)
        {
            response.payload = fleetDao.UpdateFleetDriver(fleetId, userId);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }
    }
}