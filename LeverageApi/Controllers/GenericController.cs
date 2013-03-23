using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbLayer.Models;
using DbLayer.Repositories;

namespace LeverageApi.Controllers {
  public class GenericController<T, TId> : ApiController where T : Entity<TId>, new()
  {
    private readonly IRepository<T, TId> _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericController{T, TId}"/> class.
    /// </summary>
    public GenericController()
    {
      _db = new MongoRepository<T, TId>() {
                  ConnectionString = WebApiConfig.MongoConnectionString,
                  DataBaseName = WebApiConfig.DataBaseName
                };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericController{T, TId}"/> class.
    /// </summary>
    /// <param name="db">The db.</param>
    public GenericController(IRepository<T, TId> db)
    {
      // ToDo: Add DI framework
      _db = db;
    }

    // GET Resource
    /// <summary>
    /// Gets the resources.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <returns></returns>
    public List<Resource> GetResources(string resource) {
      var resources = new RenderResource<T>(new T());
      // Call the Write method.
      return resources.GetResource();
    }

    // GET api/ToDoList
    /// <summary>
    /// Gets to do lists.
    /// </summary>
    /// <returns>All the items</returns>
    public IEnumerable<T> GetList() {
      return _db.Get();
    }


    // GET api/ToDoList/5
    /// <summary>
    /// Gets to do list.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>An item by Id</returns>
    public T GetById(Guid id) {
      return _db.Get(id);
    }

    /// <summary>
    /// Posts to do list.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A HttpStatusCode based on the result of the DB call</returns>
    public HttpResponseMessage Post(T model) {
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
    public HttpResponseMessage Delete(Guid id) {
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
