using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace LeverageApi {
  public static class WebApiConfig {
    public static void Register(HttpConfiguration config) {

      config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{version}/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
        name: "OneLevelNested",
        routeTemplate: "api/{version}/{controller}/{id}/action/{userLoginId}",
        defaults: new { id = RouteParameter.Optional }
      );

      //config.Routes.MapHttpRoute(
      //	name: "ResourceApi",
      //	routeTemplate: "api/{version}/{controller}/{resource}"
      //);
    }
    public static string MongoConnectionString = ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
    public static string DataBaseName = ConfigurationManager.AppSettings["databaseName"]; // ToDo: Strongly type this stringy appsettings stuff
  }
}
