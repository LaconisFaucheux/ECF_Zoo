using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class VetVisitService : IVetVisitService
    {
        private readonly ContextArcadia _context;

        public VetVisitService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<VetVisit?> GetVetVisit(int id)
        {
            var req = from vv in _context.VetVisits                        
                        .Include(vv => vv.foodWeightUnit)
                        .Include(vv => vv.animal)
                            .ThenInclude(a => a.SpeciesData)
                      where vv.Id == id
                      select new
                      {
                          vv.Id,
                          vv.Food,
                          vv.FoodWeight,
                          vv.foodWeightUnit,
                          vv.VisitDate,
                          vv.Observations,
                          vv.animal,
                          SpeciesName = vv.animal.SpeciesData.Name
                      };
            var VetVisitData = await req.FirstOrDefaultAsync();

            if (VetVisitData == null)
            {
                return null;
            }

            var vetVisit = new VetVisit
            {
                Id = VetVisitData.Id,
                Food = VetVisitData.Food,
                FoodWeight = VetVisitData.FoodWeight,
                foodWeightUnit = VetVisitData.foodWeightUnit,
                VisitDate = VetVisitData.VisitDate,
                Observations = VetVisitData.Observations,
                animal = VetVisitData.animal
            };
            vetVisit.animal.SpeciesData = new Species
            {
                Name = VetVisitData.SpeciesName
            };

            return vetVisit;
        }

        public async Task<List<VetVisit>> GetVetVisits()
        {
            return await _context.VetVisits.ToListAsync();
        }
    }
}
