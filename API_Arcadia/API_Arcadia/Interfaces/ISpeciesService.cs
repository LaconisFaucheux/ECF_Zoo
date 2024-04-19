using API_Arcadia.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface ISpeciesService
    {
        Task<List<Species>> GetSpeciess();

        Task<Species?> GetSpeciesById(int id);
    }
}
