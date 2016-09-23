using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace WcfService
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "Require application token";
        private static string[] _tokens;

        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
            context.AuthorizeRequest += Context_AuthorizeRequest;

            // get all the username and tokens from web.config
            _tokens = ConfigurationManager.AppSettings["AuthorizationToken"].Split(';');
        }

        private void Context_AuthorizeRequest(object sender, EventArgs e)
        {
            Console.WriteLine(e);
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static void AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));
                foreach(string token in _tokens)
                {
                    if (token.CompareTo(credentials) == 0)
                    {
                        // found correct credential
                        return;
                    }
                }

                HttpContext.Current.Response.StatusCode = 401;
            }
            catch (Exception e)
            {
                // Credentials were not formatted correctly.
                Console.WriteLine(e.Message);
                HttpContext.Current.Response.StatusCode = 401;
            }
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
                else
                {
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            else
            {
                HttpContext.Current.Response.StatusCode = 401;
            }


        }

        // If the request was unauthorized, add the WWW-Authenticate header 
        // to the response.
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("WWW-Authenticate",
                    string.Format("Basic realm=\"{0}\"", Realm));
            }
        }

        public void Dispose()
        {
        }
    }
}