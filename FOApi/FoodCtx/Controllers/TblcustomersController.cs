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
    public class TblcustomersController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblcustomersController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblcustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblcustomer>>> GetTblcustomers()
        {
            return await _context.Tblcustomers.ToListAsync();
        }

        // GET: api/Tblcustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblcustomer>> GetTblcustomer(int id)
        {
            var tblcustomer = await _context.Tblcustomers.FindAsync(id);

            if (tblcustomer == null)
            {
                return NotFound();
            }

            return tblcustomer;
        }

        // PUT: api/Tblcustomers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblcustomer(int id, Tblcustomer tblcustomer)
        {
            if (id != tblcustomer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(tblcustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblcustomerExists(id))
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

        // POST: api/Tblcustomers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblcustomer>> PostTblcustomer(Tblcustomer tblcustomer)
        {
            _context.Tblcustomers.Add(tblcustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblcustomer", new { id = tblcustomer.CustomerId }, tblcustomer);
        }

        // DELETE: api/Tblcustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblcustomer(int id)
        {
            var tblcustomer = await _context.Tblcustomers.FindAsync(id);
            if (tblcustomer == null)
            {
                return NotFound();
            }

            _context.Tblcustomers.Remove(tblcustomer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblcustomerExists(int id)
        {
            return _context.Tblcustomers.Any(e => e.CustomerId == id);
        }
    }
}
