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
    public class TblmenutypesController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblmenutypesController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblmenutypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblmenutype>>> GetTblmenutypes()
        {
            return await _context.Tblmenutypes.ToListAsync();
        }

        // GET: api/Tblmenutypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblmenutype>> GetTblmenutype(int id)
        {
            var tblmenutype = await _context.Tblmenutypes.FindAsync(id);

            if (tblmenutype == null)
            {
                return NotFound();
            }

            return tblmenutype;
        }

        // PUT: api/Tblmenutypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblmenutype(int id, Tblmenutype tblmenutype)
        {
            if (id != tblmenutype.MenuTypeId)
            {
                return BadRequest();
            }

            _context.Entry(tblmenutype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblmenutypeExists(id))
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

        // POST: api/Tblmenutypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblmenutype>> PostTblmenutype(Tblmenutype tblmenutype)
        {
            _context.Tblmenutypes.Add(tblmenutype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblmenutype", new { id = tblmenutype.MenuTypeId }, tblmenutype);
        }

        // DELETE: api/Tblmenutypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblmenutype(int id)
        {
            var tblmenutype = await _context.Tblmenutypes.FindAsync(id);
            if (tblmenutype == null)
            {
                return NotFound();
            }

            _context.Tblmenutypes.Remove(tblmenutype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblmenutypeExists(int id)
        {
            return _context.Tblmenutypes.Any(e => e.MenuTypeId == id);
        }
    }
}
