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
using Web_ban_giay;

namespace Web_ban_giay.ApiControllers
{
    public class NHACUNGCAPController : ApiController
    {
        private QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();

        // GET: api/NHACUNGCAP
        public IQueryable<NHACUNGCAP> GetNHACUNGCAPs()
        {
            return db.NHACUNGCAPs;
        }

        // GET: api/NHACUNGCAP/5
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> GetNHACUNGCAP(string id)
        {
            NHACUNGCAP nHACUNGCAP = await db.NHACUNGCAPs.FindAsync(id);
            if (nHACUNGCAP == null)
            {
                return NotFound();
            }

            return Ok(nHACUNGCAP);
        }

        // PUT: api/NHACUNGCAP/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNHACUNGCAP(string id, NHACUNGCAP nHACUNGCAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nHACUNGCAP.MANHACUNGCAP)
            {
                return BadRequest();
            }

            db.Entry(nHACUNGCAP).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NHACUNGCAPExists(id))
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

        // POST: api/NHACUNGCAP
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> PostNHACUNGCAP(NHACUNGCAP nHACUNGCAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NHACUNGCAPs.Add(nHACUNGCAP);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NHACUNGCAPExists(nHACUNGCAP.MANHACUNGCAP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nHACUNGCAP.MANHACUNGCAP }, nHACUNGCAP);
        }

        // DELETE: api/NHACUNGCAP/5
        [ResponseType(typeof(NHACUNGCAP))]
        public async Task<IHttpActionResult> DeleteNHACUNGCAP(string id)
        {
            NHACUNGCAP nHACUNGCAP = await db.NHACUNGCAPs.FindAsync(id);
            if (nHACUNGCAP == null)
            {
                return NotFound();
            }

            db.NHACUNGCAPs.Remove(nHACUNGCAP);
            await db.SaveChangesAsync();

            return Ok(nHACUNGCAP);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NHACUNGCAPExists(string id)
        {
            return db.NHACUNGCAPs.Count(e => e.MANHACUNGCAP == id) > 0;
        }
    }
}