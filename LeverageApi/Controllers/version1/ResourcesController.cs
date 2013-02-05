using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;

namespace LeverageApi.Controllers.version1 {
  public class ResourcesController : ApiController {

    public List<Resource> GetResources(string ResourceName) {
      dynamic typeObject = ResourceName;
      var objectType = new ToDoList().GetType();
      var properties = objectType.GetProperties();
      var resourceList = new List<Resource>();
      foreach (var property in properties) {
        var resource = new Resource();
        resource.Name = property.Name;
        resource.Value = property.PropertyType.Name;
        if (property.GetCustomAttributes(true).Length > 0) {
          resource.Required = ((DbLayer.Models.Require)(property.GetCustomAttributes(true)[0])).Required;
        }
        resourceList.Add(resource);
      }
      return resourceList;
    }

  }
}
