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
                      where a != null
                      select a;

            List<Animal> l = new List<Animal>();
            l = await req.ToListAsync();

            return l;
            // TODO inclure le SlugMini dans AnimalImage pour tous les animaux
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
            Animal? animal = await req.FirstOrDefaultAsync();

            //if (animal != null)
            //animal.Image = File.ReadAllBytes(animal.Pics[0].MiniSlug);
            //if (animal != null)
            //{
            //    foreach (var p in animal.Pics)
            //    {
            //        FileStream file = File.Open(p.MiniSlug, FileMode.Open);
            //        animal.Images.Add(file);
            //    }
            //}
            return animal;

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
                    //var fileExtension = Path.GetExtension(image.FileName);
                    //string fileName = $"{DateTime.Now.Ticks}_{animal.Name}{fileExtension}";
                    //string fileNameMini = $"{DateTime.Now.Ticks}_{animal.Name}_mini{fileExtension}";
                    //string storagePath = Path.Combine("Assets\\Images\\Animals", fileName);
                    //string storagePathMini = Path.Combine("Assets\\Images\\Animals", fileNameMini);
                    //using (var stream = new FileStream(storagePath, FileMode.Create))
                    //{
                    //    await image.CopyToAsync(stream);
                    //}

                    //Utils.ResizeImage(storagePath, storagePathMini, 50, 50);

                    //var animalImage = new AnimalImage
                    //{
                    //    Slug = storagePath,
                    //    MiniSlug = storagePathMini,
                    //    IdAnimal = animal.Id
                    //};

                    var ai = await Utils.UploadImage<AnimalImage>(image, "Animals", a.Name, a.Id);
                    if (ai != null)
                    {
                        a.Pics.Add(ai);
                    }
                    
                }

            }
            _context.Animals.Add(a);
            await _context.SaveChangesAsync();

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
                    if (!String.IsNullOrEmpty(image.Slug)) File.Delete(image.Slug);
                    if (!String.IsNullOrEmpty(image.MiniSlug)) File.Delete(image.MiniSlug);
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
                    File.Delete(pic.Slug);
                    File.Delete(pic.MiniSlug);
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
