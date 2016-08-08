using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class FleetType
    {
        [DataMember]
        public string fleetTypeId;

        [DataMember]
        public string name;

        [DataMember]
        public int capacity;

        [DataMember]
        public string type;
    }
}