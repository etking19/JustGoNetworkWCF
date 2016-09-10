using System;
using System.ServiceModel.Activation;
using System.Web.Routing;
using WcfService.Utility;
using WebHttpCors;

namespace WcfService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           // RouteTable.Routes.Add(new ServiceRoute("api", new CorsWebServiceHostFactory(), typeof(AjaxService)));
            DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, "Application_Start");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //DBLogger.GetInstance().Log(DBLogger.ESeverity.Info, "Application_BeginRequest");
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{
            //    var headers = HttpContext.Current.Request.Headers["Access-Control-Request-Headers"];

            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", headers);
            //    HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            //    HttpContext.Current.Response.End();
            //}
        }
    }
}