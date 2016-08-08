using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class FleetDao : BaseDao
    {
        public string Add(Model.Fleet payload)
        {
            return "1";
        }

        public bool Delete(string id)
        {
            return true;
        }

        public List<Model.Fleet> Get()
        {
            List<Model.Fleet> fleetList = new List<Model.Fleet>();
            fleetList.Add(new Model.Fleet() { fleetId = "1", companyId = "1", fleetTypeId = "1", fleetDriverId = "1", roadTaxExpiry = "20160808 000000000", creationDate = "20160808 000000000", registrationNumber = "WWW 1", lastModifiedDate = "20160808 000000000", enabled = true, deleted = false, remarks = "no remarks", serviceDueDate = "20170808 000000000", serviceDueMileage = 80000 });
            fleetList.Add(new Model.Fleet() { fleetId = "2", companyId = "1", fleetTypeId = "2", fleetDriverId = "2", roadTaxExpiry = "20160808 000000000", creationDate = "20160808 000000000", registrationNumber = "WWW 2", lastModifiedDate = "20160808 000000000", enabled = true, deleted = false, remarks = "no remarks", serviceDueDate = "20170808 000000000", serviceDueMileage = 80000 });

            return fleetList;
        }

        public Model.Fleet Get(string id)
        {
            return new Model.Fleet() { fleetId = "1", companyId = "1", fleetTypeId = "1", fleetDriverId = "1", roadTaxExpiry = "20160808 000000000", creationDate = "20160808 000000000", registrationNumber = "WWW 1", lastModifiedDate = "20160808 000000000", enabled = true, deleted = false, remarks = "no remarks", serviceDueDate = "20170808 000000000", serviceDueMileage = 80000 };
        }

        public bool Update(string id, Model.Fleet payload)
        {
            return true;
        }

        public bool UpdateFleetDriver(string fleetId, string driverId)
        {
            return true;
        }
    }
}