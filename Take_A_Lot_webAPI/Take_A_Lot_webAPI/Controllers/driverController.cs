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
    public class driverController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/driver
        public IQueryable<driver> Getdrivers()
        {
            return db.drivers;
        }
        [HttpGet]
        [Route("api/Getdriv")]
        public object Getdriv(string email)
        {
            var admin = db.drivers.Where(x => x.email == email);
            return admin;

        }

        // GET: api/supplier/5
        [HttpGet]
        [Route("api/Getdriver")]
        [ResponseType(typeof(supplier))]
        public driver Getdriver()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            driver model = new driver()
            {
                
                firstname = identityClaims.FindFirst("firstname").Value,
                ID = Convert.ToInt32( identityClaims.FindFirst("ID").Value),


            };
            return model;
        }

        // GET: api/driver/5
        [ResponseType(typeof(driver))]
        public IHttpActionResult Getdriver(int id)
        {
            driver driver = db.drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/driver/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdriver(int id, driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.ID)
            {
                return BadRequest();
            }

            db.Entry(driver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!driverExists(id))
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

        // POST: api/driver
        [ResponseType(typeof(driver))]
        public IHttpActionResult Postdriver(driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.drivers.Add(driver);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (driverExists(driver.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = driver.ID }, driver);
        }

        // DELETE: api/driver/5
        [ResponseType(typeof(driver))]
        public IHttpActionResult Deletedriver(int id)
        {
            driver driver = db.drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            db.drivers.Remove(driver);
            db.SaveChanges();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool driverExists(int id)
        {
            return db.drivers.Count(e => e.ID == id) > 0;
        }
    }
}