using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbLayer.Models;

namespace LeverageApi {
  public class RenderResource<T> {
    
    T value;

    public RenderResource(T t) {
        // The field has the same type as the parameter.
        this.value = t;
      }

    public List<Resource> GetResource() {

      var objectType = this.value.GetType();
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
