using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Repositories;

namespace LeverageApi.Controllers.version2
{
    public class PersonController : ApiController
    {
      IRepository<Person, Guid> db = new MongoRepository<Person, Guid>() {
          ConnectionString = WebApiConfig.MongoConnectionString,
          DataBaseName = WebApiConfig.DataBaseName
      };

      // GET api/ToDoList
      public IEnumerable<Person> GetPeople() {
        return db.Get();
      }

      public List<Resource> GetResources(string Resource) {
        RenderResource<Person> resource = new RenderResource<Person>(new Person());
        // Call the Write method.
        return resource.GetResource();
      }


      // GET api/ToDoList/5
      public Person GetPerson(Guid id) {
        return db.Get(id);
      }

      public HttpResponseMessage PostPerson(Person model) {
        if (ModelState.IsValid) {
          db.Create(model);

          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
          response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = model.Id }));
          return response;
        } else {
          return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
      }

      public HttpResponseMessage DeletePerson(Guid id) {
        var dbModel = db.Get(id);
        //var dbModel = db.SingleOrDefault(p => p.Id == id);
        if (dbModel == null) {
          return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        db.Delete(dbModel);

        return Request.CreateResponse(HttpStatusCode.OK, dbModel);
      }
    }
}
