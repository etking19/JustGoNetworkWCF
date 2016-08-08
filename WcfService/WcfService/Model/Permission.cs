using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Permission
    {
        [DataMember]
        public string permissionId;

        [DataMember]
        public string name;
    }
}