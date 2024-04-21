using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class AnimalService : IAnimalService
    {

        private readonly ContextArcadia _context;

        public AnimalService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal?> GetAnimal(int id)
        {
            //return await _context.Animals.FindAsync(id);
            var req = from a in _context.Animals
                        .Include(a => a.Pics)
                        .Include(a => a.HealthData)
                        .Include(a => a.SpeciesData)
                            .ThenInclude(sd => sd.sizeUnit)
                        .Include(a => a.SpeciesData)
                            .ThenInclude(sd => sd.weightUnit)
                        .Include(a => a.SpeciesData)
                            .ThenInclude(sd => sd.diet)
                        .Include(a => a.SpeciesData)
                            .ThenInclude(sd => sd.habitats)
                                .ThenInclude(h => h.Pics)
                      where a.Id == id
                      select a;
            return await req.FirstOrDefaultAsync();
        }

        // POST: api/Animals
        public async Task<Animal> PostAnimal(Animal animal)
        {
            animal.HealthData = null!;
            animal.SpeciesData = null!;
            animal.Pics = null!;

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return animal;
        }
    }
}
