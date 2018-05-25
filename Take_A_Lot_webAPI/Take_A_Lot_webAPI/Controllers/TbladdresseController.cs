using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Take_A_Lot_webAPI.Models;

namespace Take_A_Lot_webAPI.Controllers
{
    public class TbladdresseController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tbladdresse
        public IQueryable<Tbladdress> GetTbladdresses()
        {
            return db.Tbladdresses;
        }

        // GET: api/Tbladdresse/5
        [ResponseType(typeof(Tbladdress))]
        public IHttpActionResult GetTbladdress(int id)
        {
            var address = db.Tbladdresses
               .FirstOrDefault(c => c.customerID == id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Tbladdresse/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbladdress(int id, Tbladdress tbladdress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbladdress.addressID)
            {
                return BadRequest();
            }

            db.Entry(tbladdress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbladdressExists(id))
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

        // POST: api/Tbladdresse
        [ResponseType(typeof(Tbladdress))]
        public IHttpActionResult PostTbladdress(Tbladdress tbladdress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbladdresses.Add(tbladdress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbladdress.addressID }, tbladdress);
        }

        // DELETE: api/Tbladdresse/5
        [ResponseType(typeof(Tbladdress))]
        public IHttpActionResult DeleteTbladdress(int id)
        {
            Tbladdress tbladdress = db.Tbladdresses.Find(id);
            if (tbladdress == null)
            {
                return NotFound();
            }

            db.Tbladdresses.Remove(tbladdress);
            db.SaveChanges();

            return Ok(tbladdress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TbladdressExists(int id)
        {
            return db.Tbladdresses.Count(e => e.addressID == id) > 0;
        }
    }
}