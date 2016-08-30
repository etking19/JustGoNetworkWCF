using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class JobDetails
    {
        [DataMember]
        public string jobId;

        [DataMember]
        public string ownerUserId;

        [DataMember]
        public string jobTypeId;

        [DataMember]
        public float amount;

        [DataMember]
        public float amountPaid;

        [DataMember]
        public bool cashOnDelivery;

        [DataMember]
        public int workerAsistance;

        [DataMember]
        public string remarks;

        [DataMember]
        public string uniqueId;

        [DataMember]
        public bool enabled;

        [DataMember]
        public string createdBy;

        [DataMember]
        public string addressFromId;

        [DataMember]
        public string addressToId;

        [DataMember]
        public string creationDate;

        [DataMember]
        public string jobStatusId;

        public string modifiedBy;

        public string lastModifiedDate;

        public bool deleted;
    }
}