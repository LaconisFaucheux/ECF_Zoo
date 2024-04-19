using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using API_Arcadia.Interfaces;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _ServiceSpec;

        public SpeciesController(ISpeciesService ServiceSpec)
        {
            _ServiceSpec = ServiceSpec;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpeciess()
        {
            return await _ServiceSpec.GetSpeciess();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Species>> GetSpecies(int id)
        {
            var species = await _ServiceSpec.GetSpeciesById(id);

            if (species == null)
            {
                return NotFound();
            }

            return species;
        }

        //// PUT: api/Species/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSpecies(int id, Species species)
        //{
        //    if (id != species.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(species).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SpeciesExists(id))
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

        //// POST: api/Species
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Species>> PostSpecies(Species species)
        //{
        //    _context.Speciess.Add(species);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSpecies", new { id = species.Id }, species);
        //}

        //// DELETE: api/Species/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSpecies(int id)
        //{
        //    var species = await _context.Speciess.FindAsync(id);
        //    if (species == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Speciess.Remove(species);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SpeciesExists(int id)
        //{
        //    return _context.Speciess.Any(e => e.Id == id);
        //}
    }
}
