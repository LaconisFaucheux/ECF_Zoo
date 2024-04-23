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
using API_Arcadia.Migrations;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitatsController : ControllerBase
    {
        private readonly IHabitatService _ServiceHab;

        public HabitatsController(IHabitatService serviceHab)
        {
            _ServiceHab = serviceHab;
        }

        // GET: api/Habitats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitat>>> GetHabitats()
        {
            return await _ServiceHab.GetHabitats();
        }

        // GET: api/Habitats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitat>> GetHabitat(int id)
        {
            var habitat = await _ServiceHab.GetHabitat(id);

            if (habitat == null)
            {
                return NotFound();
            }

            return habitat;
        }

        //// PUT: api/Habitats/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHabitat(int id, Habitat habitat)
        //{
        //    if (id != habitat.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(habitat).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HabitatExists(id))
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

        // POST: api/Habitats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Habitat>> PostHabitat([FromForm] HabitatDTO habitat)
        {
            try
            {

                Habitat h = await _ServiceHab.PostHabitat(habitat);
                return CreatedAtAction(nameof(GetHabitat), new { id = h.Id }, h);
            }
            catch (DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }
        }

        // DELETE: api/Habitats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            try
            {
                await _ServiceHab.DeleteHabitat(id);
                return NoContent();
            }
            catch (DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }
        }

        //private bool HabitatExists(int id)
        //{
        //    return _context.Habitats.Any(e => e.Id == id);
        //}
    }
}
