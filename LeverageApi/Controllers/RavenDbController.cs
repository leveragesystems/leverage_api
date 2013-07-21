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
using Raven.Client.Document;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Threading;

namespace LeverageApi {
    public abstract class RavenDbController : ApiController {
        public IDocumentStore Store {
            get { return LazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() => {
            var docStore = new DocumentStore {
                Url = "http://localhost:8080",
                DefaultDatabase = "WebApiSample"
            };

            docStore.Initialize();
            return docStore;
        });

        public IAsyncDocumentSession Session { get; set; }

        public async override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken) {
            using (Session = Store.OpenAsyncSession()) {
                var result = await base.ExecuteAsync(controllerContext, cancellationToken);
                await Session.SaveChangesAsync();

                return result;
            }
        }
    }
}