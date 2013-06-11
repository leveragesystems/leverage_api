using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LeverageApi.Filter {
  public class ApiAuthorization : AuthorizationFilterAttribute {

    public override void OnAuthorization(HttpActionContext actionContext) {
     
    }
  }
}