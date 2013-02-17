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

namespace LeverageApi.Controllers.version1 {
  public class ToDoListsController : ApiController {
    private SQLContext db = new SQLContext();

    //public dynamic GetToDoLists_Resource() {
    //  return new ToDoList();
    //}
    // GET api/ToDoList
    public IEnumerable<ToDoList> GetToDoLists() {
      return db.ToDoLists.AsEnumerable();
    }

    public List<Resource> GetResources(string Resource) {
      RenderResource<ToDoList> resource = new RenderResource<ToDoList>(new ToDoList());
      // Call the Write method.
      return resource.GetResource();
    }


    // GET api/ToDoList/5
    public ToDoList GetToDoList(Guid id) {
      ToDoList todolist = db.ToDoLists.Find(id);
      if (todolist == null) {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }
      return todolist;
    }

    // PUT api/ToDoList/5
    public HttpResponseMessage PutToDoList(Guid id, ToDoList todolist) {
      if (ModelState.IsValid && id == todolist.Id) {
        db.Entry(todolist).State = EntityState.Modified;

        try {
          db.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
          return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        return Request.CreateResponse(HttpStatusCode.OK);
      } else {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // POST api/ToDoList
    public HttpResponseMessage PostToDoList(ToDoList todolist) {
      if (ModelState.IsValid) {
        db.ToDoLists.Add(todolist);
        db.SaveChanges();

        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todolist);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todolist.Id }));
        return response;
      } else {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // DELETE api/ToDoList/5
    public HttpResponseMessage DeleteToDoList(int id) {
      ToDoList todolist = db.ToDoLists.Find(id);
      if (todolist == null) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      db.ToDoLists.Remove(todolist);

      try {
        db.SaveChanges();
      } catch (DbUpdateConcurrencyException) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      return Request.CreateResponse(HttpStatusCode.OK, todolist);
    }

    protected override void Dispose(bool disposing) {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}