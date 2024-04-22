using API_Arcadia.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Interfaces
{
    public interface IVetVisitService
    {

        Task<List<VetVisit>> GetVetVisits();

        Task<VetVisit?> GetVetVisit(int id);

        Task<VetVisit> PostVetVisit(VetVisit vetVisit);
    }
}
