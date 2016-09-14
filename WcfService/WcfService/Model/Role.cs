using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WcfService.Utility;

namespace WcfService.Model
{
    [DataContract]
    public class Role
    {
        [DataMember]
        public string roleId;

        [DataMember]
        public string name;

        [DataMember]
        public List<Permission> permissionList { get; set; }
    }
}