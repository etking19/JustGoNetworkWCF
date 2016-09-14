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

        [DataMember(IsRequired =true)]
        public string registrationNumber;

        [DataMember(IsRequired = true)]
        public string fleetTypeId;

        [DataMember(IsRequired = true)]
        public string roadTaxExpiry;

        [DataMember(IsRequired = true)]
        public string serviceDueDate;

        [DataMember(IsRequired = true)]
        public int serviceDueMileage;

        [DataMember(IsRequired = true)]
        public string companyId;

        [DataMember(IsRequired = true)]
        public string remarks;

        public bool enabled;

        public bool deleted;

        public string creationDate;

        public string lastModifiedDate;
    }
}