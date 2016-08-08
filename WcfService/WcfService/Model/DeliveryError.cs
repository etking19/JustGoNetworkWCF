using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class DeliveryError
    {
        [DataMember]
        public string deliveryErrorId;

        [DataMember]
        public string name;
    }
}