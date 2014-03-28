using DemoApp.Api.Handlers;
using System.Web.Http;

namespace DemoApp.Web
{
    public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional },
				constraints: null,
				handler: new ApiHandler(config)
			);
		}
    }
}
