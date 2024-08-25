using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Mvc;
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

        public async Task DeleteVetVisit(int id)
        {
            var vv = await _context.VetVisits.FindAsync(id);

            if (vv != null)
            {
                _context.Remove(vv);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
        }

        public async Task<VetVisit?> GetVetVisit(int id)
        {
            var req = from vv in _context.VetVisits
                        .Include(vv => vv.foodWeightUnit)
                        .Include(vv => vv.animal)
                            .ThenInclude(a => a.SpeciesData)
                                .ThenInclude(sd => sd.diet)
                        .Include(vv => vv.animal)
                            .ThenInclude(a => a.HealthData)
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
                          SpeciesName = vv.animal.SpeciesData.Name,
                          SpeciesDiet = vv.animal.SpeciesData.diet,
                          HealthData = vv.animal.HealthData
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
            vetVisit.animal.HealthData = VetVisitData.HealthData;
            vetVisit.animal.SpeciesData = new Species
            {
                Name = VetVisitData.SpeciesName,
                diet = VetVisitData.SpeciesDiet
            };


            return vetVisit;
        }

        public async Task<List<VetVisit>> GetVetVisits()
        {
            var req = from vv in _context.VetVisits
            .Include(vv => vv.animal)
            .Include(vv => vv.animal)
                .ThenInclude(a => a.HealthData)
                      where vv != null
                      select vv;

            return await req.ToListAsync();
        }

        public async Task<VetVisit> PostVetVisit(VetVisitDTO vetVisit)
        {
            VetVisit vv = new()
            {
                Food = vetVisit.Food,
                FoodWeight = vetVisit.FoodWeight,
                IdWeightUnit = vetVisit.IdWeightUnit,
                VisitDate = DateTime.Now,
                Observations = vetVisit.Observations,
                IdAnimal = vetVisit.IdAnimal
            };
            vv.animal = null!;
            vv.foodWeightUnit = null!;

            var involvedAnimal = await _context.Animals.FindAsync(vv.IdAnimal);
            if (involvedAnimal != null)
            {
                involvedAnimal.IdHealth = vetVisit.healthId;
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }

            _context.VetVisits.Add(vv);
            await _context.SaveChangesAsync();

            return vv;
        }

        public async Task<int> UpdateVetVisit(int id, [FromForm]VetVisitDTO vetVisit)
        {
            var req = from vv in _context.VetVisits
                      .Include(vv => vv.animal)
                      where vv.Id == id
                      select vv;
            var currentVetVisit = await req.FirstOrDefaultAsync();

            if (currentVetVisit == null) return 0;

            currentVetVisit.Food = vetVisit.Food;
            currentVetVisit.FoodWeight = vetVisit.FoodWeight;
            currentVetVisit.IdWeightUnit = vetVisit.IdWeightUnit;
            currentVetVisit.VisitDate = DateTime.Now;
            currentVetVisit.Observations = vetVisit.Observations;
            currentVetVisit.IdAnimal = vetVisit.IdAnimal;

            currentVetVisit.animal.IdHealth = vetVisit.healthId;

            _context.Entry(currentVetVisit).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
