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
            var req = from h in _context.Habitats
                      .Include(a => a.Pics)
                      where h != null
                      select h;

            List<Habitat> l = new List<Habitat>();
            l = await req.ToListAsync();

            return l;
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

            foreach (var image in habitat.images)
            {
                if (image != null)
                {
                    //var fileExtension = Path.GetExtension(image.FileName);
                    //string fileName = $"{DateTime.Now.Ticks}_{habitat.Name}{fileExtension}";
                    //string fileNameMini = $"{DateTime.Now.Ticks}_{habitat.Name}_mini{fileExtension}";
                    //string storagePath = Path.Combine("Assets\\Images\\Habitats", fileName);
                    //string storagePathMini = Path.Combine("Assets\\Images\\Habitats", fileNameMini);
                    //using (var stream = new FileStream(storagePath, FileMode.Create))
                    //{
                    //    await image.CopyToAsync(stream);
                    //}

                    //Utils.ResizeImage(storagePath, storagePathMini, 50, 50);                    

                    //var habitatImage = new HabitatImage
                    //{
                    //    Slug = storagePath,
                    //    MiniSlug = storagePathMini,
                    //    IdHabitat = habitat.Id
                    //};

                    var hi = await Utils.UploadImage<HabitatImage>(image, "habitats", h.Name, h.Id);
                    if (hi != null)
                    {
                        h.Pics.Add(hi);
                    }
                    
                }
            }
            _context.Habitats.Add(h);
            await _context.SaveChangesAsync();
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
                    if (!String.IsNullOrWhiteSpace(image.Slug)) File.Delete(image.Slug);
                    if (!String.IsNullOrWhiteSpace(image.MiniSlug)) File.Delete(image.MiniSlug);
                }

                _context.Habitats.Remove(habitat);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateHabitat(int id, HabitatDTO habitat)
        {

            var req = from a in _context.Habitats
                        .Include(a => a.Pics)
                      where a.Id == id
                      select a;
            Habitat? dbHabitat = await req.FirstOrDefaultAsync();

            if (dbHabitat == null) return 0;

            dbHabitat.Name = habitat.Name;
            dbHabitat.Description = habitat.Description;

            foreach (var imageId in habitat.deletedImages)
            {
                var req2 = from ai in _context.HabitatImages
                           where ai.Id == imageId && ai.IdHabitat == dbHabitat.Id
                           select ai;
                var pic = await req2.FirstOrDefaultAsync();

                if (pic != null)
                {
                    if (!String.IsNullOrEmpty(pic.Slug)) File.Delete(Path.Combine("wwwroot", pic.Slug));
                    if (!String.IsNullOrEmpty(pic.MiniSlug)) File.Delete(Path.Combine("wwwroot", pic.MiniSlug));
                    dbHabitat.Pics.Remove(pic);
                }
            }

            foreach (var image in habitat.images)
            {
                if (image != null)
                {
                    var hi = await Utils.UploadImage<HabitatImage>(image, "Habitats", dbHabitat.Name, dbHabitat.Id);
                    if (hi != null)
                    {
                        dbHabitat.Pics.Add(hi);
                    }
                }
            }
            _context.Entry(dbHabitat).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
