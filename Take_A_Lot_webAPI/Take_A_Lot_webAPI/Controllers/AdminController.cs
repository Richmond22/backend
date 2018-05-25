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

namespace Take_A_Lot_webAPI.Models
{
    public class AdminController : ApiController
    {
        private DBmodel db = new DBmodel();

        // GET: api/Admin
        public IQueryable<Admin> GetAdmins()
        {
            return db.Admins;
        }

        [HttpGet]
        [Route("api/GetAd")]
        public object GetAd(string email)
        {
            var admin = db.Admins.Where(x => x.email == email);
            return admin;

        }

        // GET: api/supplier/5
        [HttpGet]
        [Route("api/Getadmin")]
        [ResponseType(typeof(supplier))]
        public Admin Getadmin()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            Admin model = new Admin()
            {
                
                firstname = identityClaims.FindFirst("firstname").Value,
                ID = Convert.ToInt32(identityClaims.FindFirst("ID").Value),


            };
            return model;
        }

        // GET: api/Admin/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetAdmin(string id)
        {
            var admin = db.Admins.Where(Admin => Admin.email.Equals(id));
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // PUT: api/Admin/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdmin(int id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.ID)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admin
        [ResponseType(typeof(Admin))]
        public IHttpActionResult PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Admins.Add(admin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = admin.ID }, admin);
        }

        // DELETE: api/Admin/5
        [ResponseType(typeof(Admin))]
        public IHttpActionResult DeleteAdmin(int id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.Admins.Remove(admin);
            db.SaveChanges();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(int id)
        {
            return db.Admins.Count(e => e.ID == id) > 0;
        }
    }
}