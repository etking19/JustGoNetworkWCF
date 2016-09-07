using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Address
    {
        [DataMember(IsRequired = false)]
        public string addressId;

        [DataMember(IsRequired = true)]
        public string address1;

        [DataMember(IsRequired = true)]
        public string address2;

        [DataMember(IsRequired = true)]
        public string address3;

        [DataMember(IsRequired = true)]
        public string stateId;

        [DataMember(IsRequired = true)]
        public string countryId;

        [DataMember(IsRequired = true)]
        public string postcode;

        [DataMember(IsRequired = true)]
        public float gpsLongitude;

        [DataMember(IsRequired = true)]
        public float gpsLatitude;

        [DataMember(IsRequired = false)]
        public string contactPerson;

        [DataMember(IsRequired = false)]
        public string contact;

        public string createdBy;

        public string creationDate;

        public string lastModifiedDate;
    }
}