using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbLayer.Models;

namespace LeverageApi {
  public class RenderResource<T> where T : new() {
    
    T type;

    public RenderResource(T t) {
        // The field has the same type as the parameter.
			this.type = t;
      }

    public List<Resource> GetResource() {
			var objectType = this.type.GetType();
      var properties = objectType.GetProperties();
      var resourceList = new List<Resource>();
      foreach (var property in properties) {
        var resource = new Resource();
        resource.Property = property.Name;
        resource.Type = property.PropertyType.Name;
        if (property.GetCustomAttributes(true).Length > 0) {
          List<Validation> validationList = new List<Validation>();
          foreach (var item in property.GetCustomAttributes(true)) {
            Validation validation = new Validation();
            validation.Reg = ((ValidateAttribute)(item)).Regex;
            validation.Message = ((ValidateAttribute)(item)).Message;
            validationList.Add(validation);
            
          }
          resource.Validations = validationList;
//resource.Required = ((DbLayer.Models.Require)(property.GetCustomAttributes(true)[0])).Required;
        }
        resourceList.Add(resource);
      }
      return resourceList;
    }



  }
}
