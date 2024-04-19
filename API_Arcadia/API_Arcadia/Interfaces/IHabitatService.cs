using API_Arcadia.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface IHabitatService
    {
        Task<List<Habitat>> GetHabitats();

        Task<Habitat?> GetHabitat(int id);
    }
}
