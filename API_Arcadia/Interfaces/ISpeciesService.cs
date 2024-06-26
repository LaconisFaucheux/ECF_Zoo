﻿using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Interfaces
{
    public interface ISpeciesService
    {
        Task<List<Species>> GetSpeciess();

        Task<Species?> GetSpeciesById(int id);

        Task<Species> PostSpecies(SpeciesDTO species);

        Task DeleteSpecies(int id);

        Task<int> UpdateSpecies(int id, SpeciesDTO species);
    }
}
