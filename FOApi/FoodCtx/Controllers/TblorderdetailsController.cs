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
    public class TblorderdetailsController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblorderdetailsController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblorderdetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblorderdetail>>> GetTblorderdetails()
        {
            return await _context.Tblorderdetails.ToListAsync();
        }

        // GET: api/Tblorderdetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblorderdetail>> GetTblorderdetail(int id)
        {
            var tblorderdetail = await _context.Tblorderdetails.FindAsync(id);

            if (tblorderdetail == null)
            {
                return NotFound();
            }

            return tblorderdetail;
        }

        // PUT: api/Tblorderdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblorderdetail(int id, Tblorderdetail tblorderdetail)
        {
            if (id != tblorderdetail.OrderDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(tblorderdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblorderdetailExists(id))
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

        // POST: api/Tblorderdetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblorderdetail>> PostTblorderdetail(Tblorderdetail tblorderdetail)
        {
            _context.Tblorderdetails.Add(tblorderdetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblorderdetail", new { id = tblorderdetail.OrderDetailsId }, tblorderdetail);
        }

        // DELETE: api/Tblorderdetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblorderdetail(int id)
        {
            var tblorderdetail = await _context.Tblorderdetails.FindAsync(id);
            if (tblorderdetail == null)
            {
                return NotFound();
            }

            _context.Tblorderdetails.Remove(tblorderdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblorderdetailExists(int id)
        {
            return _context.Tblorderdetails.Any(e => e.OrderDetailsId == id);
        }
    }
}
