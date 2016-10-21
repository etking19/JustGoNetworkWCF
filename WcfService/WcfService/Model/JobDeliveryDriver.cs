﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Model
{
    public class JobDeliveryDriver
    {
        public string id { get; set; }
        public string jobId { get; set; }
        public float rating { get; set; }

        public User driver { get; set; }
        public Company company { get; set; }
    }
}