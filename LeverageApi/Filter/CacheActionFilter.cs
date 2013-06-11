using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LeverageApi.Filter {
  public class CacheActionFilter : ActionFilterAttribute {

    public override void OnActionExecuted(HttpActionExecutedContext c) {
      object returnVal = null;
      var oc = c.Response.Content as ObjectContent;
      if (oc != null)
        returnVal = oc.Value;

      Trace.TraceInformation("Ran action {0}::{1} with result {2}",
        c.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
        c.ActionContext.ActionDescriptor.ActionName,
        returnVal ?? string.Empty);
    }
  }
}