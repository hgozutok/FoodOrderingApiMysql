#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FOApi;

namespace FOApi.FoodCtx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblratingsController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblratingsController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblrating>>> GetTblratings()
        {
            return await _context.Tblratings.ToListAsync();
        }

        // GET: api/Tblratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblrating>> GetTblrating(int id)
        {
            var tblrating = await _context.Tblratings.FindAsync(id);

            if (tblrating == null)
            {
                return NotFound();
            }

            return tblrating;
        }

        // PUT: api/Tblratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblrating(int id, Tblrating tblrating)
        {
            if (id != tblrating.RatingId)
            {
                return BadRequest();
            }

            _context.Entry(tblrating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblratingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tblratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblrating>> PostTblrating(Tblrating tblrating)
        {
            _context.Tblratings.Add(tblrating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblrating", new { id = tblrating.RatingId }, tblrating);
        }

        // DELETE: api/Tblratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblrating(int id)
        {
            var tblrating = await _context.Tblratings.FindAsync(id);
            if (tblrating == null)
            {
                return NotFound();
            }

            _context.Tblratings.Remove(tblrating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblratingExists(int id)
        {
            return _context.Tblratings.Any(e => e.RatingId == id);
        }
    }
}
