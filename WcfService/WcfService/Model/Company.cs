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
        [DataMember(IsRequired = true)]
        public string name;

        [DataMember(IsRequired = true)]
        public string address1;

        [DataMember(IsRequired = true)]
        public string address2;

        [DataMember(IsRequired = true)]
        public string postcode;

        [DataMember(IsRequired = true)]
        public string stateId;

        [DataMember(IsRequired = true)]
        public string countryId;

        [DataMember(IsRequired = true)]
        public string registrationNumber;

        public string companyId;

        public bool enabled;

        public bool deleted;

        public string creationDate;

        public string lastModifiedDate;

        public float rating;

        public User[] admin;
    }
}