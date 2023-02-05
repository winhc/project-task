using ProjectAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssignment.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();

        Task<Employee> GetById(Guid id);

        Task<Employee> GetByEmployeeID(string employeeID);

        bool Create(Employee employee);

        bool Update(Guid id, Employee employee);

        bool Delete(Employee employee);

        bool Save();
    }
}
