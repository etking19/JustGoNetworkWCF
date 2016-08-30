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
        [DataMember]
        public string userId;

        [DataMember]
        public string username;

        public string password;

        [DataMember]
        public string displayName;

        [DataMember]
        public string identityCard;

        [DataMember]
        public string image;

        [DataMember]
        public string contactNumber;

        [DataMember]
        public string email;

        [DataMember]
        public bool enabled;

        public bool deleted;

        public string creationDate;

        public string lastModifiedDate;

        [DataMember]
        public string companyId;

        [DataMember]
        public string roleId;
    }
}