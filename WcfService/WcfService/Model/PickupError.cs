using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class PickupError
    {
        [DataMember]
        public string pickupErrorId;

        [DataMember]
        public string name;
    }
}