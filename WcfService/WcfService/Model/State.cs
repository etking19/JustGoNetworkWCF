using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class State
    {
        [DataMember]
        public string stateId;

        [DataMember]
        public string name;

        [DataMember]
        public string countryId;
    }
}