using API_Arcadia.Models;
using API_Arcadia.Services;
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
        public async Task<List<AnimalStats>> GetAnimalsStats() =>
            await _statsService.GetAnimalStatsAsync();

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
            

        [HttpPut("animals/{id}")]
        public async Task UpdateAnimalStat(string id, AnimalStats animalStat) =>
            await _statsService.UpdateAnimalStats(id, animalStat);

        [HttpDelete("animals/{id}")]
        public async Task DeleteAnimalStat(string id) =>
            await _statsService.DeleteAnimalStats(id);


        //HABITATS
        [HttpGet("habitats")]
        public async Task<List<HabitatStats>> GetHabitatsStats() =>
            await _statsService.GetHabitatStatsAsync();

        [HttpPost("habitats")]
        public async Task<ActionResult> CreateHabitatStat(HabitatStats habitatStat)
        {
            var result = await _statsService.CreateHabitatStatsAsync(habitatStat);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Une entrée existe déjà avec ce nom");
            }
        }            

        [HttpPut("habitats/{id}")]
        public async Task UpdateHabitatStat(string id, HabitatStats habitatStat) =>
            await _statsService.UpdateHabitatStats(id, habitatStat);

        [HttpDelete("habitats/{id}")]
        public async Task DeleteHabitatStat(string id) =>
            await _statsService.DeleteHabitatStats(id);
    }
}
