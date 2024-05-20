using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Interfaces
{
    public interface IVetVisitService
    {

        Task<List<VetVisit>> GetVetVisits();

        Task<VetVisit?> GetVetVisit(int id);

        Task<VetVisit> PostVetVisit(VetVisitDTO vetVisit);

        Task DeleteVetVisit(int id);

        Task<int> UpdateVetVisit(int id, VetVisitDTO vetVisit);
    }
}
