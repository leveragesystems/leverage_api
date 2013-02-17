using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Context;
using DbLayer.Repositories;

namespace LeverageApi.Controllers.version2 {
	public class ToDoListsController : ApiController {

		MongoRepository<ToDoList, Guid> db = new MongoRepository<ToDoList, Guid>() {
			ConnectionString = "mongodb://localhost/database"
		};

    // GET Resource
    public List<Resource> GetResources(string Resource) {
      RenderResource<ToDoList> resource = new RenderResource<ToDoList>(new ToDoList());
      // Call the Write method.
      return resource.GetResource();
    }

      // GET api/ToDoList
      public IEnumerable<ToDoList> GetToDoLists() {
        return db.Get();
      }
		

      // GET api/ToDoList/5
			public ToDoList GetToDoList(Guid id) {
        return db.Get(id);
      }

			public HttpResponseMessage PostToDoList(ToDoList model) {
        if (ModelState.IsValid) {
          db.Create(model);

          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
          response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = model.Id }));
          return response;
        } else {
          return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
      }

			public HttpResponseMessage DeleteToDoList(Guid id) {
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
