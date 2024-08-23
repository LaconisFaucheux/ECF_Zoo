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
using Microsoft.AspNetCore.Authorization;
using API_Arcadia.Interfaces;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooServicesController : ControllerBase
    {
        private readonly IZooServiceService _zooService;
        private readonly ILogger<ZooServicesController> _logger;

        public ZooServicesController(IZooServiceService zss, ILogger<ZooServicesController> logger)
        {
            _zooService = zss;
            _logger = logger;
        }

        // GET: api/ZooServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZooService>>> GetZooServices()
        {
            var zooServices = await _zooService.GetServices();
            return Ok(zooServices);
        }

        // GET: api/ZooServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZooService>> GetZooService(int id)
        {
            var zooService = await _zooService.GetService(id);

            if (zooService == null)
            {
                return NotFound();
            }

            return Ok(zooService);
        }

        // PUT: api/ZooServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PutZooService(int id, [FromForm]ServiceDTO zooService)
        {
            if (id != zooService.Id) return BadRequest();

            try
            {
                var updateResult = await _zooService.UpdateService(id, zooService);
                if(updateResult == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Services'");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<ZooService>(e, null, _logger);
            }

            return NoContent();
        }

        // POST: api/ZooServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<ZooService>> PostZooService([FromForm]ServiceDTO zooService)
        {
            try
            {
                ZooService zs = await _zooService.PostService(zooService);
                return Ok(zs);
            }
            catch(Exception e)
            {
                return this.CustomErrorResponse(e, zooService, _logger);
            }

        }

        // DELETE: api/ZooServices/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteZooService(int id)
        {
            try
            {
                int delCode = await _zooService.DeleteService(id);
                if (delCode == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Services'");
                }
                return NoContent();
            }
            catch(Exception e)
            {
                return this.CustomErrorResponse<ZooService>(e, null, _logger);
            }
        }
    }
}

//TODO: Créer un service ZooService et adapter les POST et PUT pour la gestion des photos