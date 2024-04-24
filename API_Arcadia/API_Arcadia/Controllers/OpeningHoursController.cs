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
    public class OpeningHoursController : ControllerBase
    {
        private readonly ContextArcadia _context;
        private readonly ILogger<OpeningHoursController> _logger;

        public OpeningHoursController(ContextArcadia context, ILogger<OpeningHoursController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/OpeningHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpeningHour>>> GetOpeningHours()
        {
            return await _context.OpeningHours.ToListAsync();
        }

        // GET: api/OpeningHours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpeningHour>> GetOpeningHour(int id)
        {
            var openingHour = await _context.OpeningHours.FindAsync(id);

            if (openingHour == null)
            {
                return NotFound();
            }

            return openingHour;
        }

        // PUT: api/OpeningHours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpeningHour(int id, OpeningHour openingHour)
        {
            if (id != openingHour.Id)
            {
                return BadRequest();
            }

            _context.Entry(openingHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpeningHourExists(id))
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

        //No need for post or delete request: Anyway admin will only have to update already existing opening hours for each day

        // POST: api/OpeningHours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<OpeningHour>> PostOpeningHour(OpeningHour openingHour)
        //{
        //    _context.OpeningHours.Add(openingHour);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOpeningHour", new { id = openingHour.Id }, openingHour);
        //}

        // DELETE: api/OpeningHours/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOpeningHour(int id)
        //{
        //    var openingHour = await _context.OpeningHours.FindAsync(id);
        //    if (openingHour == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.OpeningHours.Remove(openingHour);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool OpeningHourExists(int id)
        {
            return _context.OpeningHours.Any(e => e.Id == id);
        }
    }
}
