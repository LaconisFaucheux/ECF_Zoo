using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeUnitsController : ControllerBase
    {
        private readonly ContextArcadia _context;

        public SizeUnitsController(ContextArcadia context)
        {
            _context = context;
        }

        // GET: api/SizeUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeUnit>>> GetSizeUnits()
        {
            return await _context.SizeUnits.ToListAsync();
        }

        // GET: api/SizeUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeUnit>> GetSizeUnit(int id)
        {
            var sizeUnit = await _context.SizeUnits.FindAsync(id);

            if (sizeUnit == null)
            {
                return NotFound();
            }

            return sizeUnit;
        }

        // PUT: api/SizeUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSizeUnit(int id, SizeUnit sizeUnit)
        //{
        //    if (id != sizeUnit.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sizeUnit).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SizeUnitExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/SizeUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<SizeUnit>> PostSizeUnit(SizeUnit sizeUnit)
        //{
        //    _context.SizeUnits.Add(sizeUnit);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSizeUnit", new { id = sizeUnit.Id }, sizeUnit);
        //}

        // DELETE: api/SizeUnits/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSizeUnit(int id)
        //{
        //    var sizeUnit = await _context.SizeUnits.FindAsync(id);
        //    if (sizeUnit == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SizeUnits.Remove(sizeUnit);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SizeUnitExists(int id)
        //{
        //    return _context.SizeUnits.Any(e => e.Id == id);
        //}
    }
}
