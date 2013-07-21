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
using System.Threading.Tasks;

namespace LeverageApi.Controllers.Version2
{
    public class CustomerController : RavenDbController {

        // GET Resource
        public List<Resource> GetResources(string Resource) {
            var resource = new RenderResource<Customer>(new Customer());
            return resource.GetResource();
        }

        public IEnumerable<Customer> Get() {
            return Session.Query<Customer>();
        }

        // GET <#= routePrefix #>
        public Task<Customer> GetById(Guid id) {
            var entity = Session.LoadAsync<Customer>(id);
            return entity;
        }

        public async Task<HttpResponseMessage> Post(Customer customer) {
            await Session.StoreAsync(customer);
            return Request.CreateResponse(HttpStatusCode.Created, customer);
        }

        public async Task<HttpResponseMessage> Put(Guid id, Customer customer) {
            await Session.StoreAsync(customer);
            return Request.CreateResponse(HttpStatusCode.Created, customer);
        }

        public HttpResponseMessage Delete(Guid id) {
            Customer entity = Session.LoadAsync<Customer>(id).Result;
            if (entity == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Session.Delete<Customer>(entity);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

    }
}