using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ContextArcadia _context;

        public SpeciesService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<List<Species>> GetSpeciess()
        {
            return await _context.Speciess.ToListAsync();
        }

        public async Task<Species?> GetSpeciesById(int id)
        {
            var req = from s in _context.Speciess
                        .Include(s => s.diet)
                        .Include(s => s.weightUnit)
                        .Include(s => s.sizeUnit)
                        .Include(s => s.habitats)
                        .ThenInclude(h => h.Pics)
                      where s.Id == id
                      select s;
            return await req.FirstOrDefaultAsync();
        }
    }
}
