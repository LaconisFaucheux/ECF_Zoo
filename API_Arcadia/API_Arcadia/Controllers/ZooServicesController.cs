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
using API_Arcadia.Migrations;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooServicesController : ControllerBase
    {
        private readonly ContextArcadia _context;
        private readonly ILogger<ZooServicesController> _logger;

        public ZooServicesController(ContextArcadia context, ILogger<ZooServicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ZooServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZooService>>> GetZooServices()
        {
            return await _context.ZooServices.ToListAsync();
        }

        // GET: api/ZooServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZooService>> GetZooService(int id)
        {
            var zooService = await _context.ZooServices.FindAsync(id);

            if (zooService == null)
            {
                return NotFound();
            }

            return zooService;
        }

        // PUT: api/ZooServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZooService(int id, ZooService zooService)
        {
            if (id != zooService.Id)
            {
                return BadRequest();
            }

            _context.Entry(zooService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZooServiceExists(id))
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

        // POST: api/ZooServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ZooService>> PostZooService(ZooService zooService)
        {
            try
            {
                _context.ZooServices.Add(zooService);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetZooService", new { id = zooService.Id }, zooService);
            }
            catch(DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }

        }

        // DELETE: api/ZooServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZooService(int id)
        {
            var zooService = await _context.ZooServices.FindAsync(id);
            if (zooService == null)
            {
                return NotFound();
            }

            _context.ZooServices.Remove(zooService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZooServiceExists(int id)
        {
            return _context.ZooServices.Any(e => e.Id == id);
        }
    }
}
