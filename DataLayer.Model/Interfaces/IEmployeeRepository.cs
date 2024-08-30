using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetByCode(string employeeCode);
        Task<bool> SaveEmployee(Employee employee);
        Task<bool> DeleteEmployee(string employeeCode);
    }
}
