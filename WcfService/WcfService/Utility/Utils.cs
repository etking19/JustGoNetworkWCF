using Newtonsoft.Json;
using System.Net.Http;
using WcfService.Model;
using WcfService.Model.Google;

namespace WcfService.Utility
{
    public class Utils
    {
        public static Response SetResponse(Response response, bool success, int errorCode)
        {
            response.success = success;
            response.errorCode = errorCode;
            response.errorMessage = Constant.ErrorMsg.GetInstance().GetErrorMsg(errorCode);

            if(success == false)
            {
                response.payload = null;
            }
            return response;
        }

        public static bool ContainsAny(string haystack, string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }

        public static AddressComponents GetGpsCoordinate(string address)
        {
            string result = "";
            address = address.Replace(" ", "+");
            string url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?region=my&address={0}", address);
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
            if (jsonObj.status.CompareTo("OK") != 0)
            {
                return null;
            }

            var representAddFrom = jsonObj.results.Find(t => t.formatted_address.Contains("Malaysia"));
            if (representAddFrom == null)
            {
                return null;
            }

            return representAddFrom;
        }

        public static AddressComponents GetGpsCoordinate(string add1, string add2, string add3, string poscode)
        {
            string result = "";
            string address = add1 + ", " + add2 + ", " + add3;
            int retryCount = 3;
            do {
                address = address.Replace(" ", "+");
                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?region=my&address={0}", address);
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
                do
                {
                    if (jsonObj.status.CompareTo("OK") != 0)
                    {
                        break;
                    }

                    var representAddFrom = jsonObj.results.Find(t => t.formatted_address.Contains("Malaysia"));
                    if (representAddFrom == null)
                    {
                        break;
                    }

                    return representAddFrom;

                } while (true);

                switch (retryCount)
                {
                    case 3:
                        address = add2 + ", " + add3;
                        break;
                    case 2:
                        address = add3;
                        break;
                    case 1:
                        address = poscode;
                        break;
                }
                retryCount--;

            } while (retryCount >= 0) ;

            return null;
        }
    }
}