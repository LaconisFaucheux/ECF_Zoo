using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAnimals();

        Task<Animal?> GetAnimal(int id);

        Task<Animal> PostAnimal(AnimalDTO animalDTO);

        Task<int> DeleteAnimal(int id);
    }
}
