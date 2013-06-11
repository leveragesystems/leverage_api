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
  public class PartyController : ApiController {
    MongoRepository<Party, Guid> db = new MongoRepository<Party, Guid>() {
      ConnectionString = WebApiConfig.MongoConnectionString,
      DataBaseName = WebApiConfig.DataBaseName
    };
    // GET Resource
    public List<Resource> GetResources(string Resource) {
      RenderResource<Party> resource = new RenderResource<Party>(new Party());
      // Call the Write method.
      return resource.GetResource();
    }

    // GET <#= routePrefix #>
    public IEnumerable<Party> GetPartys() {
      Seed s = new Seed();
      s.Create();
      return db.Get();
    }

    // GET <#= routePrefix #>/5
    public Party GetParty(Guid id) {
      Party party = db.Get(id);
      if (party == null) {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return party;
    }

    // GET <#= routePrefix #>/5
    public Party GetParty(Guid id, Guid userLoginId) {
      Party party = db.Get(id);
      if (party == null) {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return party;
    }


    // PUT <#= routePrefix #>/5
    public HttpResponseMessage PutParty(Guid id, Party party) {
      if (ModelState.IsValid) {
        db.Update(id, party);

        return Request.CreateResponse(HttpStatusCode.OK);
      }

      return Request.CreateResponse(HttpStatusCode.BadRequest);
    }

    // POST <#= routePrefix #>
    public HttpResponseMessage PostParty(Party party) {
      if (ModelState.IsValid) {
        db.Create(party);

        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, party);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = party.Id }));
        return response;
      } else {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // DELETE <#= routePrefix #>/5
    public HttpResponseMessage DeleteParty(Guid id) {
      Party party = db.Get(id);
      if (party == null) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      db.Delete(party);

      return Request.CreateResponse(HttpStatusCode.OK, party);
    }

    protected override void Dispose(bool disposing) {
      //db.Dispose();
      base.Dispose(disposing);
    }
  }
}