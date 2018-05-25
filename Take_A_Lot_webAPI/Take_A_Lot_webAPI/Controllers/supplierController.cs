using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Take_A_Lot_webAPI.Models;

namespace Take_A_Lot_webAPI.Controllers
{
    public class supplierController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/supplier
        public IQueryable<supplier> Getsuppliers()
        {
            return db.suppliers;
        }

        [HttpGet]
        [Route("api/GetSup")]
        public object Getsp(string email)
        {
            var admin = db.suppliers.Where(x => x.email == email);
            return admin;

        }

        // GET: api/supplier/5
        [HttpGet]
        [Route("api/Getsupplier")]
        [ResponseType(typeof(supplier))]
        public supplier Getsupplier()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            supplier model = new supplier()
            {
                ID = Convert.ToInt32(identityClaims.FindFirst("ID").Value),
                firstname = identityClaims.FindFirst("firstname").Value,
               

            };
            return model;
        }
        public IHttpActionResult GetSupplier(int id)
        {
           
            var supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // PUT: api/supplier/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsupplier(int id, supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.ID)
            {
                return BadRequest();
            }

            db.Entry(supplier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!supplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/supplier
        [ResponseType(typeof(supplier))]
        public IHttpActionResult Postsupplier(supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.suppliers.Add(supplier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplier.ID }, supplier);
        }

        // DELETE: api/supplier/5
        [ResponseType(typeof(supplier))]
        public IHttpActionResult Deletesupplier(int id)
        {
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            db.suppliers.Remove(supplier);
            db.SaveChanges();

            return Ok(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool supplierExists(int id)
        {
            return db.suppliers.Count(e => e.ID == id) > 0;
        }
    }
}