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
    public class CustomerTypesController : ApiController
    {
        private VikaroContext db = new VikaroContext();


        // GET: api/CustomerTypes/SearchKey
        [Route("api/CustomerTypes/SearchKey/{SearchKey}")]        
        [HttpGet]
        public IQueryable<CustomerType> FindCustomerTypes(String SearchKey)
        {
            IQueryable<CustomerType> result = null;
            if (String.IsNullOrEmpty(SearchKey)) {
                return result;
            }
            result =  from c in db.CustomerTypes
                       where c.Description.ToUpper().Contains(SearchKey.ToUpper())
                       select c;
            
            return result;
        }

        // GET: api/CustomerTypes
        public IQueryable<CustomerType> GetCustomerTypes()
        {
            return db.CustomerTypes;
        }

        // GET: api/CustomerTypes/5
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> GetCustomerType(int? id)
        {
            CustomerType customerType = await db.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }

            return Ok(customerType);
        }

        // PUT: api/CustomerTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerType(int id, CustomerType customerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerType.Id)
            {
                return BadRequest();
            }

            db.Entry(customerType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTypeExists(id))
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

        // POST: api/CustomerTypes
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> PostCustomerType(CustomerType customerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerTypes.Add(customerType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customerType.Id }, customerType);
        }

        // DELETE: api/CustomerTypes/5
        [ResponseType(typeof(CustomerType))]
        public async Task<IHttpActionResult> DeleteCustomerType(int id)
        {
            CustomerType customerType = await db.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }

            db.CustomerTypes.Remove(customerType);
            await db.SaveChangesAsync();

            return Ok(customerType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerTypeExists(int id)
        {
            return db.CustomerTypes.Count(e => e.Id == id) > 0;
        }
    }
}