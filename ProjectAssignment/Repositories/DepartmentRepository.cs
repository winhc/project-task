using ProjectAssignment.DAL;
using ProjectAssignment.Interfaces;
using ProjectAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace ProjectAssignment.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ProjectAssignmentContext _context;
        
        public DepartmentRepository(ProjectAssignmentContext context)
        {
            _context = context;
        }

        public bool Create(Department department)
        {
            _context.Department.Add(department);
            return Save();
        }

        public bool Delete(Department department)
        {
            _context.Department.Remove(department);
            return Save();
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Department.ToListAsync();
        }

        public async Task<Department> GetById(Guid id)
        {
            return await _context.Department.FindAsync(id);
        }

        public bool Update(Guid id, Department department)
        {
            var data = _context.Department.FirstOrDefault(d => d.ID == id);
            if(data != null)
            {
                data.DepartmentName = department.DepartmentName;
                data.Description = department.Description;
            }
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}