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
    public class DietsController : ControllerBase
    {
        private readonly ContextArcadia _context;

        public DietsController(ContextArcadia context)
        {
            _context = context;
        }

        // GET: api/Diets
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Diet>>> GetDiets()
        {
            return await _context.Diets.ToListAsync();
        }

        // GET: api/Diets/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Diet>> GetDiet(int id)
        {
            var diet = await _context.Diets.FindAsync(id);

            if (diet == null)
            {
                return NotFound();
            }

            return diet;
        }

        // PUT: api/Diets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateDiet")]
        public async Task<IActionResult> PutDiet(int id, Diet diet)
        {
            if (id != diet.Id)
            {
                return BadRequest();
            }

            _context.Entry(diet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietExists(id))
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

        // POST: api/Diets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "CreateDiet")]
        public async Task<ActionResult<Diet>> PostDiet(Diet diet)
        {
            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiet", new { id = diet.Id }, diet);
        }

        // DELETE: api/Diets/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteDiet")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            var diet = await _context.Diets.FindAsync(id);
            if (diet == null)
            {
                return NotFound();
            }

            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DietExists(int id)
        {
            return _context.Diets.Any(e => e.Id == id);
        }
    }
}
