namespace WcfService.Constant
{
    public class ErrorCode
    {
        public static int ESuccess = 200;

        public static int EGeneralError = 1000;
        public static int EUnknownError = 1001;

        // User error
        public static int ECredentialError = 2000;
        public static int ETokenError = 2001;
        public static int ETokenExpired = 2002;
        public static int EMultipleLogin = 2003;
        public static int EAccountDisabled = 2004;
        public static int EAccountDeleted = 2005;

    }
}