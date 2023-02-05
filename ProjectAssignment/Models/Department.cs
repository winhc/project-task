using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAssignment.Models
{
    public class Department
    {
        [Key]
        public Guid ID { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}