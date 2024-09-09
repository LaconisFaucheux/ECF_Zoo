using API_Arcadia.Models;
using API_Arcadia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly StatsService _statsService;

        public StatsController( StatsService statsService)
        {
            _statsService = statsService;
        }

        //ANIMALS
        [HttpGet("animals")]
        [Authorize(Roles = "Admin")]
        public async Task<List<AnimalStats>> GetAnimalsStats() =>
            await _statsService.GetAnimalStatsAsync();

        [HttpGet("animals/{id}")]
        public async Task<AnimalStats> GetAnimalStat(int id) =>
            await _statsService.GetAnimalStatAsync(id);

        [HttpPost("animals")]
        public async Task<ActionResult> CreateAnimalStat(AnimalStats animalStat)
        {
            var result = await _statsService.CreateAnimalStatsAsync(animalStat);

            if(result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Une entrée existe déjà avec ce nom");
            }
        }
            

        [HttpGet("animalsUpdate/{id}")]
        public async Task UpdateAnimalStat(int id) =>
            await _statsService.UpdateAnimalStats(id);

        [HttpDelete("animals/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAnimalStat(int id) =>
            await _statsService.DeleteAnimalStats(id);


        //HABITATS
        [HttpGet("habitats")]
        [Authorize(Roles = "Admin")]
        public async Task<List<HabitatStats>> GetHabitatsStats() =>
            await _statsService.GetHabitatStatsAsync();

        [HttpGet("habitats/{id}")]
        public async Task<HabitatStats> GetHabitatStat(int id) =>
            await _statsService.GetHabitatStatAsync(id);

        [HttpPost("habitats")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateHabitatStat(HabitatStats habitatStat)
        {
            var result = await _statsService.CreateHabitatStatsAsync(habitatStat);

            if (result == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Une entrée existe déjà avec ce nom");
            }
        }            

        [HttpGet("habitatsUpdate/{id}")]
        public async Task UpdateHabitatStat(int id) =>
            await _statsService.UpdateHabitatStats(id);

        [HttpDelete("habitats/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteHabitatStat(int id) =>
            await _statsService.DeleteHabitatStats(id);
    }
}
