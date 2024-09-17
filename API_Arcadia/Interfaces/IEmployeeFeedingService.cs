using API_Arcadia.Models;

namespace API_Arcadia.Interfaces
{
    public interface IEmployeeFeedingService
    {
        Task<List<EmployeeFeeding>> GetEmployeeFeedings();
        Task<EmployeeFeeding?> GetEmployeeFeeding(int id);
        Task<EmployeeFeeding> PostEmployeeFeeding(EmployeeFeeding ef);
        Task<int> DeleteEmployeeFeeding(int id);

    }
}
