using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectAssignment.ViewModels
{
    public class DepartmentViewModel
    {
        public Guid ID { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}