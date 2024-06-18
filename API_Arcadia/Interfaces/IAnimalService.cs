using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAnimals();

        Task<Animal?> GetAnimal(int id);

        Task<Animal> PostAnimal(AnimalDTO animalDTO);

        Task<int> DeleteAnimal(int id);

        Task<int> UpdateAnimal(int id, AnimalDTO animal);

        Task<List<Animal>?> GetAnimalsByHabitat(int habitatId);
    }
}
