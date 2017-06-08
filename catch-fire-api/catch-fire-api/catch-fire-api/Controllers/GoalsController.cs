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
    public class GoalsController : ApiController
    {
        private CatchFireEntities db = new CatchFireEntities();

        // GET: api/Goals
        public IQueryable<Goal> GetGoals()
        {
            return db.Goals;
        }

        // GET: api/Goals/5
        [ResponseType(typeof(Goal))]
        public async Task<IHttpActionResult> GetGoal(int id)
        {
            Goal goal = await db.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // PUT: api/Goals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGoal(int id, Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.GoalId)
            {
                return BadRequest();
            }

            db.Entry(goal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(id))
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

        // POST: api/Goals
        [ResponseType(typeof(Goal))]
        public async Task<IHttpActionResult> PostGoal(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Goals.Add(goal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = goal.GoalId }, goal);
        }

        // DELETE: api/Goals/5
        [ResponseType(typeof(Goal))]
        public async Task<IHttpActionResult> DeleteGoal(int id)
        {
            Goal goal = await db.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            db.Goals.Remove(goal);
            await db.SaveChangesAsync();

            return Ok(goal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoalExists(int id)
        {
            return db.Goals.Count(e => e.GoalId == id) > 0;
        }
    }
}