using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        // CRadke-2024_08
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode);
        Task SaveEmployeeAsync(EmployeeInfo employeeInfo);
        Task UpdateEmployeeAsync(string EmployeeCode, EmployeeInfo employeeInfo);
        Task DeleteEmployeeAsync(string employeeCode);
    }
}
