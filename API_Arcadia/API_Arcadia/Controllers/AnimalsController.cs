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
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalServ;

        public AnimalsController(IAnimalService animalServ)
        {
            _animalServ = animalServ;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {        
            var animals = await _animalServ.GetAnimals();
            return Ok(animals);
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _animalServ.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        //// PUT: api/Animals/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAnimal(int id, Animal animal)
        //{
        //    if (id != animal.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(animal).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal([FromForm] AnimalDTO animal)
        {
            try
            {
                Animal a = await _animalServ.PostAnimal(animal);
                return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, a);
            }
            catch (DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {

            try
            {
                await _animalServ.DeleteAnimal(id);
                return NoContent();
            }
            catch (DbUpdateException e) 
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }
        }

        //private bool AnimalExists(int id)
        //{
        //    return _context.Animals.Any(e => e.Id == id);
        //}
    }
}
