using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class HabitatService : IHabitatService
    {
        private readonly ContextArcadia _context;

        public HabitatService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<List<Habitat>> GetHabitats()
        {
            return await _context.Habitats.ToListAsync();
        }

        public async Task<Habitat?> GetHabitat(int id)
        {
            var req = from h in _context.Habitats
                        .Include(h => h.Pics)
                      where h.Id == id
                      select h;
            return await req.FirstOrDefaultAsync();
        }

        public async Task<Habitat> PostHabitat(HabitatDTO habitat)
        {
            Habitat h = new Habitat
            {
                Name = habitat.Name,
                Description = habitat.Description
            };

            _context.Habitats.Add(h);
            await _context.SaveChangesAsync();

            foreach (var image in habitat.image)
            {
                if (image != null)
                {
                    var fileExtension = Path.GetExtension(image.FileName);
                    string fileName = $"{DateTime.Now.Ticks}_{habitat.Name}{fileExtension}";
                    string fileNameMini = $"{DateTime.Now.Ticks}_{habitat.Name}_mini{fileExtension}";
                    string storagePath = Path.Combine("Assets\\Images\\Habitats", fileName);
                    string storagePathMini = Path.Combine("Assets\\Images\\Habitats", fileNameMini);
                    using (var stream = new FileStream(storagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    Utils.ResizeImage(storagePath, storagePathMini, 50, 50);

                    //TODO voir pour refactoriser l'upload d'images. méthode avec <T>HabitatImage en retour + ajout de l'IdHabitat ici?

                    var habitatImage = new HabitatImage
                    {
                        Slug = storagePath,
                        MiniSlug = storagePathMini,
                        IdHabitat = habitat.Id
                    };

                    h.Pics.Add(habitatImage);
                }
            }

            return h;
        }

        public async Task DeleteHabitat(int id)
        {
            var habitat = await _context.Habitats.FindAsync(id); 

            if (habitat != null)
            {
                var req = from hi in _context.HabitatImages
                          where (hi.IdHabitat == habitat.Id)
                          select hi;
                List<HabitatImage> images = await req.ToListAsync();
                foreach (var image in images)
                {
                    File.Delete(image.Slug);
                }

                _context.Habitats.Remove(habitat);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }

            await _context.SaveChangesAsync();
        }
    }
}
