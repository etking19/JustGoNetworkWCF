using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Model;
using WcfService.Model.Delivery;
using WcfService.Model.Google;
using WcfService.Utility;

namespace WcfService.Controller
{
    public class CommonController : BaseController
    {
        public Response Test()
        {
            response.payload = javaScriptSerializer.Serialize(commonDao.Test());
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetPickupError()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var pickupErrId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["id"];
            if(pickupErrId != null)
            {
                var result = pickupErrDao.Get(pickupErrId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = pickupErrDao.Get();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }


            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }


        public Response GetDeliveryError()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var deliverErrId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["id"];

            if(deliverErrId != null)
            {
                var result = deliveryErrDao.Get(deliverErrId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = deliveryErrDao.Get();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetFleetType()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var fleetTypeId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["fleetTypeId"];
            if(fleetTypeId != null)
            {
                var result = fleetTypeDao.Get(fleetTypeId);
                if(result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EResourceNotFoundError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = fleetTypeDao.Get();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetPermission()
        {
            var result = permissionDao.Get();
            if(result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetState()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var countryId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["countryId"];
            if(countryId != null)
            {
                var result = stateDao.GetByCountryId(countryId);
                if(result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = stateDao.Get();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetCountry()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var countryId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["countryId"];
            if (countryId != null)
            {
                var result = countryDao.GetCountry(countryId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = countryDao.GetCountries();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetActivity()
        {
            var result = activityDao.Get();
            if (result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobStatusType()
        {
            var result = jobStatusDao.Get();
            if (result == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            response.payload = javaScriptSerializer.Serialize(result);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response GetJobTypes()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var jobTypeId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["jobTypeId"];

            if(jobTypeId != null)
            {
                var result = jobTypeDao.GetById(jobTypeId);
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }
            else
            {
                var result = jobTypeDao.Get();
                if (result == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }
                response.payload = javaScriptSerializer.Serialize(result);
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response CheckPostcodeValidation(string deliverFrom, string deliverTo)
        {
            // send check address to google map api service
            try
            {
                string result = "";
                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?region=my&address={0}", deliverFrom);
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // by calling .Result you are performing a synchronous call
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        result = responseContent.ReadAsStringAsync().Result;
                    }
                }

                MapsGeocode jsonObj = JsonConvert.DeserializeObject<MapsGeocode>(result);
                if(jsonObj.status.CompareTo("OK") != 0)
                {
                    response.payload = javaScriptSerializer.Serialize(jsonObj.status);
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeNotValid);
                    return response;
                }
                var representAddFrom = jsonObj.results.Find(t => t.formatted_address.Contains("Malaysia"));
                if(representAddFrom == null)
                {
                    response.payload = result;
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeNotValid);
                    return response;
                }

                // validate the available postcode
                string[] supportedFrom = supportedAreaDao.GetFrom();
                var validFrom = false;
                foreach (Model.Google.Address locationFrom in representAddFrom.address_components)
                {
                    if (Utils.ContainsAny(locationFrom.long_name, supportedFrom))
                    {
                        validFrom = true;
                        break;
                    }
                }

                if(false == validFrom)
                {
                    DBLogger.GetInstance().Log(DBLogger.ESeverity.Analytic, string.Format("From Not Supported: {0}", representAddFrom.formatted_address));
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeFromNotSupport);
                    return response;
                }

                AddressComponents representAddTo = null;
                if (deliverTo != null)
                {
                    url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?region=my&address={0}", deliverTo);
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            // by calling .Result you are performing a synchronous call
                            var responseContent = response.Content;

                            // by calling .Result you are synchronously reading the result
                            result = responseContent.ReadAsStringAsync().Result;
                        }
                    }

                    jsonObj = JsonConvert.DeserializeObject<MapsGeocode>(result);
                    if (jsonObj.status.CompareTo("OK") != 0)
                    {
                        response.payload = javaScriptSerializer.Serialize(jsonObj.status);
                        response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeNotValid);
                        return response;
                    }

                    representAddTo = jsonObj.results.Find(t => t.formatted_address.Contains("Malaysia"));
                    if (representAddTo == null)
                    {
                        response.payload = result;
                        response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeNotValid);
                        return response;
                    }

                    // validate the available postcode
                    string[] supportedTo = supportedAreaDao.GetTo();
                    var validTo = false;
                    foreach (Model.Google.Address locationFrom in representAddFrom.address_components)
                    {
                        if (Utils.ContainsAny(locationFrom.long_name, supportedFrom))
                        {
                            validTo = true;
                            break;
                        }
                    }

                    if (false == validTo)
                    {
                        DBLogger.GetInstance().Log(DBLogger.ESeverity.Analytic, string.Format("To Not Supported: {0}", representAddTo.formatted_address));
                        response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EPostcodeToNotSupport);
                        return response;
                    }
                }

                PostcodeList postCodeList = new PostcodeList()
                {
                    postCodeAddFrom = representAddFrom.formatted_address,
                    postCodeAddTo = representAddTo.formatted_address
                };

                DBLogger.GetInstance().Log(DBLogger.ESeverity.Analytic, string.Format("From Interested: {0}", representAddFrom.formatted_address));
                DBLogger.GetInstance().Log(DBLogger.ESeverity.Analytic, string.Format("To Interested: {0}", representAddTo.formatted_address));

                // calculate distance between postcode only for standard delivery
                if (representAddFrom != null &&
                    representAddTo != null)
                {
                    url = string.Format("https://maps.googleapis.com/maps/api/directions/json?region=my&origin={0}&destination={1}&mode=driving", deliverFrom, deliverTo);
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            // by calling .Result you are performing a synchronous call
                            var responseContent = response.Content;

                            // by calling .Result you are synchronously reading the result
                            result = responseContent.ReadAsStringAsync().Result;
                        }
                    }

                    Direction directionObj = JsonConvert.DeserializeObject<Direction>(result);
                    if (directionObj.status.CompareTo("OK") != 0)
                    {
                        response.payload = javaScriptSerializer.Serialize(directionObj.status);
                    }

                    postCodeList.distance = directionObj.routes[0].legs[0].distance.value;
                    postCodeList.duration = directionObj.routes[0].legs[0].duration.value;
                }

                response.payload = javaScriptSerializer.Serialize(postCodeList);
            }
            catch (Exception)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }
            
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Model.Response GeneratePriceDisposal()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var lorryType = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["lorryType"];
            var fromBuildingType = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["fromBuildingType"];

            var labor = 1;
            float transportCost = 0;
            switch (int.Parse(lorryType))
            {
                case (int)Configuration.LorryType.Lorry_1tonne:
                    labor = 2;
                    transportCost = 120;
                    break;
                case (int)Configuration.LorryType.Lorry_3tonne:
                    labor = 3;
                    transportCost = 230;
                    break;
                case (int)Configuration.LorryType.Lorry_5tonne:
                    labor = 3;
                    transportCost = 300;
                    break;
                default:
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                    return response;
            }

            PriceDetails priceDetails = new PriceDetails();
            priceDetails.total = transportCost;

            // calculate for labors
            var additionalService = deliveryAdditionalDao.Get();
            DeliveryExtraService laborCost = null;

            // extra labor cost
            if(fromBuildingType == null)
            {
                fromBuildingType = "1";
            }
            if (int.Parse(fromBuildingType) == (int)Configuration.BuildingType.HighRise_nolift)
            {
                laborCost = additionalService.Find(t => t.name.CompareTo("labor-highrise-nolift") == 0);
            }
            else if (int.Parse(fromBuildingType) == (int)Configuration.BuildingType.HighRise_lift)
            {
                laborCost = additionalService.Find(t => t.name.CompareTo("labor-highrise-lift") == 0);
            }
            else
            {
                laborCost = additionalService.Find(t => t.name.CompareTo("labor-landed") == 0);
            }

            if (laborCost == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                return response;
            }

            var addCost = labor * laborCost.value;
            priceDetails.total += addCost;
            priceDetails.labor = addCost;

            // breakdown cost
            priceDetails.fuel = 0.35f * transportCost;
            priceDetails.maintenance = 0.25f * transportCost;
            priceDetails.labor += 0.15f * transportCost;
            priceDetails.partner = 0.15f * transportCost;
            priceDetails.justlorry = 0.10f * transportCost;

            response.payload = javaScriptSerializer.Serialize(priceDetails);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Model.Response GeneratePrice()
        {
            if (WebOperationContext.Current == null)
            {
                response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.EParameterError);
                return response;
            }

            var distance = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["distance"];
            var lorryType = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["lorryType"];
            var fromBuildingType = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["fromBuildingType"];
            var toBuildingType = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["toBuildingType"];
            var labor = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["labor"];

            var assembleBed = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["assembleBed"];
            var assemblyDining = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["assemblyDiningCount"];
            var assemblyWardrobe = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["assemblyWardrobeCount"];
            var assemblyTable = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["assemblyTableCount"];

            var promoCode = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["promoCode"];

            if (distance == null ||
                lorryType == null)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            var transportCost = deliveryPriceDao.GetPrice(distance, lorryType);
            if (transportCost == 0)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EParameterError);
                return response;
            }

            PriceDetails priceDetails = new PriceDetails();
            priceDetails.total = transportCost;

            var additionalService = deliveryAdditionalDao.Get();
            if (fromBuildingType != null &&
                toBuildingType != null &&
                labor != null)
            {
                DeliveryExtraService laborCost = null;

                // extra labor cost
                if (int.Parse(fromBuildingType) == (int)Configuration.BuildingType.HighRise_nolift ||
                    int.Parse(toBuildingType) == (int)Configuration.BuildingType.HighRise_nolift)
                {
                    laborCost = additionalService.Find(t => t.name.CompareTo("labor-highrise-nolift") == 0);
                }
                else if (int.Parse(fromBuildingType) == (int)Configuration.BuildingType.HighRise_lift ||
                    int.Parse(toBuildingType) == (int)Configuration.BuildingType.HighRise_lift)
                {
                    laborCost = additionalService.Find(t => t.name.CompareTo("labor-highrise-lift") == 0);
                }
                else
                {
                    laborCost = additionalService.Find(t => t.name.CompareTo("labor-landed") == 0);
                }

                if (laborCost == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                var addCost = int.Parse(labor) * laborCost.value;
                priceDetails.total += addCost;
                priceDetails.labor = addCost;
            }

            if(assembleBed != null)
            {
                var cost = additionalService.Find(t => t.name.CompareTo("assemble-bed") == 0);
                if(cost == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                var addCost = int.Parse(assembleBed) * cost.value;
                priceDetails.total += addCost;
                priceDetails.labor += addCost;
            }

            if (assemblyDining != null)
            {
                var cost = additionalService.Find(t => t.name.CompareTo("assemble-diningtable") == 0);
                if (cost == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                var addCost = int.Parse(assemblyDining) * cost.value;
                priceDetails.total += addCost;
                priceDetails.labor += addCost;
            }


            if (assemblyTable != null)
            {
                var cost = additionalService.Find(t => t.name.CompareTo("assemble-table") == 0);
                if (cost == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                var addCost = int.Parse(assemblyTable) * cost.value;
                priceDetails.total += addCost;
                priceDetails.labor += addCost;
            }

            if (assemblyTable != null)
            {
                var cost = additionalService.Find(t => t.name.CompareTo("assemble-wardrobe") == 0);
                if (cost == null)
                {
                    response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EGeneralError);
                    return response;
                }

                var addCost = int.Parse(assemblyTable) * cost.value;
                priceDetails.total += addCost;
                priceDetails.labor += addCost;
            }

            // break down the cost
            priceDetails.fuel = 0.35f * transportCost;
            priceDetails.maintenance = 0.25f * transportCost;
            priceDetails.labor += 0.15f * transportCost;
            priceDetails.partner = 0.15f * transportCost;
            priceDetails.justlorry = 0.10f * transportCost;

            // discount voucher
            if(promoCode != null)
            {
                var voucherResult = new Vouchers();
                var responseCode = validateVoucher(promoCode, priceDetails.total, out voucherResult);
                if(responseCode == Constant.ErrorCode.ESuccess)
                {
                    if (int.Parse(voucherResult.voucherType.id) == (int)Configuration.VoucherType.Percentage)
                    {
                        var discountedValue = voucherResult.discountValue * priceDetails.total;
                        if (discountedValue > voucherResult.maximumDiscount)
                        {
                            discountedValue = voucherResult.maximumDiscount;
                        }

                        priceDetails.total -= discountedValue;
                        priceDetails.discount = discountedValue;
                        priceDetails.discountRate = (int)voucherResult.discountValue;
                    }
                    else if (int.Parse(voucherResult.voucherType.id) == (int)Configuration.VoucherType.Value)
                    {
                        var discountedValue = voucherResult.discountValue;
                        if (discountedValue > voucherResult.maximumDiscount)
                        {
                            discountedValue = voucherResult.maximumDiscount;
                        }

                        priceDetails.discountRate = (int)(voucherResult.discountValue / priceDetails.total);
                        priceDetails.total -= discountedValue;
                        priceDetails.discount = discountedValue;
                    }
                }
            }

            response.payload = javaScriptSerializer.Serialize(priceDetails);
            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        public Response ValidateVoucher(string promoCode)
        {
            var result = voucherDao.GetByVoucherCode(promoCode);
            if (result == null ||
                result.enabled == false)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EVoucherNotValid);
                return response;
            }

            DateTime startDate = DateTime.Parse(result.startDate);
            DateTime endDate = DateTime.Parse(result.endDate);

            if (DateTime.Now.CompareTo(startDate) < 0 ||
                DateTime.Now.CompareTo(endDate) > 0)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EVoucherExpired);
                return response;
            }

            if (result.used >= result.quantity)
            {
                response = Utility.Utils.SetResponse(response, false, Constant.ErrorCode.EVoucherRedemptionLimit);
                return response;
            }

            response = Utility.Utils.SetResponse(response, true, Constant.ErrorCode.ESuccess);
            return response;
        }

        private int validateVoucher(string promoCode, float totalSpending, out Vouchers result)
        {
            result = voucherDao.GetByVoucherCode(promoCode);
            if(result == null ||
                result.enabled == false)
            {
                return Constant.ErrorCode.EVoucherNotValid;
            }

            DateTime startDate = DateTime.ParseExact(result.startDate, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            DateTime endDate = DateTime.ParseExact(result.startDate, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            if (DateTime.Now.CompareTo(startDate) < 0 ||
                DateTime.Now.CompareTo(endDate) > 0)
            {
                return Constant.ErrorCode.EVoucherExpired;
            }

            if(result.minimumPurchase != -1 &&
                totalSpending < result.minimumPurchase)
            {
                return Constant.ErrorCode.EVoucherMinimumRequired;
            }

            if(result.used >=  result.quantity)
            {
                return Constant.ErrorCode.EVoucherRedemptionLimit;
            }

            return Constant.ErrorCode.ESuccess;
        }
    }
}