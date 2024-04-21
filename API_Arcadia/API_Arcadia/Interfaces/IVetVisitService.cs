using API_Arcadia.Models;

namespace API_Arcadia.Interfaces
{
    public interface IVetVisitService
    {

        Task<List<VetVisit>> GetVetVisits();

        Task<VetVisit?> GetVetVisit(int id);
    }
}
