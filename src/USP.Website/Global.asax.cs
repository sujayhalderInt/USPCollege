using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Logging;
using Umbraco.Web;

namespace USP.Website
{
    public class MvcApplication : UmbracoApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override void OnApplicationError(object sender, EventArgs e)
        {
            var error = Server.GetLastError();

            while (error.InnerException != null) error = error.InnerException;

            // Log Error
            string message;
            try
            {
                message = "An unhandled exception occured at " + Request.Url + " - " + error.Message;
            }
            catch
            {
                message = "An unhandled exception occured " + error.Message;
            }

            LogHelper.Error<ArgumentException>(message, error);
        }
    }
}
