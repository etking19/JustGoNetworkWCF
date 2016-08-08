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
        [DataMember]
        public string addressId;

        [DataMember]
        public string address1;

        public string address2;

        [DataMember]
        public string address3;

        [DataMember]
        public string stateId;

        [DataMember]
        public string countryId;

        [DataMember]
        public string postcode;

        [DataMember]
        public float gpsLongitude;

        [DataMember]
        public float gpsLatitude;

        public string createdBy;

        public string creationDate;

        public string lastModifiedDate;
    }
}