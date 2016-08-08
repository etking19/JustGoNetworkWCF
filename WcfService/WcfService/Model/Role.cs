using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Role
    {
        [DataMember]
        public string roleId;

        [DataMember]
        public string name;
    }
}