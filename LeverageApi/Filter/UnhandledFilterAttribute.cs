using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace LeverageApi.Filter {
  public class UnhandledFilterAttibute: ExceptionFilterAttribute {

    public override void OnException(HttpActionExecutedContext context) {
      if (context.Exception is NotImplementedException) {
        context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
      } else {
        var errorResponse = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Please contact your server administrator for more details.");
        context.Response = errorResponse;
      }
    }

  }
}
