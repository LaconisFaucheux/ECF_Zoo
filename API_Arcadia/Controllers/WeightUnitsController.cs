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
    public class WeightUnitsController : ControllerBase
    {
        private readonly ContextArcadia _context;

        public WeightUnitsController(ContextArcadia context)
        {
            _context = context;
        }

        // GET: api/WeightUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeightUnit>>> GetWeightUnits()
        {
            return await _context.WeightUnits.ToListAsync();
        }

        // GET: api/WeightUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeightUnit>> GetWeightUnit(int id)
        {
            var weightUnit = await _context.WeightUnits.FindAsync(id);

            if (weightUnit == null)
            {
                return NotFound();
            }

            return weightUnit;
        }

        // PUT: api/WeightUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWeightUnit(int id, WeightUnit weightUnit)
        //{
        //    if (id != weightUnit.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(weightUnit).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WeightUnitExists(id))
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

        // POST: api/WeightUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/WeightUnits/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWeightUnit(int id)
        //{
        //    var weightUnit = await _context.WeightUnits.FindAsync(id);
        //    if (weightUnit == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WeightUnits.Remove(weightUnit);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool WeightUnitExists(int id)
        //{
        //    return _context.WeightUnits.Any(e => e.Id == id);
        //}
    }
}
