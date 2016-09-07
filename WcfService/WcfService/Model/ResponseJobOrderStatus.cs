using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class ResponseJobOrderStatus
    {
        [DataMember]
        public string job_id { get; set; }

        [DataMember]
        public bool enabled { get; set; }

        [DataMember]
        public bool deleted { get; set; }

        [DataMember]
        public List<JobOrderStatus> orderStatus { get; set; }
    }
}