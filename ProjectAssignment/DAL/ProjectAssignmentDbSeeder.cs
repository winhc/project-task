using ProjectAssignment.Models;
using ProjectAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectAssignment.DAL
{
    public static class ProjectAssignmentDbSeeder
    {
        public static void Seed()
        {
            using(ProjectAssignmentContext context = new ProjectAssignmentContext())
            {
                if (context.Database.Exists())
                {
                    if (!context.Department.Any())
                    {
                        Department department = new Department
                        {
                            ID = Guid.NewGuid(),
                            DepartmentName = "Information Technology",
                            Description = "This is IT department."
                        };
                        context.Department.Add(department);
                        context.SaveChanges();

                        if (!context.Employee.Any())
                        {
                            var keyCode = PasswordUtil.GenerateSaltKey(10);
                            var password = PasswordUtil.EncodePassword("123", keyCode);

                            Employee employee = new Employee
                            {
                                ID = Guid.NewGuid(),
                                EmployeeID = "EMP-001",
                                Name = "Mg Mg",
                                Email = "mgmg@gmail.com",
                                Phone = "09778633986",
                                DepartmentID = department.ID,
                                JoinDate = DateTime.Now,
                                Password = password,
                                KeyCode = keyCode,
                                Active = true,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now
                            };
                            context.Employee.Add(employee);
                            context.SaveChanges();
                        }
                    }
                }
            }
            
        }
    }
}