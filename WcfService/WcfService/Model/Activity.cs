using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Activity
    {
        [DataMember]
        public string activityId;

        [DataMember]
        public string name;
    }
}