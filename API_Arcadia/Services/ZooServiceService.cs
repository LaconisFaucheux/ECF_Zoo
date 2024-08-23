using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class ZooServiceService : IZooServiceService
    {
        private readonly ContextArcadia _context;

        public ZooServiceService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<List<ZooService>> GetServices()
        {
            var req = from s in _context.ZooServices
                      select s;

            List<ZooService> l = new List<ZooService>();
            l = await req.ToListAsync();

            return l;
        }

        public async Task<ZooService?> GetService(int id)
        {
            var req = from s in _context.ZooServices
                      where s.Id == id
                      select s;

            ZooService? zooService = await req.FirstOrDefaultAsync();

            return zooService;
        }

        public async Task<ZooService> PostService(ServiceDTO service)
        {
            ZooService zs = new ZooService
            {
                Name = service.Name,
                Description = service.Description,
                FullPrice = service.FullPrice,
                ChildPrice = service.ChildPrice,
            };

            if (service.image != null)
            {
                var si = await Utils.UploadImage<string>(service.image, "zoo-services", zs.Name, zs.Id, false);
                if (si != null)
                {
                    zs.Pic = si;
                }
            }

            _context.ZooServices.Add(zs);
            await _context.SaveChangesAsync();

            return zs;
        }

        public async Task<int> DeleteService(int id)
        {
            var zooService = await _context.ZooServices.FindAsync(id);
            if (zooService == null) return 0;

            if (!String.IsNullOrEmpty(zooService.Pic))
            {
                File.Delete(Path.Combine("wwwroot", zooService.Pic));
            }

            _context.ZooServices.Remove(zooService);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateService(int id, ServiceDTO service)
        {
            var req = from zs in _context.ZooServices
                      where zs.Id == id
                      select zs;
            ZooService? dbzs = await req.FirstOrDefaultAsync();

            if (dbzs == null) return 0;

            dbzs.Name = service.Name;
            dbzs.Description = service.Description;
            dbzs.FullPrice = service.FullPrice;
            dbzs.ChildPrice = service.ChildPrice;

            if (service.deletedImage != null)
            {
                File.Delete(Path.Combine("wwwroot", service.deletedImage));
            }

            if (service.image != null)
            {
                File.Delete(Path.Combine("wwwroot", dbzs.Pic));
                var si = await Utils.UploadImage<string>(service.image, "zoo-services", dbzs.Name, dbzs.Id, false);
                if (si != null)
                {
                    dbzs.Pic = si;
                }                
            }

            _context.Entry(dbzs).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
