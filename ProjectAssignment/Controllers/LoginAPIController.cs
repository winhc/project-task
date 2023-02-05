using ProjectAssignment.Interfaces;
using ProjectAssignment.Models;
using ProjectAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectAssignment.Controllers
{
    public class LoginAPIController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public LoginAPIController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(String employeeID, String employeePassword)
        {
            string response = "fail";
            Employee employee = await _employeeRepository.GetByEmployeeID(employeeID);
            if (employee != null)
            {
                var keyCode = employee.KeyCode;
                var password = PasswordUtil.EncodePassword(employeePassword, keyCode);
                if (password.Equals(employee.Password))
                {
                    FormsAuthentication.SetAuthCookie(employee.Name, false);
                    response = "success";
                }
            }
            return Json(new { result = response });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Json(new { result = "success" });
        }
    }
}