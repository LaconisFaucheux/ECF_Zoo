using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

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

        public async Task<Species> PostSpecies(SpeciesDTO species)
        {
            Species s = new Species
            {
                Name = species.Name,
                ScientificName = species.ScientificName,
                Description = species.Description,
                MaleMaxSize = species.MaleMaxSize,
                FemaleMaxSize = species.FemaleMaxSize,
                MaleMaxWeight = species.MaleMaxWeight,
                FemaleMaxWeight = species.FemaleMaxWeight,
                IdSizeUnit = species.IdSizeUnit,
                IdWeightUnit = species.IdWeightUnit,
                Lifespan = species.Lifespan,
                IdDiet = species.IdDiet,
                diet = null!,
                weightUnit = null!,
                sizeUnit = null!
            };

            _context.Speciess.Add(s);
            await _context.SaveChangesAsync();


            foreach (int hid in species.habitats)
            {
                var h = await _context.Habitats.FindAsync(hid);

                if (h != null)
                {
                    s.habitats.Add(h);
                }                
            }


            await _context.SaveChangesAsync();

            return s;
        }

        public async Task DeleteSpecies(int id)
        {
            var species = await _context.Speciess.FindAsync(id);
            if (species != null)
            {
                _context.Speciess.Remove(species);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateSpecies(int id, SpeciesDTO species)
        {
            var req = from s in _context.Speciess
                      .Include(s => s.habitats)
                      where s.Id == id
                      select s;
            var dbSpecies = await req.FirstOrDefaultAsync();

            if (dbSpecies == null) return 0;

            dbSpecies.Name              = String.IsNullOrEmpty(species.Name) ? dbSpecies.Name : species.Name; //vraiment utile ?
            dbSpecies.ScientificName    = String.IsNullOrEmpty(species.ScientificName) ? dbSpecies.ScientificName : species.ScientificName;
            dbSpecies.Description       = String.IsNullOrEmpty(species.Description) ? dbSpecies.Description : species.Description;
            dbSpecies.MaleMaxSize       = species.MaleMaxSize ;
            dbSpecies.FemaleMaxSize     = species.FemaleMaxSize ;
            dbSpecies.MaleMaxWeight     = species.MaleMaxWeight ;
            dbSpecies.FemaleMaxWeight   = species.FemaleMaxWeight ;
            dbSpecies.IdSizeUnit        = species.IdSizeUnit ;
            dbSpecies.IdWeightUnit      = species.IdWeightUnit ;
            dbSpecies.Lifespan          = species.Lifespan ;
            dbSpecies.IdDiet            = species.IdDiet ;

            if(species.habitats.Count() > 0)
            {
                dbSpecies.habitats.Clear();
                foreach (var hid in species.habitats)
                {
                    var habitat = await _context.Habitats.FindAsync(hid);
                    if (habitat != null)
                    {
                        dbSpecies.habitats.Add(habitat);
                    }                    
                }
            }

            _context.Entry(dbSpecies).State = EntityState.Modified;
            return await _context.SaveChangesAsync();            
        }
    }
}
