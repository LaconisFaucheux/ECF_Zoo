//test OK 26/04/2024
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
    public class VetVisitsController : ControllerBase
    {
        private readonly IVetVisitService _ServiceVetV;
        private readonly ILogger<VetVisitsController> _logger;

        public VetVisitsController(IVetVisitService ServiceVetV, ILogger<VetVisitsController> logger)
        {
            _ServiceVetV = ServiceVetV;
            _logger = logger;
        }

        // GET: api/VetVisits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetVisit>>> GetVetVisits()
        {
            return await _ServiceVetV.GetVetVisits();
        }

        // GET: api/VetVisits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VetVisit>> GetVetVisit(int id)
        {
            var vetVisit = await _ServiceVetV.GetVetVisit(id);

            if (vetVisit == null)
            {
                return NotFound();
            }

            return vetVisit;
        }

        // PUT: api/VetVisits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVetVisit(int id, [FromForm]VetVisitDTO vetVisit)
        {
            if (id != vetVisit.Id)
            {
                return BadRequest();
            }

            try
            {
                var updateResult = await _ServiceVetV.UpdateVetVisit(id, vetVisit);
                if (updateResult == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'VetVisits'");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Animal>(e, null, _logger);
            }

            return NoContent();
        }

        // POST: api/VetVisits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VetVisit>> PostVetVisit([FromForm]VetVisitDTO vetVisit)
        {
            try
            {
                await _ServiceVetV.PostVetVisit(vetVisit);
                return CreatedAtAction("GetVetVisit", new { id = vetVisit.Id }, vetVisit);
            }
            catch (DbUpdateException e)
            {
                return this.CustomErrorResponse(e, vetVisit, _logger);
            }
        }

        // DELETE: api/VetVisits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVetVisit(int id)
        {
            try
            {
                await _ServiceVetV.DeleteVetVisit(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<VetVisit>(e, null, _logger);
            }
        }

        //private bool VetVisitExists(int id)
        //{
        //    return _ServiceVetV.VetVisits.Any(e => e.Id == id);
        //}
    }
}
