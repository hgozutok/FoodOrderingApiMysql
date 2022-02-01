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
    public class TblsiteinfoesController : ControllerBase
    {
        private readonly FoodorderingContext _context;

        public TblsiteinfoesController(FoodorderingContext context)
        {
            _context = context;
        }

        // GET: api/Tblsiteinfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tblsiteinfo>>> GetTblsiteinfos()
        {
            return await _context.Tblsiteinfos.ToListAsync();
        }

        // GET: api/Tblsiteinfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tblsiteinfo>> GetTblsiteinfo(int id)
        {
            var tblsiteinfo = await _context.Tblsiteinfos.FindAsync(id);

            if (tblsiteinfo == null)
            {
                return NotFound();
            }

            return tblsiteinfo;
        }

        // PUT: api/Tblsiteinfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblsiteinfo(int id, Tblsiteinfo tblsiteinfo)
        {
            if (id != tblsiteinfo.SiteInfoId)
            {
                return BadRequest();
            }

            _context.Entry(tblsiteinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblsiteinfoExists(id))
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

        // POST: api/Tblsiteinfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tblsiteinfo>> PostTblsiteinfo(Tblsiteinfo tblsiteinfo)
        {
            _context.Tblsiteinfos.Add(tblsiteinfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblsiteinfo", new { id = tblsiteinfo.SiteInfoId }, tblsiteinfo);
        }

        // DELETE: api/Tblsiteinfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblsiteinfo(int id)
        {
            var tblsiteinfo = await _context.Tblsiteinfos.FindAsync(id);
            if (tblsiteinfo == null)
            {
                return NotFound();
            }

            _context.Tblsiteinfos.Remove(tblsiteinfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblsiteinfoExists(int id)
        {
            return _context.Tblsiteinfos.Any(e => e.SiteInfoId == id);
        }
    }
}
