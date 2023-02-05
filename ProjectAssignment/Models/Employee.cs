using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAssignment.Models
{
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }

        public string EmployeeID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }

        public DateTime JoinDate { get; set; }

        public string Password { get; set; }

        public string KeyCode { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}