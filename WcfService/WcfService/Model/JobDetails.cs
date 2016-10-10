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
        private float _amount = 0;

        [DataMember(IsRequired = false)]
        public string jobId { get; set; }

        [DataMember(IsRequired = true)]
        public string ownerUserId { get; set; }

        [DataMember(IsRequired = true)]
        public string jobTypeId { get; set; }

        [DataMember(IsRequired = true)]
        public string fleetTypeId { get; set; }

        [DataMember(IsRequired = true)]
        public float amount
        {
            get
            {
                return _amount;
            }

            set
            {
                this._amount = value;
            }
        }

        [DataMember(IsRequired = false)]
        public float amountPartner
        {
            get
            {
                return _amount * 0.9f;
            }
            set
            {

            }
        }

        [DataMember(IsRequired = false)]
        public float amountPaid { get; set; }

        [DataMember(IsRequired = false)]
        public bool cashOnDelivery { get; set; }

        [DataMember(IsRequired = true)]
        public int workerAssistant { get; set; }

        [DataMember(IsRequired = true)]
        public string deliveryDate { get; set; }

        [DataMember(IsRequired = true)]
        public string remarks { get; set; }

        [DataMember(IsRequired = false)]
        public bool enabled { get; set; }

        [DataMember(IsRequired = false)]
        public string createdBy { get; set; }

        [DataMember(IsRequired = true)]
        public List<Address> addressFrom { get; set; }

        [DataMember(IsRequired = true)]
        public List<Address> addressTo { get; set; }

        [DataMember(IsRequired = false)]
        public string creationDate { get; set; }

        [DataMember(IsRequired = false)]
        public string jobStatusId { get; set; }

        [DataMember(IsRequired = false)]
        public string modifiedBy { get; set; }
   
        [DataMember(IsRequired = false)]
        public string lastModifiedDate { get; set; }

        public bool deleted { get; set; }
    }
}