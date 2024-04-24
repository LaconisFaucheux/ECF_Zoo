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
                      where a != null
                      select a;

            List<Animal> l = new List<Animal>();
            l = await req.ToListAsync();

            return l;
            //inclure le SlugMini dans AnimalImage pour tous les animaux
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

            _context.Animals.Add(a);
            await _context.SaveChangesAsync();

            foreach (var image in animal.images)
            {
                if (image != null)
                {//TODO: ajOUter vérification de l'extension
                    var fileExtension = Path.GetExtension(image.FileName);
                    string fileName = $"{DateTime.Now.Ticks}_{animal.Name}{fileExtension}";
                    string fileNameMini = $"{DateTime.Now.Ticks}_{animal.Name}_mini{fileExtension}";
                    string storagePath = Path.Combine("Assets\\Images\\Animals", fileName);
                    string storagePathMini = Path.Combine("Assets\\Images\\Animals", fileNameMini);
                    using (var stream = new FileStream(storagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    Utils.ResizeImage(storagePath, storagePathMini, 50, 50);

                    var animalImage = new AnimalImage
                    {
                        Slug = storagePath,
                        MiniSlug = storagePathMini,
                        IdAnimal = animal.Id
                    };

                    a.Pics.Add(animalImage);
                }
            }

            return a;
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
                    File.Delete(image.Slug);
                    File.Delete(image.MiniSlug);
                }

                _context.Animals.Remove(animal);
            //}
            //else
            //{
            //    throw new DbUpdateConcurrencyException();
            //}
            
            return await _context.SaveChangesAsync();
        }
    }
}
