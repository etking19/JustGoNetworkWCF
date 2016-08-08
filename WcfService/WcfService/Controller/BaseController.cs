﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Dao;
using WcfService.Model;

namespace WcfService.Controller
{
    public class BaseController
    {
        // share common attributes
        protected Response response = new Response();

        // Dao classes
        protected static CommonDao commonDao = new CommonDao();

        protected static CountryDao countryDao = new CountryDao();
        protected static StateDao stateDao = new StateDao();
        protected static FleetTypeDao fleetTypeDao = new FleetTypeDao();
        protected static PickUpErrorDao pickupErrDao = new PickUpErrorDao();
        protected static DeliveryErrorDao deliveryErrDao = new DeliveryErrorDao();
        protected static JobStatusDao jobStatusDao = new JobStatusDao();

        protected static PermissionDao permissionDao = new PermissionDao();
        protected static ActivityDao activityDao = new ActivityDao();
        protected static RoleDao roleDao = new RoleDao();

        protected static UsersDao userDao = new UsersDao();
        protected static CompanyDao companyDao = new CompanyDao();
        protected static FleetDao fleetDao = new FleetDao();
        protected static JobDetailsDao jobDetailsDao = new JobDetailsDao();
        protected static JobDeliveryDao jobDeliveryDao = new JobDeliveryDao();
    }
}