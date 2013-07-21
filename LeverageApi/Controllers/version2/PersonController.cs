using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Repositories;
using System.Threading.Tasks;

namespace LeverageApi.Controllers.version2
{
    public class PersonController : RavenDbController
    {

        // GET Person Resource
        public List<Resource> GetResources(string Resource) {
            var resource = new RenderResource<Person>(new Person());
            return resource.GetResource();
        }

        public IEnumerable<Person> Get() {
            return Session.Query<Person>();
        }

        public Task<Person> GetById(Guid id) {
            var entity = Session.LoadAsync<Person>(id);
            return entity;
        }

        public async Task<HttpResponseMessage> Post(Person person) {
            await Session.StoreAsync(person);
            return Request.CreateResponse(HttpStatusCode.Created, person);
        }

        public async Task<HttpResponseMessage> Put(Guid id, Person person) {
            await Session.StoreAsync(person);
            return Request.CreateResponse(HttpStatusCode.Created, person);
        }

        public HttpResponseMessage Delete(Guid id) {
            Person entity = Session.LoadAsync<Person>(id).Result;
            if (entity == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Session.Delete<Person>(entity);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }
    }
}
