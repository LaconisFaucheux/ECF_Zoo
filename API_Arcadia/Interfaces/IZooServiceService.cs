using API_Arcadia.Models.Data;
using API_Arcadia.Models;

namespace API_Arcadia.Interfaces
{
    public interface IZooServiceService
    {
        Task<List<ZooService>> GetServices();

        Task<ZooService?> GetService(int id);

        Task<ZooService> PostService(ServiceDTO service);

        Task<int> DeleteService(int id);

        Task<int> UpdateService(int id, ServiceDTO service);
    }
}
