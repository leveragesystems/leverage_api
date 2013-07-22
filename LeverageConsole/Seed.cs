using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer.Models;
using DbLayer.Repositories;
using Raven.Client;
using Raven.Client.Document;

namespace LeverageApi {

  public class Seed {

    DocumentStore documentStore;

    public Seed() {

        documentStore = new DocumentStore {
            Url = "http://localhost:8080",
            DefaultDatabase = "WebApi"
        };
        documentStore.Initialize();
    }

    public void Create() {

        // Saving changes using the session API
        using (IDocumentSession session = documentStore.OpenSession()) {
            // Operations against session

            Party p = new Party();
            UserLogin userLogin = new UserLogin();
            userLogin.Username = "dev1";
            userLogin.Password = "dev1";
            userLogin.party = p;
            session.Store(p);
            session.Store(userLogin);
            // Flush those changes
            session.SaveChanges();
        }
      Console.WriteLine("Completed");

    }
  }
}
