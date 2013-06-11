using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using SDammann.WebApi.Versioning;

namespace LeverageApi {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {

      GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
      GlobalConfiguration.Configuration.Formatters[0].SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
																												new RouteVersionedControllerSelector(GlobalConfiguration.Configuration));

      //Read http://chimera.labs.oreilly.com/books/1234000001708/ch08.html#fig_ch11_controllerpipeline, to undersatnd filters!
      GlobalConfiguration.Configuration.Filters.Add(new Filter.UnhandledFilterAttibute());
      GlobalConfiguration.Configuration.Filters.Add(new Filter.ApiAuthorization());
      GlobalConfiguration.Configuration.Filters.Add(new Filter.CacheActionFilter());
      GlobalConfiguration.Configuration.Filters.Add(new Filter.VerifyModelStateActionFilter());


			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}
	}
}

