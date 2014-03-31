using System.Web.Optimization;

namespace DemoApp.Web.Angular.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/JS").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/bootstrap.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
						"~/Scripts/angular-file-uploader/angular-file-upload-shim.js",
						"~/Scripts/angular.js",
						"~/Scripts/angular-resource.js",
						"~/Scripts/angular-route.js",
						"~/Scripts/angular-file-upload.js"
					));


			bundles.Add(new ScriptBundle("~/bundles/Application").Include(
					  "~/Scripts/Application/Base.js",
					  "~/Scripts/Application/Models/Account.js",
					  "~/Scripts/Application/Models/Book.js",
					  "~/Scripts/Application/Models/Content.js",
					  "~/Scripts/Application/Models/Person.js",
					  "~/Scripts/Application/Services/BookService.js",
					  "~/Scripts/Application/Services/ContentService.js",
					  "~/Scripts/Application/Services/PersonService.js",
					  "~/Scripts/Application/Controllers/AccountController.js",
					  "~/Scripts/Application/Controllers/BookController.js",
					  "~/Scripts/Application/Controllers/ContentController.js",
					  "~/Scripts/Application/Controllers/NewBookController.js",
					  "~/Scripts/Application/Controllers/PersonController.js",
					  "~/Scripts/Application/Authentication.js",
					  "~/Scripts/Application/Application.js"

					  ));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap.css",
						"~/Content/Site.css"
					));
		}
	}
}