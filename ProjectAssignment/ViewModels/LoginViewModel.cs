using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAssignment.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Employee ID is required!")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}