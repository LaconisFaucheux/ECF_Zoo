using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
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
    }
}
