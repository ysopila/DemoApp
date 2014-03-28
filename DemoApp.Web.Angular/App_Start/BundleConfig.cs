using System.Web.Optimization;

namespace DemoApp.Web.Angular.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/JS").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/bootstrap.js",
						"~/Scripts/angular.js",
						"~/Scripts/angular-file-upload.js",
						"~/Scripts/angular-route.js",
						"~/Scripts/angular-resource.js"
						));


			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
						"~/Scripts/angular-file-uploader/angular-file-upload-shim.js",
						"~/Scripts/angular.js",
						"~/Scripts/angular-resource.js",
						"~/Scripts/angular-route.js",
						"~/Scripts/angular-file-upload.js"
					));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap.css",
						"~/Content/Site.css"
					));
		}
	}
}