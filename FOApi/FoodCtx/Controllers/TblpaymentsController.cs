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
    public class TblpaymentsController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblpaymentsController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblpayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblpayment>>> GetTblpayments()
        {
            return await _context.Tblpayments.ToListAsync();
        }

        // GET: api/Tblpayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblpayment>> GetTblpayment(int id)
        {
            var tblpayment = await _context.Tblpayments.FindAsync(id);

            if (tblpayment == null)
            {
                return NotFound();
            }

            return tblpayment;
        }

        // PUT: api/Tblpayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblpayment(int id, Tblpayment tblpayment)
        {
            if (id != tblpayment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(tblpayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblpaymentExists(id))
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

        // POST: api/Tblpayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblpayment>> PostTblpayment(Tblpayment tblpayment)
        {
            _context.Tblpayments.Add(tblpayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblpayment", new { id = tblpayment.PaymentId }, tblpayment);
        }

        // DELETE: api/Tblpayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblpayment(int id)
        {
            var tblpayment = await _context.Tblpayments.FindAsync(id);
            if (tblpayment == null)
            {
                return NotFound();
            }

            _context.Tblpayments.Remove(tblpayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblpaymentExists(int id)
        {
            return _context.Tblpayments.Any(e => e.PaymentId == id);
        }
    }
}
