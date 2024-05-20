using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthsController : ControllerBase
    {
        private readonly ContextArcadia _context;
        private readonly ILogger<HealthsController> _logger;

        public HealthsController(ContextArcadia context, ILogger<HealthsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Healths
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Health>>> GetHealths()
        {
            return await _context.Healths.ToListAsync();
        }

        // GET: api/Healths/5
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<Health>> GetHealth(int id)
        {
            var health = await _context.Healths.FindAsync(id);

            if (health == null)
            {
                return NotFound();
            }

            return health;
        }

        // PUT: api/Healths/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Policy = "UpdateHealth")]
        public async Task<IActionResult> PutHealth(int id, Health health)
        {
            if (id != health.Id)
            {
                return BadRequest();
            }

            _context.Entry(health).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthExists(id))
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

        // POST: api/Healths
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Policy = "CreateHealth")]
        public async Task<ActionResult<Health>> PostHealth(Health health)
        {
            _context.Healths.Add(health);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealth", new { id = health.Id }, health);
        }

        // DELETE: api/Healths/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = "DeleteHealth")]
        public async Task<IActionResult> DeleteHealth(int id)
        {
            var health = await _context.Healths.FindAsync(id);
            if (health == null)
            {
                return NotFound();
            }

            _context.Healths.Remove(health);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HealthExists(int id)
        {
            return _context.Healths.Any(e => e.Id == id);
        }
    }
}
