using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class JobStatus
    {
        [DataMember]
        public string jobStatusId;

        [DataMember]
        public string name;
    }
}