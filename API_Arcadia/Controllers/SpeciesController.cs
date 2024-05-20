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
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _ServiceSpec;
        private readonly ILogger<SpeciesController> _logger;

        public SpeciesController(ISpeciesService ServiceSpec, ILogger<SpeciesController> logger)
        {
            _ServiceSpec = ServiceSpec;
            _logger = logger;
        }

        // GET: api/Species
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpeciess()
        {
            return await _ServiceSpec.GetSpeciess();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<Species>> GetSpecies(int id)
        {
            var species = await _ServiceSpec.GetSpeciesById(id);

            if (species == null)
            {
                return NotFound();
            }

            return species;
        }

        // PUT: api/Species/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Policy = "UpdateSpecies")]
        public async Task<IActionResult> PutSpecies(int id, [FromForm]SpeciesDTO species)
        {
            if (id != species.Id)
            {
                return BadRequest();
            }

            try
            {
                var updateResult = await _ServiceSpec.UpdateSpecies(id, species);
                if (updateResult == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Species'");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Animal>(e, null, _logger);
            }

            return NoContent();
        }

        // POST: api/Species
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Policy = "CreateSpecies")]
        public async Task<ActionResult<Species>> PostSpecies([FromForm] SpeciesDTO species)
        {
            try
            {
                Species s = await _ServiceSpec.PostSpecies(species);
                return CreatedAtAction(nameof(GetSpecies), new { id = s.Id }, s);
            }
            catch (DbUpdateException e)
            {
                return this.CustomErrorResponse(e, species, _logger);
            }
        }

        // DELETE: api/Species/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = "DeleteSpecies")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            try
            {
                await _ServiceSpec.DeleteSpecies(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Species>(e, null, _logger);
            }
        }

        //private bool SpeciesExists(int id)
        //{
        //    return _context.Speciess.Any(e => e.Id == id);
        //}
    }
}
