using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Repositories;

namespace LeverageApi.Controllers.version2 {
  public class ToDoListsController : ApiController {
    readonly IRepository<ToDoList, Guid> _db = new MongoRepository<ToDoList, Guid>() {
      ConnectionString = WebApiConfig.MongoConnectionString,
      DataBaseName = WebApiConfig.DataBaseName
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ToDoListsController"/> class.
    /// </summary>
    /// <param name="db">The db.</param>
    public ToDoListsController(IRepository<ToDoList, Guid> db)
    {
      // ToDo: Add DI framework
      _db = db;
    }

    // GET Resource
    /// <summary>
    /// Gets the resources.
    /// </summary>
    /// <param name="Resource">The resource.</param>
    /// <returns></returns>
    public List<Resource> GetResources(string Resource) {
      var resource = new RenderResource<ToDoList>(new ToDoList());
      // Call the Write method.
      return resource.GetResource();
    }

    // GET api/ToDoList
    /// <summary>
    /// Gets to do lists.
    /// </summary>
    /// <returns>All the to do List items</returns>
    public IEnumerable<ToDoList> GetToDoLists() {
      return _db.Get();
    }


    // GET api/ToDoList/5
    /// <summary>
    /// Gets to do list.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A to do list by Id</returns>
    public ToDoList GetToDoList(Guid id) {
      return _db.Get(id);
    }

    /// <summary>
    /// Posts to do list.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A HttpStatusCode based on the result of the DB call</returns>
    public HttpResponseMessage PostToDoList(ToDoList model)
    {
      if (ModelState.IsValid) {
        _db.Create(model);

        var response = Request.CreateResponse(HttpStatusCode.Created, model);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = model.Id }));
        return response;
      }

      return Request.CreateResponse(HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// Deletes to do list.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A HttpStatusCode based on the result of the DB call</returns>
    public HttpResponseMessage DeleteToDoList(Guid id) {
      var dbModel = _db.Get(id);
      //var dbModel = db.SingleOrDefault(p => p.Id == id);
      if (dbModel == null) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }
      _db.Delete(dbModel);

      return Request.CreateResponse(HttpStatusCode.OK, dbModel);
    }
  }
}
