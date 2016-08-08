using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Token
    {
        [DataMember]
        public string userId;

        [DataMember]
        public string token;

        [DataMember]
        public string validTill;
    }
}