using API_Arcadia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface ISpeciesService
    {
        Task<List<Species>> GetSpeciess();

        Task<Species?> GetSpeciesById(int id);

        Task<Species> PostSpecies(Species species);
    }
}
