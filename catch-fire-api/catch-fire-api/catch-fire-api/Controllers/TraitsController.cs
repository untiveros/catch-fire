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
using catch_fire_api;
using System.Web.Http.Cors;

namespace catch_fire_api.Controllers
{
    [EnableCorsAttribute("http://localhost:4200", "*", "*")]
    public class TraitsController : ApiController
    {
        private CatchFireEntities db = new CatchFireEntities();

        // GET: api/Traits
        public IQueryable<Trait> GetTraits()
        {
            return db.Traits;
        }

        // GET: api/Traits/5
        [ResponseType(typeof(Trait))]
        public async Task<IHttpActionResult> GetTrait(int id)
        {
            Trait trait = await db.Traits.FindAsync(id);
            if (trait == null)
            {
                return NotFound();
            }

            return Ok(trait);
        }

        // PUT: api/Traits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTrait(int id, Trait trait)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trait.TraitId)
            {
                return BadRequest();
            }

            db.Entry(trait).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraitExists(id))
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

        // POST: api/Traits
        [ResponseType(typeof(Trait))]
        public async Task<IHttpActionResult> PostTrait(Trait trait)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Traits.Add(trait);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = trait.TraitId }, trait);
        }

        // DELETE: api/Traits/5
        [ResponseType(typeof(Trait))]
        public async Task<IHttpActionResult> DeleteTrait(int id)
        {
            Trait trait = await db.Traits.FindAsync(id);
            if (trait == null)
            {
                return NotFound();
            }

            db.Traits.Remove(trait);
            await db.SaveChangesAsync();

            return Ok(trait);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TraitExists(int id)
        {
            return db.Traits.Count(e => e.TraitId == id) > 0;
        }
    }
}