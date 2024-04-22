using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface IHabitatService
    {
        Task<List<Habitat>> GetHabitats();

        Task<Habitat?> GetHabitat(int id);

        Task<Habitat> PostHabitat(HabitatDTO habitat);
    }
}
