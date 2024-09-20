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
using Microsoft.AspNetCore.Authorization;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeFeedingsController : ControllerBase
    {
        private readonly IEmployeeFeedingService _employeeFeedingService;
        private readonly ILogger<EmployeeFeedingsController> _logger;

        public EmployeeFeedingsController(
            IEmployeeFeedingService employeeFeedingService,
            ILogger<EmployeeFeedingsController> logger)
        {
            _employeeFeedingService = employeeFeedingService;
            _logger = logger;
        }

        // GET: api/EmployeeFeedings
        [HttpGet]
        [Authorize(Roles = "Vet, Employee")]
        public async Task<ActionResult<IEnumerable<EmployeeFeeding>>> GetEmployeeFeedings()
        {
            var ef = await _employeeFeedingService.GetEmployeeFeedings();
            return Ok(ef);
        }

        // GET: api/EmployeeFeedings/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Vet, Employee")]
        public async Task<ActionResult<EmployeeFeeding>> GetEmployeeFeeding(int id)
        {
            var employeeFeeding = await _employeeFeedingService.GetEmployeeFeeding(id);

            if (employeeFeeding == null)
            {
                return NotFound();
            }

            return employeeFeeding;
        }

        //Rapports non modifiables pour une meilleure protection des animaux (pas de modif après coup pour camoufler des erreurs)
        //// PUT: api/EmployeeFeedings/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployeeFeeding(int id, EmployeeFeeding employeeFeeding)
        //{
        //    if (id != employeeFeeding.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employeeFeeding).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeFeedingExists(id))
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

        // POST: api/EmployeeFeedings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Vet, Employee")]
        public async Task<ActionResult<EmployeeFeeding>> PostEmployeeFeeding(EmployeeFeeding employeeFeeding)
        {
            try
            {
                EmployeeFeeding ef = await _employeeFeedingService.PostEmployeeFeeding(employeeFeeding);
                return CreatedAtAction(nameof(GetEmployeeFeeding), new { id = ef.Id }, ef);
            }
            catch (Exception ex)
            {
                return this.CustomErrorResponse(ex, employeeFeeding, _logger);
            }
        }

        // DELETE: api/EmployeeFeedings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployeeFeeding(int id)
        {
            try
            {
                int response = await _employeeFeedingService.DeleteEmployeeFeeding(id);
                if (response == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return this.CustomErrorResponse<EmployeeFeeding>(ex, null, _logger);
            }
        }
    }
}
