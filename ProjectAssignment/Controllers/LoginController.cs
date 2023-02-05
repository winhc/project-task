using ProjectAssignment.Interfaces;
using ProjectAssignment.Models;
using ProjectAssignment.Utils;
using ProjectAssignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectAssignment.Controllers
{
    public class LoginController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        public LoginController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }


        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _employeeRepository.GetByEmployeeID(loginViewModel.EmployeeID);
                if(employee != null)
                {
                    var keyCode = employee.KeyCode;
                    var password = PasswordUtil.EncodePassword(loginViewModel.Password, keyCode);
                    if (password.Equals(employee.Password))
                    {
                        FormsAuthentication.SetAuthCookie(loginViewModel.EmployeeID, false);

                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect password!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Employee ID!");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }
    }
}
