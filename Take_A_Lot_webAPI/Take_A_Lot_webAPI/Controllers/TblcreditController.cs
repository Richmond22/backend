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
    public class TblcreditController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tblcredit
        public IQueryable<Tblcredit> GetTblcredits()
        {
            return db.Tblcredits;
        }

        // GET: api/Tblcredit/5
        [ResponseType(typeof(Tblcredit))]
        public IHttpActionResult GetTblcredit(int id)
        {
            var tblcredit = db.Tblcredits
                 .FirstOrDefault(c => c.paymentID == id); 
            if (tblcredit == null)
            {
                return NotFound();
            }

            return Ok(tblcredit);
        }

        // PUT: api/Tblcredit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblcredit(int id, Tblcredit tblcredit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblcredit.ID)
            {
                return BadRequest();
            }

            db.Entry(tblcredit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblcreditExists(id))
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

        // POST: api/Tblcredit
        [ResponseType(typeof(Tblcredit))]
        public IHttpActionResult PostTblcredit(Tblcredit tblcredit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tblcredits.Add(tblcredit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblcredit.ID }, tblcredit);
        }

        // DELETE: api/Tblcredit/5
        [ResponseType(typeof(Tblcredit))]
        public IHttpActionResult DeleteTblcredit(int id)
        {
            Tblcredit tblcredit = db.Tblcredits.Find(id);
            if (tblcredit == null)
            {
                return NotFound();
            }

            db.Tblcredits.Remove(tblcredit);
            db.SaveChanges();

            return Ok(tblcredit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblcreditExists(int id)
        {
            return db.Tblcredits.Count(e => e.ID == id) > 0;
        }
    }
}