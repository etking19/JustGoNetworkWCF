using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class JobOrderStatus 
    {
        public string id { get; set; }
        public string job_id { get; set; }

        [DataMember]
        public string job_status_id { get; set; }

        [DataMember]
        public string modify_by { get; set; }

        [DataMember]
        public string last_modified_date { get; set; }
    }
}