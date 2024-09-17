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
    public class EmployeeFeedingsController : ControllerBase
    {
        private readonly ContextArcadia _context;

        public EmployeeFeedingsController(ContextArcadia context)
        {
            _context = context;
        }

        // GET: api/EmployeeFeedings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeFeeding>>> GetEmployeeFeedings()
        {
            return await _context.EmployeeFeedings.ToListAsync();
        }

        // GET: api/EmployeeFeedings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeFeeding>> GetEmployeeFeeding(int id)
        {
            var employeeFeeding = await _context.EmployeeFeedings.FindAsync(id);

            if (employeeFeeding == null)
            {
                return NotFound();
            }

            return employeeFeeding;
        }

        // PUT: api/EmployeeFeedings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeFeeding(int id, EmployeeFeeding employeeFeeding)
        {
            if (id != employeeFeeding.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeFeeding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeFeedingExists(id))
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

        // POST: api/EmployeeFeedings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeFeeding>> PostEmployeeFeeding(EmployeeFeeding employeeFeeding)
        {
            _context.EmployeeFeedings.Add(employeeFeeding);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeFeeding", new { id = employeeFeeding.Id }, employeeFeeding);
        }

        // DELETE: api/EmployeeFeedings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeFeeding(int id)
        {
            var employeeFeeding = await _context.EmployeeFeedings.FindAsync(id);
            if (employeeFeeding == null)
            {
                return NotFound();
            }

            _context.EmployeeFeedings.Remove(employeeFeeding);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeFeedingExists(int id)
        {
            return _context.EmployeeFeedings.Any(e => e.Id == id);
        }
    }
}
