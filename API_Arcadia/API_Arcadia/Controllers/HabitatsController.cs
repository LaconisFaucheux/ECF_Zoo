//testé OK 26/04/2024
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
using Microsoft.AspNetCore.Authorization;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitatsController : ControllerBase
    {
        private readonly IHabitatService _ServiceHab;
        private readonly ILogger<HabitatsController> _logger;

        public HabitatsController(IHabitatService serviceHab, ILogger<HabitatsController> logger)
        {
            _ServiceHab = serviceHab;
            _logger = logger;
        }

        // GET: api/Habitats
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Habitat>>> GetHabitats()
        {
            return await _ServiceHab.GetHabitats();
        }

        // GET: api/Habitats/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Habitat>> GetHabitat(int id)
        {
            var habitat = await _ServiceHab.GetHabitat(id);

            if (habitat == null)
            {
                return NotFound();
            }

            return habitat;
        }

        // PUT: api/Habitats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateHabitat")]
        public async Task<IActionResult> PutHabitat(int id, HabitatDTO habitat)
        {
            if (id != habitat.Id)
            {
                return BadRequest();
            }

            try
            {
                var updateResult = await _ServiceHab.UpdateHabitat(id, habitat);
                if (updateResult == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Habitats'");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Animal>(e, null, _logger);
            }

            return NoContent();
        }

        // POST: api/Habitats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "CreateHabitat")]
        public async Task<ActionResult<Habitat>> PostHabitat([FromForm] HabitatDTO habitat)
        {
            try
            {

                Habitat h = await _ServiceHab.PostHabitat(habitat);
                return CreatedAtAction(nameof(GetHabitat), new { id = h.Id }, h);
            }
            catch (DbUpdateException e)
            {
                return this.CustomErrorResponse(e, habitat, _logger);
            }
        }

        // DELETE: api/Habitats/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteHabitat")]
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            try
            {
                await _ServiceHab.DeleteHabitat(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Habitat>(e, null, _logger);
            }
        }

        //private bool HabitatExists(int id)
        //{
        //    return _context.Habitats.Any(e => e.Id == id);
        //}
    }
}
