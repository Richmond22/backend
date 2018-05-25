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
    public class OrderItemController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/OrderItem
        public IQueryable<Object> GetOrderItem(int id)
        {
            var list = db.Tblcustomers.
                  Join(db.Tblorders,
                  c => c.customerID, od => od.customerID,
                  (o, od) => new
                  {
                      customerID = o.customerID,
                      OrderID = od.OrderID,
                      date = od.date,
                      deliveryDate = od.deliveryDate,
                      totalCost = od.totalCost
                  }).Join(db.OrderItems,
                          a => a.OrderID, oi => oi.OrderID,
                          (a, oi) => new
                          {
                              customerID = a.customerID,
                              OrderID = a.OrderID,
                              date = a.date,
                              deliveryDate = a.deliveryDate,
                              totalCost = a.totalCost,
                              productID = oi.productID,
                              quantity = oi.quantity
                              
                          }).Join(db.Tblproducts,
                          a => a.productID, p => p.productID,
                          (a, p) => new
                          {
                              customerID = a.customerID,
                              OrderID = a.OrderID,
                              deliveryDate = a.deliveryDate,
                              totalCost = a.totalCost,
                              date = a.date,
                              quantity = a.quantity,
                              name = p.name,
                              model = p.model,
                              price = p.price,
                              imgName = p.ProductImage,
                              productID = p.productID
                          });

            var orderDetails = list.Where(c => c.OrderID.Equals(id));
            if (orderDetails == null)
            {
                return (null);
            }

            return orderDetails;
        }

        // GET: api/OrderItem/5
        [ResponseType(typeof(Object))]
        IQueryable<Object> GetOrderItem()
        {
            var list = db.Tblcustomers.
                  Join(db.Tblorders,
                  c => c.customerID, od => od.customerID,
                  (o, od) => new
                  {
                      customerID = o.customerID,
                      OrderID = od.OrderID,
                      date = od.date,
                      deliveryDate = od.deliveryDate,
                      totalCost = od.totalCost
                  }).Join(db.OrderItems,
                          a => a.OrderID, oi => oi.OrderID,
                          (a, oi) => new
                          {
                              customerID = a.customerID,
                              OrderID = a.OrderID,
                              date = a.date,
                              deliveryDate = a.deliveryDate,
                              totalCost = a.totalCost,
                              productID = oi.productID,
                              quantity = oi.quantity

                          }).Join(db.Tblproducts,
                          a => a.productID, p => p.productID,
                          (a, p) => new
                          {
                              customerID = a.customerID,
                              OrderID = a.OrderID,
                              deliveryDate = a.deliveryDate,
                              totalCost = a.totalCost,
                              date = a.date,
                              quantity = a.quantity,
                              name = p.name,
                              model = p.model,
                              price = p.price,
                              imgName = p.ProductImage,
                              productID = p.productID
                          });

            
            if (list == null)
            {
                return (null);
            }

            return list;
        }

        // PUT: api/OrderItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderItem(int id, OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderItem.itemlist)
            {
                return BadRequest();
            }

            db.Entry(orderItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/OrderItem
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult PostOrderItem(OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderItems.Add(orderItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderItem.itemlist }, orderItem);
        }

        // DELETE: api/OrderItem/5
        [ResponseType(typeof(OrderItem))]
        public IHttpActionResult DeleteOrderItem(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);


            if (orderItem == null)
            {
                return NotFound();
            }

            db.OrderItems.Remove(orderItem);
            db.SaveChanges();

            return Ok(orderItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderItemExists(int id)
        {
            return db.OrderItems.Count(e => e.itemlist == id) > 0;
        }
    }
}