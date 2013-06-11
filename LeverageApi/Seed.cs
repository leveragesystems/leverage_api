using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer.Models;
using DbLayer.Repositories;

namespace LeverageApi {

  public class Seed {

    public void Create() {
      MongoRepository<Party, Guid> db = new MongoRepository<Party, Guid>() {
        ConnectionString = WebApiConfig.MongoConnectionString,
        DataBaseName = WebApiConfig.DataBaseName
      };
      Party party= new Party();
      db.Create(party);
      UserLogin userLogin = new UserLogin();
      userLogin.party = party;
      userLogin.Username = "Mike";
      userLogin.Password = "mic001";
      db.Create(userLogin);
    }
  }
}
