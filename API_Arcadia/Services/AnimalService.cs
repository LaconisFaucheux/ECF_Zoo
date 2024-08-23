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
            var req = from a in _context.Animals
                      .Include(a => a.Pics)
                      .Include(a => a.SpeciesData)
                        .ThenInclude(sd => sd.habitats)
                      where a != null
                      select a;

            List<Animal> l = new List<Animal>();
            l = await req.ToListAsync();

            return l;
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
                                //.ThenInclude(h => h.Pics)
                      where a.Id == id
                      select a;
            Animal? animal = await req.FirstOrDefaultAsync();
            return animal;
        }

        public async Task<List<Animal>?> GetAnimalsByHabitat(int habitatId)
        {
            var req = from a in _context.Animals
                        .Include(a => a.Pics)
                        .Include(a => a.SpeciesData)
                            .ThenInclude (sd => sd.habitats)
                      where a.SpeciesData.habitats.Any(h => h.Id == habitatId)
                      select a;
            List<Animal>? animal = await req.ToListAsync();
            return animal;
        }

        public async Task<int> GetAnimalsListLength()
        {
            return await _context.Animals.CountAsync();
        }

        public async Task<Animal> PostAnimal(AnimalDTO animal)
        {
            Animal a = new Animal
            {
                Name = animal.Name,
                IsMale = animal.IsMale,
                IdSpecies = animal.IdSpecies,
                IdHealth = animal.IdHealth
            };

            a.HealthData = null!;
            a.SpeciesData = null!;

            foreach (var image in animal.images)
            {
                if (image != null)
                {
                    var ai = await Utils.UploadImage<AnimalImage>(image, "animals", a.Name, a.Id);
                    if (ai != null)
                    {
                        a.Pics.Add(ai);
                    }

                }

            }
            _context.Animals.Add(a);
            await _context.SaveChangesAsync();

            return a;

            //Animal a = new Animal
            //{
            //    Name = "Pouet",
            //    IsMale = true,
            //    IdSpecies = 1,
            //    IdHealth = 1
            //};

            //return a;
        }

        public async Task<int> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null) return 0;

            //if (animal != null)
            //{
            var req = from ai in _context.AnimalImages
                      where (ai.IdAnimal == animal.Id)
                      select ai;
            List<AnimalImage> images = await req.ToListAsync();
            foreach (var image in images)
            {
                if (!String.IsNullOrEmpty(image.Slug)) File.Delete(Path.Combine("wwwroot", image.Slug));
                if (!String.IsNullOrEmpty(image.MiniSlug)) File.Delete(Path.Combine("wwwroot", image.MiniSlug));
            }

            _context.Animals.Remove(animal);
            //}
            //else
            //{
            //    throw new DbUpdateConcurrencyException();
            //}

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAnimal(int id, AnimalDTO animal)
        {
            var req = from a in _context.Animals
                        .Include(a => a.Pics)
                      where a.Id == id
                      select a;
            Animal? dbAnimal = await req.FirstOrDefaultAsync();

            if (dbAnimal == null) return 0;

            dbAnimal.Name = animal.Name;
            dbAnimal.IsMale = animal.IsMale;
            dbAnimal.IdSpecies = animal.IdSpecies;
            dbAnimal.IdHealth = animal.IdHealth;

            foreach (var imageId in animal.deletedImages)
            {
                var req2 = from ai in _context.AnimalImages
                           where ai.Id == imageId && ai.IdAnimal == dbAnimal.Id
                           select ai;
                var pic = await req2.FirstOrDefaultAsync();

                if (pic != null)
                {
                    File.Delete(Path.Combine("wwwroot", pic.Slug));
                    File.Delete(Path.Combine("wwwroot", pic.MiniSlug));
                    dbAnimal.Pics.Remove(pic);
                }
            }

            foreach (var image in animal.images)
            {
                if (image != null)
                {
                    var ai = await Utils.UploadImage<AnimalImage>(image, "Animals", dbAnimal.Name, dbAnimal.Id);
                    if (ai != null)
                    {
                        dbAnimal.Pics.Add(ai);
                    }
                }
            }
            _context.Entry(dbAnimal).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
