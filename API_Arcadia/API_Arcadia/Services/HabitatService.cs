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

            foreach (var image in habitat.image)
            {
                if (image != null)
                {
                    var fileExtension = Path.GetExtension(image.FileName);
                    string fileName = $"{DateTime.Now.Ticks}_{habitat.Name}{fileExtension}";
                    string storagePath = Path.Combine("Assets\\Images\\Habitats", fileName);
                    using (var stream = new FileStream(storagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    var habitatImage = new HabitatImage
                    {
                        Slug = storagePath,
                        IdHabitat = habitat.Id
                    };

                    h.Pics.Add(habitatImage);
                }
            }

            _context.Habitats.Add(h);
            await _context.SaveChangesAsync();

            return h;
        }
    }
}
