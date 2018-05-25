
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using System.Web.Http.Description;
using Take_A_Lot_webAPI.Models;

namespace Take_A_Lot_webAPI.Controllers
{
    public class TblproductController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Tblproduct
        public IQueryable<object> GetTblproducts()
        {
            
            return db.Tblproducts.Select(x => new { x.productID, x.name, x.model, x.quantity, x.ProductImage });
        }

        [HttpGet]
        [Route("api/GetbyCategory")]
        public IQueryable<Tblproduct> GetbyCategory(string category)
        {
            var cat = db.Tblproducts.Where(x => x.category == category);
         
            return cat;
        }

        // GET: api/supplier/5
        [ResponseType(typeof(Tblproduct))]
        public IHttpActionResult Getproduct(int id)
        {
            Tblproduct product = db.Tblproducts.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Tblproduct/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblproduct(int id, Tblproduct tblproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblproduct.productID)
            {
                return BadRequest();
            }

            var pr = db.Tblproducts.SingleOrDefault(x => x.productID == id);
      
            if (pr != null)
            {
                pr.quantity = tblproduct.quantity;
                pr.price = tblproduct.price;
             
            }


           

         

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblproductExists(id))
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

        // POST: api/Tblproduct
        [ResponseType(typeof(Tblproduct))]
        public IHttpActionResult PostTblproduct(Tblproduct tblproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tblproducts.Add(tblproduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblproduct.productID }, tblproduct);
        }

        // DELETE: api/Tblproduct/5
        [ResponseType(typeof(Tblproduct))]
        public IHttpActionResult DeleteTblproduct(int id)
        {
            Tblproduct tblproduct = db.Tblproducts.Find(id);
            if (tblproduct == null)
            {
                return NotFound();
            }

            db.Tblproducts.Remove(tblproduct);
            db.SaveChanges();

            return Ok(tblproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblproductExists(int id)
        {
            return db.Tblproducts.Count(e => e.productID == id) > 0;
        }
    }
}