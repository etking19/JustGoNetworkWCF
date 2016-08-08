using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Company
    {
        [DataMember]
        public string companyId;

        [DataMember]
        public string name;

        [DataMember]
        public bool enabled;

        [DataMember]
        public string address1;

        [DataMember]
        public string address2;

        [DataMember]
        public string postcode;

        [DataMember]
        public string stateId;

        [DataMember]
        public string countryId;

        [DataMember]
        public string registrationNumber;

        public bool deleted;

        public string creationDate;

        public string lastModifiedDate;
    }
}