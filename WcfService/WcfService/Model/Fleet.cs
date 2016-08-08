using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Fleet
    {
        [DataMember]
        public string fleetId;

        [DataMember]
        public string registrationNumber;

        [DataMember]
        public string fleetTypeId;

        [DataMember]
        public string roadTaxExpiry;

        [DataMember]
        public string serviceDueDate;

        [DataMember]
        public int serviceDueMileage;

        [DataMember]
        public string companyId;

        [DataMember]
        public string remarks;

        [DataMember]
        public bool enabled;

        [DataMember]
        public string fleetDriverId;

        public bool deleted;

        public string creationDate;

        public string lastModifiedDate;
    }
}