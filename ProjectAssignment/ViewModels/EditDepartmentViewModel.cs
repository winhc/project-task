using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAssignment.ViewModels
{
    public class EditDepartmentViewModel
    {
        public Guid ID { get; set; }

        [Display(Name = "Department Name *")]
        [Required(ErrorMessage = "Department name is required!")]
        public string DepartmentName { get; set; }

        public string Description { get; set; }
    }
}