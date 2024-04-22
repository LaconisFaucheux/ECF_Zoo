﻿using API_Arcadia.Interfaces;
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
        public async Task<Animal> PostAnimal(AnimalDTO animal)
        {
            Animal a = new Animal
            {
                Name = animal.Name,
                IsMale = animal.IsMale,
                IdSpecies = animal.IdSpecies,
                IdHealth = animal.IdHealth
            };

            foreach (var image in animal.images)
            {
                if (image != null)
                {
                    var fileExtension = Path.GetExtension(image.FileName);
                    string fileName = $"{DateTime.Now.Ticks}_{animal.Name}{fileExtension}";
                    string storagePath = Path.Combine("Assets\\Images\\Animals", fileName);
                    using (var stream = new FileStream(storagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    var animalImage = new AnimalImage
                    {
                        Slug = storagePath,
                        IdAnimal = animal.Id
                    };

                    a.Pics.Add(animalImage);
                }
            }

            a.HealthData = null!;
            a.SpeciesData = null!;

            _context.Animals.Add(a);
            await _context.SaveChangesAsync();

            return a;
        }
    }
}
