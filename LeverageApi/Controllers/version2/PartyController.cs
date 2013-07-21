using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Context;
using DbLayer.Repositories;
using System.Threading.Tasks;

namespace LeverageApi.Controllers.Version2 {
    public class PartyController : RavenDbController {

        // GET Party Resource
        public List<Resource> GetResources(string Resource) {
            var resource = new RenderResource<Party>(new Party());
            return resource.GetResource();
        }

        public IEnumerable<Party> Get() {
            return Session.Query<Party>();
        }

        public Task<Party> GetById(Guid id) {
            var entity = Session.LoadAsync<Party>(id);
            return entity;
        }

        public async Task<HttpResponseMessage> Post(Party party) {
            await Session.StoreAsync(party);
            return Request.CreateResponse(HttpStatusCode.Created, party);
        }

        public async Task<HttpResponseMessage> Put(Guid id, Party party) {
            await Session.StoreAsync(party);
            return Request.CreateResponse(HttpStatusCode.Created, party);
        }

        public HttpResponseMessage Delete(Guid id) {
            Party entity = Session.LoadAsync<Party>(id).Result;
            if (entity == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Session.Delete<Party>(entity);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }
    }
}