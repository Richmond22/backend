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
    public class TblcartController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tblcart/
        public IQueryable<Object> GetTblcarts(int id)
        {
            var list = db.Tblcustomers.
                Join(db.Tblcarts,
                o => o.customerID, od => od.customerID,
                (o, od) => new
                {
                    customerID = o.customerID,
                    cartID = od.cartID,
                    quantity = od.quantity,
                    productID = od.productID
                }).Join(db.Tblproducts,
                        a => a.productID, p => p.productID,
                        (a, p) => new
                        {
                            customerID = a.customerID,
                            cartID = a.cartID,
                            name = p.name,
                            quantity = a.quantity,
                            model = p.model,
                            price = p.price,
                            imgName = p.ProductImage,
                            productID = p.productID
                        });

            var cart = list.Where(c=> c.customerID.Equals(id));
            if (cart == null)
            {
                return (null);
            }

            return cart;
            //return db.Tblcarts;
        }

       
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblcart(int id, Tblcart tblcart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblcart.cartID)
            {
                return BadRequest();
            }

            db.Entry(tblcart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblcartExists(id))
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

        // POST: api/Tblcart
        [ResponseType(typeof(Tblcart))]
        public IHttpActionResult PostTblcart(Tblcart tblcart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tblcarts.Add(tblcart);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblcart.cartID }, tblcart);
        }

        // DELETE: api/Tblcart/5
        [ResponseType(typeof(Tblcart))]
        public IHttpActionResult DeleteTblcart(int id)
        {
            Tblcart tblcart = db.Tblcarts.Find(id);
            if (tblcart == null)
            {
                return NotFound();
            }

            db.Tblcarts.Remove(tblcart);
            db.SaveChanges();

            return Ok(tblcart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblcartExists(int id)
        {
            return db.Tblcarts.Count(e => e.cartID == id) > 0;
        }
    }
}