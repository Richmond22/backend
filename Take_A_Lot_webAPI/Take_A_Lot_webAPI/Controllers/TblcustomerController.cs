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
    public class TblcustomerController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tblcustomer
        public IQueryable<Tblcustomer> GetTblcustomers()
        {
            return db.Tblcustomers;
        }

        [HttpGet]
        [Route("api/getCust")]
        public object GetCust(string email)
        {
            var cust = db.Tblcustomers.Select(x => new { x.customerID, x.firstname, x.lastname, x.email, x.password, x.phone }).Where(x => x.email == email);
            return cust;

        }

        // GET: api/Tblcustomer/5

        [HttpGet]
        [Route("api/GetUserClaims")]
        public Tblcustomer GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            Tblcustomer model = new Tblcustomer()
            {
                customerID = Convert.ToInt32(identityClaims.FindFirst("customerID").Value),
                firstname = identityClaims.FindFirst("firstname").Value,
                lastname = identityClaims.FindFirst("lastname").Value,
                email = identityClaims.FindFirst("email").Value,
                password = identityClaims.FindFirst("password").Value,
                phone = identityClaims.FindFirst("phone").Value
                
            };
            return model;
        }

        // PUT: api/Tblcustomer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblcustomer(int id, Tblcustomer tblcustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblcustomer.customerID)
            {
                return BadRequest();
            }

            db.Entry(tblcustomer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblcustomerExists(id))
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

        // POST: api/Tblcustomer
        [ResponseType(typeof(Tblcustomer))]
        public IHttpActionResult PostTblcustomer(Tblcustomer tblcustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tblcustomers.Add(tblcustomer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblcustomer.customerID }, tblcustomer);
        }

        // DELETE: api/Tblcustomer/5
        [ResponseType(typeof(Tblcustomer))]
        public IHttpActionResult DeleteTblcustomer(int id)
        {
            Tblcustomer tblcustomer = db.Tblcustomers.Find(id);
            if (tblcustomer == null)
            {
                return NotFound();
            }

            db.Tblcustomers.Remove(tblcustomer);
            db.SaveChanges();

            return Ok(tblcustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblcustomerExists(int id)
        {
            return db.Tblcustomers.Count(e => e.customerID == id) > 0;
        }
    }
}