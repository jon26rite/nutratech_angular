using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using vikaro_angular.Models;

namespace vikaro_angular.Controllers
{
    public class CustomerController : ApiController
    {
        private VikaroContext db = new VikaroContext();

        // GET: api/Customer
        public IQueryable<Business> GetBusinesses()
        {
            return db.Businesses.Where(a => a.BusinessType.Equals("customer"));
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Business))]
        public async Task<IHttpActionResult> GetBusiness(int id)
        {
            //Business business = await db.Businesses.FindAsync(id);
            Business business = await db.Businesses.Where(a => a.BusinessType.Equals("customer")).SingleOrDefaultAsync(a => a.Id == id);
            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBusiness(int id, Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != business.Id)
            {
                return BadRequest();
            }

            db.Entry(business).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(id))
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

        // POST: api/Customer
        [ResponseType(typeof(Business))]
        public async Task<IHttpActionResult> PostBusiness(Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Businesses.Add(business);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = business.Id }, business);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Business))]
        public async Task<IHttpActionResult> DeleteBusiness(int id)
        {
            Business business = await db.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            db.Businesses.Remove(business);
            await db.SaveChangesAsync();

            return Ok(business);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessExists(int id)
        {
            return db.Businesses.Count(e => e.Id == id) > 0;
        }
    }
}