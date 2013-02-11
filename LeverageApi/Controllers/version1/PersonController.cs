using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Repositories;

namespace LeverageApi.Controllers.version1
{
    public class PersonController : ApiController
    {
      MongoRepository<Person,int> db = new MongoRepository<Person, int>() {
        ConnectionString = "mongodb://localhost/database"
      };

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

      public HttpResponseMessage DeletePerson(int person_id) {
        
        var dbModel = db.SingleOrDefault(p => p.Id == person_id);
        if (dbModel == null) {
          return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        db.Delete(person_id);

        return Request.CreateResponse(HttpStatusCode.OK, dbModel);
      }
    }
}
