using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class User
    {
        [DataMember(IsRequired = false)]
        public string userId;

        [DataMember(IsRequired = true)]
        public string username;

        [DataMember(IsRequired = false)]
        public string password;

        [DataMember(IsRequired = true)]
        public string displayName;

        [DataMember(IsRequired = false)]
        public string identityCard;

        [DataMember(IsRequired = false)]
        public string image;

        [DataMember(IsRequired = true)]
        public string contactNumber;

        [DataMember(IsRequired = true)]
        public string email;

        [DataMember(IsRequired = false)]
        public bool enabled;

        public bool deleted;

        [DataMember(IsRequired = false)]
        public string creationDate;

        [DataMember(IsRequired = false)]
        public string lastModifiedDate;

        [DataMember(IsRequired = false)]
        public string companyId;

        [DataMember(IsRequired = false)]
        public string roleId;
    }
}