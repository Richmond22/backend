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
    public class TblScheduleController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/TblSchedule
        public IQueryable<TblSchedule> GetTblSchedules()
        {
            return db.TblSchedules;
        }

        // GET: api/TblSchedule/5
        [ResponseType(typeof(TblSchedule))]
        public IHttpActionResult GetTblSchedule(int id)
        {
            TblSchedule tblSchedule = db.TblSchedules.Find(id);
            if (tblSchedule == null)
            {
                return NotFound();
            }

            return Ok(tblSchedule);
        }

        // PUT: api/TblSchedule/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblSchedule(int id, TblSchedule tblSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblSchedule.scheduleID)
            {
                return BadRequest();
            }

            db.Entry(tblSchedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblScheduleExists(id))
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

        // POST: api/TblSchedule
        [ResponseType(typeof(TblSchedule))]
        public IHttpActionResult PostTblSchedule(TblSchedule tblSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TblSchedules.Add(tblSchedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblSchedule.scheduleID }, tblSchedule);
        }

        // DELETE: api/TblSchedule/5
        [ResponseType(typeof(TblSchedule))]
        public IHttpActionResult DeleteTblSchedule(int id)
        {
            TblSchedule tblSchedule = db.TblSchedules.Find(id);
            if (tblSchedule == null)
            {
                return NotFound();
            }

            db.TblSchedules.Remove(tblSchedule);
            db.SaveChanges();

            return Ok(tblSchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblScheduleExists(int id)
        {
            return db.TblSchedules.Count(e => e.scheduleID == id) > 0;
        }
    }
}