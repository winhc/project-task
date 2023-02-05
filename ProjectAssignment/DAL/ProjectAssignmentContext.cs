using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProjectAssignment.Models;
using ProjectAssignment.Utils;

namespace ProjectAssignment.DAL
{
    public class ProjectAssignmentContext : DbContext
    {
        public ProjectAssignmentContext() : base("ProjectAssignmentContext")
        {

        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        
    }
}