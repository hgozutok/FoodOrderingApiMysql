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
    public class TblmenusController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblmenusController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblmenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblmenu>>> GetTblmenus()
        {
            return await _context.Tblmenus.ToListAsync();
        }

        // GET: api/Tblmenus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblmenu>> GetTblmenu(int id)
        {
            var tblmenu = await _context.Tblmenus.FindAsync(id);

            if (tblmenu == null)
            {
                return NotFound();
            }

            return tblmenu;
        }

        // PUT: api/Tblmenus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblmenu(int id, Tblmenu tblmenu)
        {
            if (id != tblmenu.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(tblmenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblmenuExists(id))
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

        // POST: api/Tblmenus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblmenu>> PostTblmenu(Tblmenu tblmenu)
        {
            _context.Tblmenus.Add(tblmenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblmenu", new { id = tblmenu.MenuId }, tblmenu);
        }

        // DELETE: api/Tblmenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblmenu(int id)
        {
            var tblmenu = await _context.Tblmenus.FindAsync(id);
            if (tblmenu == null)
            {
                return NotFound();
            }

            _context.Tblmenus.Remove(tblmenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblmenuExists(int id)
        {
            return _context.Tblmenus.Any(e => e.MenuId == id);
        }
    }
}
