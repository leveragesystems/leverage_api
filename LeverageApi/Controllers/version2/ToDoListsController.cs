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
using DbLayer.Repositories;
using System.Threading.Tasks;

namespace LeverageApi.Controllers.Version2 {
  public class ToDoListsController : RavenDbController {

      // GET ToDoList Resource
      public List<Resource> GetResources(string Resource) {
          var resource = new RenderResource<ToDoList>(new ToDoList());
          return resource.GetResource();
      }

      public IEnumerable<ToDoList> Get() {
          return Session.Query<ToDoList>();
      }

      public Task<ToDoList> GetById(Guid id) {
          var entity = Session.LoadAsync<ToDoList>(id);
          return entity;
      }

      public async Task<HttpResponseMessage> Post(ToDoList toDoList) {
          await Session.StoreAsync(toDoList);
          return Request.CreateResponse(HttpStatusCode.Created, toDoList);
      }

      public async Task<HttpResponseMessage> Put(Guid id, ToDoList toDoList) {
          await Session.StoreAsync(toDoList);
          return Request.CreateResponse(HttpStatusCode.Created, toDoList);
      }

      public HttpResponseMessage Delete(Guid id) {
          ToDoList entity = Session.LoadAsync<ToDoList>(id).Result;
          if (entity == null) {
              return Request.CreateResponse(HttpStatusCode.NotFound);
          }
          Session.Delete<ToDoList>(entity);
          return Request.CreateResponse(HttpStatusCode.OK, entity);
      }
  }
}