using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DemoApp.Web.Angular
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override void Init()
        {
            base.Init();
            Bootstrapper.Initialise();
        }

        public void Init(HttpApplication application)
        {
            application.PostAuthenticateRequest += application_PostAuthenticateRequest;
        }

        void application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var authTicket = FormsAuthentication.Decrypt(encTicket);
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), null);
                }
            }
        }
    }
}