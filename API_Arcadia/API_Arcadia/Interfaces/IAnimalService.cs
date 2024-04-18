using API_Arcadia.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAnimals();

        Task<Animal?> GetAnimal(int id);
    }
}
