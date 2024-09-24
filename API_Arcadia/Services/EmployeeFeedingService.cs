using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Services
{
    public class EmployeeFeedingService : IEmployeeFeedingService
    {
        private readonly ContextArcadia _context;

        public EmployeeFeedingService(ContextArcadia context)
        {
            _context = context;
        }

        public async Task<EmployeeFeeding?> GetEmployeeFeeding(int id)
        {
            EmployeeFeeding? result = null;
            if (id != 0)
            {
                var req = from ef in _context.EmployeeFeedings
                            .Include(ef => ef.relatedAnimal)                                
                            .Include(ef => ef.weightUnit)
                          where ef.Id == id
                          select ef;

                result = await req.FirstOrDefaultAsync();
            }

            return result;
        }

        public async Task<List<EmployeeFeeding>> GetEmployeeFeedings()
        {
            var req = from ef in _context.EmployeeFeedings
                      .Include(ef => ef.relatedAnimal)
                        .ThenInclude(ra => ra.SpeciesData)
                      .Include(ef => ef.weightUnit)                      
                      where ef != null
                      select ef;
            List<EmployeeFeeding> result = new List<EmployeeFeeding>();
            result = await req.ToListAsync();

            return result;
        }

        public async Task<EmployeeFeeding> PostEmployeeFeeding(EmployeeFeeding ef)
        {
            EmployeeFeeding newEmployeeFeeding = new EmployeeFeeding
            {
                EmployeeEmail = ef.EmployeeEmail,
                IdAnimal = ef.IdAnimal,
                Food = ef.Food,
                Date = ef.Date,
                Weight = ef.Weight,
                IdWeightUnit = ef.IdWeightUnit,
                relatedAnimal = null!,
                weightUnit = null!
            };

            _context.EmployeeFeedings.Add(newEmployeeFeeding);
            await _context.SaveChangesAsync();

            return newEmployeeFeeding;
        }

        public async Task<int> DeleteEmployeeFeeding(int id)
        {
            var ef = await _context.EmployeeFeedings.FindAsync(id);
            if (ef == null) return 0;
            _context.Remove(ef);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
