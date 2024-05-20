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
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalServ;
        private readonly ILogger<AnimalsController> _logger;

        public AnimalsController(IAnimalService animalServ, ILogger<AnimalsController> logger)
        {
            _animalServ = animalServ;
            _logger = logger;
        }

        // GET: api/Animals
        [HttpGet]
        //[AllowAnonymous]
		//[Authorize(Policy = "UpdateAnimal")]
		public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {        
            var animals = await _animalServ.GetAnimals();
            return Ok(animals);
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _animalServ.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Policy = "UpdateAnimal")]
        public async Task<IActionResult> PutAnimal(int id, [FromForm]AnimalDTO animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            try
            {
                var updateResult = await _animalServ.UpdateAnimal(id, animal);
                if (updateResult == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Animals'");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<Animal>(e, null, _logger);
            }

            return NoContent();
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Policy = "CreateAnimal")]
        public async Task<ActionResult<Animal>> PostAnimal([FromForm] AnimalDTO animal)
        {
            try
            {
                Animal a = await _animalServ.PostAnimal(animal);
                return CreatedAtAction(nameof(GetAnimal), new { id = a.Id }, a);
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse(e, animal, _logger);
            }
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        //[Authorize(Policy = "DeleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {

            try
            {
                int delCode = await _animalServ.DeleteAnimal(id);
                if (delCode == 0)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Animals'");
                }
                return NoContent();
            }
            catch (Exception e) 
            {
                return this.CustomErrorResponse<Animal>(e, null, _logger);
            }
        }
    }
}
