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

namespace LeverageApi.Controllers.version2
{
    public class UserLoginController : ApiController
    {
      MongoRepository<UserLogin, Guid> db = new MongoRepository<UserLogin, Guid>() {
      ConnectionString = WebApiConfig.MongoConnectionString,
      DataBaseName = WebApiConfig.DataBaseName
    };
				// GET Resource
        public List<Resource> GetResources(string Resource) {
					RenderResource<UserLogin> resource = new RenderResource<UserLogin>(new UserLogin());
					// Call the Write method.
					return resource.GetResource();
        }

        // GET <#= routePrefix #>
        public IEnumerable<UserLogin> GetUserLogins(){
				 return db.Get();
        }

        // GET <#= routePrefix #>/5
        public UserLogin GetUserLogin(Guid id)
        {
            UserLogin UserLogin = db.Get(id);
            if (UserLogin == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return UserLogin;
        }

        // PUT <#= routePrefix #>/5
        public HttpResponseMessage PutUserLogin(Guid id, UserLogin UserLogin)
        {
            if (ModelState.IsValid)
            {
              db.Update(id, UserLogin);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST <#= routePrefix #>
        public HttpResponseMessage PostUserLogin(UserLogin UserLogin)
        {
            if (ModelState.IsValid)
            {
                db.Create(UserLogin);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, UserLogin);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = UserLogin.UserLoginId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE <#= routePrefix #>/5
        public HttpResponseMessage DeleteUserLogin(Guid id)
        {
            UserLogin UserLogin = db.Get(id);
            if (UserLogin == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Delete(UserLogin);

            return Request.CreateResponse(HttpStatusCode.OK, UserLogin);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}