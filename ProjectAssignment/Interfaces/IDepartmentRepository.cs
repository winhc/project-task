using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAssignment.Models;

namespace ProjectAssignment.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();

        Task<Department> GetById(Guid id);

        bool Create(Department department);

        bool Update(Guid id, Department department);

        bool Delete(Department department);

        bool Save();
    }
}
