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
using System.Web.Script.Serialization;

namespace vikaro_angular.Controllers
{
    public class JournalsController : ApiController
    {
        private VikaroContext db = new VikaroContext();


        // GET: api/CustomerTypes/SearchKey
        [Route("api/Journals/SearchKey/{SearchKey}")]
        [HttpGet]
        public List<JournalDTO> FindJournals(String SearchKey)
        {
            List<JournalDTO> result = null;
            if (String.IsNullOrEmpty(SearchKey))
            {
                return result;
            }
            result = (from c in db.Journals
                      where c.Description.ToUpper().Contains(SearchKey.ToUpper()) ||
                          //c.TransactionDate.ToShortDateString().ToUpper().Contains(SearchKey.ToUpper()) ||
                            c.Account.Name.ToUpper().Contains(SearchKey.ToUpper())
                      select new JournalDTO()
                      {
                          Id = c.Id,
                          Description = c.Description,
                          TransactionDate = c.TransactionDate,
                          DebitCreditMode = c.DebitCreditMode,
                          Amount = c.Amount,
                          Account = new AccountDTO { 
                                        Id = c.Account.Id,
                                        Name = c.Account.Name
                                    }
                      }).ToList();

            return result;
        }

        // GET: api/Journals
        public List<JournalDTO> GetJournals()
        {
            return (from j in db.Journals
                    select new JournalDTO()
                    {
                        Id = j.Id,
                        TransactionDate = j.TransactionDate,
                        Description = j.Description,
                        DebitCreditMode = j.DebitCreditMode,
                        Amount = j.Amount,
                        Account = new AccountDTO { 
                                    Id = j.Account.Id,
                                    Name = j.Account.Name
                                  }
                    }).ToList();
        }

        //// GET: api/Journals
        //public IQueryable<Journal> GetJournals()
        //{
        //    return db.Journals;
        //}

        // GET: api/Journals/5
        [ResponseType(typeof(Journal))]
        public async Task<IHttpActionResult> GetJournal(int id)
        {
            Journal journal = await db.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            return Ok(journal);
        }

        // PUT: api/Journals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJournal(int id, Journal journal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != journal.Id)
            {
                return BadRequest();
            }

            db.Entry(journal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalExists(id))
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

        // POST: api/Journals
        [ResponseType(typeof(Journal))]
        public async Task<IHttpActionResult> PostJournal(Journal journal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            System.Diagnostics.Debug.WriteLine("The journal is " + new JavaScriptSerializer().Serialize(journal));
            db.Journals.Add(journal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = journal.Id }, journal);
        }

        // DELETE: api/Journals/5
        [ResponseType(typeof(Journal))]
        public async Task<IHttpActionResult> DeleteJournal(int id)
        {
            Journal journal = await db.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            db.Journals.Remove(journal);
            await db.SaveChangesAsync();

            return Ok(journal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JournalExists(int id)
        {
            return db.Journals.Count(e => e.Id == id) > 0;
        }
    }
}