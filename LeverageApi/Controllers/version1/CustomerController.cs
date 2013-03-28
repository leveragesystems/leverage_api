﻿using System;
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
  public class CustomerController : ApiController {
    private SQLContext db = new SQLContext();

    // GET Resource
    public List<Resource> GetResources(string Resource) {
      RenderResource<Customer> resource = new RenderResource<Customer>(new Customer());
      // Call the Write method.
      return resource.GetResource();
    }

    // GET api/Customers
    public IEnumerable<Customer> GetCustomers() {
      return db.Customers.AsEnumerable();
    }

    // GET api/Customers/5
    public Customer GetCustomers(int id) {
      Customer customers = db.Customers.Find(id);
      if (customers == null) {
        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
      }

      return customers;
    }

    // PUT api/Customers/5
    public HttpResponseMessage PutCustomers(Guid id, Customer customer) {
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

    // POST api/Customers
    public HttpResponseMessage PostCustomers(Customer customer) {
      if (ModelState.IsValid) {
        db.Customers.Add(customer);
        db.SaveChanges();

        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.Id }));
        return response;
      } else {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
    }

    // DELETE api/Customers/5
    public HttpResponseMessage DeleteCustomers(int id) {
      Customer customers = db.Customers.Find(id);
      if (customers == null) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      db.Customers.Remove(customers);

      try {
        db.SaveChanges();
      } catch (DbUpdateConcurrencyException) {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      return Request.CreateResponse(HttpStatusCode.OK, customers);
    }

    protected override void Dispose(bool disposing) {
      db.Dispose();
      base.Dispose(disposing);
    }
  }
}