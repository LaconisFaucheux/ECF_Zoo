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
    public class VetVisitsController : ControllerBase
    {
        private readonly IVetVisitService _ServiceVetV;

        public VetVisitsController(IVetVisitService ServiceVetV)
        {
            _ServiceVetV = ServiceVetV;
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

        //// PUT: api/VetVisits/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVetVisit(int id, VetVisit vetVisit)
        //{
        //    if (id != vetVisit.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _ServiceVetV.Entry(vetVisit).State = EntityState.Modified;

        //    try
        //    {
        //        await _ServiceVetV.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VetVisitExists(id))
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

        //// POST: api/VetVisits
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<VetVisit>> PostVetVisit(VetVisit vetVisit)
        //{
        //    _ServiceVetV.VetVisits.Add(vetVisit);
        //    await _ServiceVetV.SaveChangesAsync();

        //    return CreatedAtAction("GetVetVisit", new { id = vetVisit.Id }, vetVisit);
        //}

        //// DELETE: api/VetVisits/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVetVisit(int id)
        //{
        //    var vetVisit = await _ServiceVetV.VetVisits.FindAsync(id);
        //    if (vetVisit == null)
        //    {
        //        return NotFound();
        //    }

        //    _ServiceVetV.VetVisits.Remove(vetVisit);
        //    await _ServiceVetV.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool VetVisitExists(int id)
        //{
        //    return _ServiceVetV.VetVisits.Any(e => e.Id == id);
        //}
    }
}
