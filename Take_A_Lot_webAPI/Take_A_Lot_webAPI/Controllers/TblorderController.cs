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
    public class TblorderController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tblorder
        public IQueryable<Tblorder> GetTblorders()
        {
            return db.Tblorders;
        }
        [HttpGet]
        [Route("api/GetTblorderbyid")]
        public IQueryable<Tblorder> GetTblorderbyid(int id)
        {
            var orders = db.Tblorders.Where(a => a.customerID == id);
            return orders;
        }

        // GET: api/Tblorder/5
        [ResponseType(typeof(Tblorder))]
        public IHttpActionResult GetTblorder(int id)
        {
            //var order = db.Tblorders
            // .FirstOrDefault(c => c.customerID == id);
            var order = (from r in db.Tblorders orderby r.OrderID descending select r).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Tblorder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblorder(int id, Tblorder tblorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblorder.OrderID)
            {
                return BadRequest();
            }

            var ord = db.Tblorders.SingleOrDefault(x => x.OrderID == id);

            if (ord != null)
            {
                ord.DeliveryStatus = tblorder.DeliveryStatus;
                
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblorderExists(id))
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

        // POST: api/Tblorder
        [ResponseType(typeof(Tblorder))]
        public IHttpActionResult PostTblorder(Tblorder tblorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tblorders.Add(tblorder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblorder.OrderID }, tblorder);
        }

        // DELETE: api/Tblorder/5
        [ResponseType(typeof(Tblorder))]
        public IHttpActionResult DeleteTblorder(int id)
        {
            Tblorder tblorder = db.Tblorders.Find(id);
           
            var items = db.OrderItems.Where(x => x.OrderID == id);
            db.OrderItems.RemoveRange(items);
            db.SaveChanges();

            db.Tblorders.Remove(tblorder);
            db.SaveChanges();

            return Ok(tblorder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblorderExists(int id)
        {
            return db.Tblorders.Count(e => e.OrderID == id) > 0;
        }
    }
}