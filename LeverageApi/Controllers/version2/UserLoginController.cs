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
using Raven.Client;
using System.Threading.Tasks;

namespace LeverageApi.Controllers.Version2
{
    public class UserLoginController : RavenDbController
    {

        // GET UserLogin Resource
        public List<Resource> GetResources(string Resource) {
            var resource = new RenderResource<UserLogin>(new UserLogin());
            return resource.GetResource();
        }

        public IEnumerable<UserLogin> Get() {
            return Session.Query<UserLogin>();
        }

        public Task<UserLogin> GetById(Guid id) {
            var entity = Session.LoadAsync<UserLogin>(id);
            return entity;
        }

        public async Task<HttpResponseMessage> Post(UserLogin userLogin) {
            await Session.StoreAsync(userLogin);
            return Request.CreateResponse(HttpStatusCode.Created, userLogin);
        }

        public async Task<HttpResponseMessage> Put(Guid id, UserLogin userLogin) {
            await Session.StoreAsync(userLogin);
            return Request.CreateResponse(HttpStatusCode.Created, userLogin);
        }

        public HttpResponseMessage Delete(Guid id) {
            UserLogin entity = Session.LoadAsync<UserLogin>(id).Result;
            if (entity == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Session.Delete<UserLogin>(entity);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

    }
}