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
    public class eftController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/eft
        public IQueryable<eft> Getefts()
        {
            return db.efts;
        }

        // GET: api/eft/5
        [ResponseType(typeof(eft))]
        public IHttpActionResult Geteft(int id)
        {
            var eft = db.efts
                 .FirstOrDefault(c => c.paymentID == id);
            if (eft == null)
            {
                return NotFound();
            }

            return Ok(eft);
        }

        // PUT: api/eft/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puteft(int id, eft eft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eft.ID)
            {
                return BadRequest();
            }

            db.Entry(eft).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eftExists(id))
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

        // POST: api/eft
        [ResponseType(typeof(eft))]
        public IHttpActionResult Posteft(eft eft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.efts.Add(eft);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eft.ID }, eft);
        }

        // DELETE: api/eft/5
        [ResponseType(typeof(eft))]
        public IHttpActionResult Deleteeft(int id)
        {
            eft eft = db.efts.Find(id);
            if (eft == null)
            {
                return NotFound();
            }

            db.efts.Remove(eft);
            db.SaveChanges();

            return Ok(eft);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eftExists(int id)
        {
            return db.efts.Count(e => e.ID == id) > 0;
        }
    }
}