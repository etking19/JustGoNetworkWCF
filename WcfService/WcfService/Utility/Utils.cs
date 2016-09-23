using WcfService.Model;

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
    }
}