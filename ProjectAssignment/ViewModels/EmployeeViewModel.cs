using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectAssignment.ViewModels
{
    public class EmployeeViewModel
    {
        public Guid ID { get; set; }

        public string EmployeeID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        
        [DisplayName("Department Name")]
        public String DepartmentName { get; set; }

        public DateTime JoinDate { get; set; }

        public bool Active { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Updated Date")]
        public DateTime UpdatedDate { get; set; }
    }
}