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
        [DataMember(IsRequired = false)]
        public string jobId { get; set; }

        [DataMember(IsRequired = true)]
        public string ownerUserId { get; set; }

        [DataMember(IsRequired = true)]
        public string jobTypeId { get; set; }

        [DataMember(IsRequired = true)]
        public float amount { get; set; }

        [DataMember(IsRequired = false)]
        public float amountPaid { get; set; }

        [DataMember(IsRequired = false)]
        public bool cashOnDelivery { get; set; }

        [DataMember(IsRequired = true)]
        public int workerAsistance { get; set; }

        [DataMember(IsRequired = true)]
        public string remarks { get; set; }

        [DataMember(IsRequired = false)]
        public string uniqueId { get; set; }

        [DataMember(IsRequired = false)]
        public bool enabled { get; set; }

        [DataMember(IsRequired = false)]
        public string createdBy { get; set; }

        [DataMember(IsRequired = true)]
        public string addressFromId { get; set; }

        [DataMember(IsRequired = true)]
        public string addressToId { get; set; }

        [DataMember(IsRequired = false)]
        public string creationDate { get; set; }

        [DataMember(IsRequired = false)]
        public string jobStatusId { get; set; }

        [DataMember(IsRequired = false)]
        public string modifiedBy { get; set; }
   
        [DataMember(IsRequired = false)]
        public string lastModifiedDate { get; set; }

        [DataMember(IsRequired = false)]
        public bool deleted { get; set; }
    }
}