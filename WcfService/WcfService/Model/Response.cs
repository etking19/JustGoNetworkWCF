using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public bool success;

        [DataMember]
        public int errorCode;

        [DataMember]
        public string errorMessage;

        [DataMember]
        public object payload;
    }
}