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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ProjectAssignmentContext _context;

        public EmployeeRepository(ProjectAssignmentContext context)
        {
            _context = context;
        }

        public bool Create(Employee employee)
        {
            _context.Employee.Add(employee);
            return Save();
        }

        public bool Delete(Employee Employee)
        {
            _context.Employee.Remove(Employee);
            return Save();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employee.Include(employee => employee.Department).ToListAsync();
        }

        public async Task<Employee> GetById(Guid id)
        {
            return await _context.Employee.Include(employee => employee.Department).Where(employee => employee.ID.Equals(id)).FirstOrDefaultAsync();
        }

        public bool Update(Guid id, Employee Employee)
        {
            var data = _context.Employee.FirstOrDefault(d => d.ID == id);
            if (data != null)
            {
                data.Name = Employee.Name;
                data.Email = Employee.Email;
                data.Phone = Employee.Phone;
                data.DepartmentID = Employee.DepartmentID;
                data.JoinDate = Employee.JoinDate;
                data.Password = Employee.Password;
                data.KeyCode = Employee.KeyCode;
                data.Active = Employee.Active;
                data.UpdatedDate = Employee.UpdatedDate;
            }
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }

        public async Task<Employee> GetByEmployeeID(string employeeID)
        {
            return await _context.Employee.Where(employee => employee.EmployeeID.Equals(employeeID)).FirstOrDefaultAsync();
        }
    }
}