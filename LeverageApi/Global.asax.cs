using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using SDammann.WebApi.Versioning;

namespace LeverageApi {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {

			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
																													new RouteVersionedControllerSelector(GlobalConfiguration.Configuration));

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}

