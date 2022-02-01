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
    public class TblordersController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblordersController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblorders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblorder>>> GetTblorders()
        {
            return await _context.Tblorders.ToListAsync();
        }

        // GET: api/Tblorders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblorder>> GetTblorder(int id)
        {
            var tblorder = await _context.Tblorders.FindAsync(id);

            if (tblorder == null)
            {
                return NotFound();
            }

            return tblorder;
        }

        // PUT: api/Tblorders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblorder(int id, Tblorder tblorder)
        {
            if (id != tblorder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(tblorder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblorderExists(id))
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

        // POST: api/Tblorders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblorder>> PostTblorder(Tblorder tblorder)
        {
            _context.Tblorders.Add(tblorder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblorder", new { id = tblorder.OrderId }, tblorder);
        }

        // DELETE: api/Tblorders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblorder(int id)
        {
            var tblorder = await _context.Tblorders.FindAsync(id);
            if (tblorder == null)
            {
                return NotFound();
            }

            _context.Tblorders.Remove(tblorder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblorderExists(int id)
        {
            return _context.Tblorders.Any(e => e.OrderId == id);
        }
    }
}
