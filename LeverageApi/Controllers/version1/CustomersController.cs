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
  public class CustomersController : ApiController {
    private SQLContext db = new SQLContext();

    // GET Resource
    public List<Resource> GetResources(string Resource) {
      RenderResource<Customer> resource = new RenderResource<Customer>(new Customer());
      // Call the Write method.
      return resource.GetResource();
    }

    // GET api/Customer
    public IEnumerable<Customer> GetCustomers() {
      return db.Customers.AsEnumerable();
    }

    // GET api/Customer/5
    public Customer GetCustomers(int id) {
      Customer customer = db.Customers.Find(id);
      if (customer == null) {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return customer;
    }

    // PUT api/Customer/5
    public HttpResponseMessage PutCustomers(int id, Customer customer) {
      if (ModelState.IsValid && id == customer.CustomersId) {
        db.Entry(customer).State = EntityState.Modified;

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

    // POST api/Customer
    public HttpResponseMessage PostCustomers(Customer customer) {
      if (ModelState.IsValid) {
        db.Customers.Add(customer);
        db.SaveChanges();

        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.CustomersId }));
        return response;
      } else {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // DELETE api/Customer/5
    public HttpResponseMessage DeleteCustomers(int id) {
      Customer customer = db.Customers.Find(id);
      if (customer == null) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      db.Customers.Remove(customer);

      try {
        db.SaveChanges();
      } catch (DbUpdateConcurrencyException) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      return Request.CreateResponse(HttpStatusCode.OK, customer);
    }

    protected override void Dispose(bool disposing) {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}