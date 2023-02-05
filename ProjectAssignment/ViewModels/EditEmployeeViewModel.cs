using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectAssignment.Models;

namespace ProjectAssignment.ViewModels
{
    public class EditEmployeeViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Employee ID is required!")]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Department")]
        public Guid DepartmentID { get; set; }

        public List<Department> Departments { get; set; }

        [Display(Name = "Join Date")]
        [DataType(DataType.DateTime)]
        public DateTime JoinDate { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passord do not match!")]
        public string ConfirmPassword { get; set; }
    }
}