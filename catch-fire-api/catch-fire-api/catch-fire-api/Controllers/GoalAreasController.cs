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

namespace catch_fire_api.Controllers
{
    public class GoalAreasController : ApiController
    {
        private CatchFireEntities db = new CatchFireEntities();

        // GET: api/GoalAreas
        public IQueryable<GoalArea> GetGoalAreas()
        {
            return db.GoalAreas;
        }

        // GET: api/GoalAreas/5
        [ResponseType(typeof(GoalArea))]
        public async Task<IHttpActionResult> GetGoalArea(int id)
        {
            GoalArea goalArea = await db.GoalAreas.FindAsync(id);
            if (goalArea == null)
            {
                return NotFound();
            }

            return Ok(goalArea);
        }

        // PUT: api/GoalAreas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGoalArea(int id, GoalArea goalArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goalArea.GoalAreaId)
            {
                return BadRequest();
            }

            db.Entry(goalArea).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalAreaExists(id))
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

        // POST: api/GoalAreas
        [ResponseType(typeof(GoalArea))]
        public async Task<IHttpActionResult> PostGoalArea(GoalArea goalArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GoalAreas.Add(goalArea);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = goalArea.GoalAreaId }, goalArea);
        }

        // DELETE: api/GoalAreas/5
        [ResponseType(typeof(GoalArea))]
        public async Task<IHttpActionResult> DeleteGoalArea(int id)
        {
            GoalArea goalArea = await db.GoalAreas.FindAsync(id);
            if (goalArea == null)
            {
                return NotFound();
            }

            db.GoalAreas.Remove(goalArea);
            await db.SaveChangesAsync();

            return Ok(goalArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoalAreaExists(int id)
        {
            return db.GoalAreas.Count(e => e.GoalAreaId == id) > 0;
        }
    }
}