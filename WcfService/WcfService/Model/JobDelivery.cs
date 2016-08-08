using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class JobDelivery
    {
        [DataMember]
        public string jobDeliveryId;

        [DataMember]
        public string jobId;

        [DataMember]
        public string companyId;

        [DataMember]
        public string driverUserId;

        public string lastModifiedBy;

        public string lastModifiedDate;
    }
}