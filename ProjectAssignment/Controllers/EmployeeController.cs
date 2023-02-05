using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectAssignment.Interfaces;
using ProjectAssignment.Models;
using ProjectAssignment.Utils;
using ProjectAssignment.ViewModels;

namespace ProjectAssignment.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            this._employeeRepository = employeeRepository;
            this._departmentRepository = departmentRepository;
        }

        public async Task<ActionResult> Index()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            List<Employee> employees = await _employeeRepository.GetAll();
            foreach (Employee employee in employees)
            {
                employeeList.Add(
                    new EmployeeViewModel
                    {
                        ID = employee.ID,
                        EmployeeID = employee.EmployeeID,
                        Name = employee.Name,
                        Email = employee.Email,
                        Phone = employee.Phone,
                        DepartmentName = employee.Department.DepartmentName,
                        JoinDate = employee.JoinDate,
                        Active = employee.Active,
                        CreatedDate = employee.CreatedDate,
                        UpdatedDate = employee.UpdatedDate
                    });
            }
            return View(employeeList);
        }

        public async Task<ActionResult> Create()
        {
            CreateEmployeeViewModel createEmployeeViewModel = new CreateEmployeeViewModel();
            List<Department> departments = await _departmentRepository.GetAll();
            createEmployeeViewModel.Departments = departments;
            createEmployeeViewModel.JoinDate = DateTime.Now;
            return View(createEmployeeViewModel);
        }

        // POST: Employee/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Department department = await _departmentRepository.GetById(viewModel.DepartmentID);
                var keyCode = PasswordUtil.GenerateSaltKey(10);
                var password = PasswordUtil.EncodePassword(viewModel.ConfirmPassword, keyCode);
                
               try
                {
                    Employee employee = new Employee
                    {
                        ID = Guid.NewGuid(),
                        EmployeeID = viewModel.EmployeeID,
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Phone = viewModel.Phone,
                        DepartmentID = department.ID,
                        JoinDate = viewModel.JoinDate,
                        Password = password,
                        KeyCode = keyCode,
                        Active = viewModel.Active,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    bool result = _employeeRepository.Create(employee);
                    if (result)
                    {
                        string message = "Created the record successfully";
                        ViewBag.Message = message;
                        return RedirectToAction("Index");
                    }
                }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Fail to create new Department => " + e);
                List<Department> departmentList = await _departmentRepository.GetAll();
                viewModel.Departments = departmentList;
                return View(viewModel);
            }

        }

            List<Department> departments = await _departmentRepository.GetAll();
            viewModel.Departments = departments;
            return View(viewModel);
        }

        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            Employee employee = await _employeeRepository.GetById(id);
            if (employee != null)
            {
                employeeViewModel = new EmployeeViewModel
                {
                    ID = employee.ID,
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    DepartmentName = employee.Department.DepartmentName,
                    JoinDate = employee.JoinDate,
                    Active = employee.Active,
                    CreatedDate = employee.CreatedDate,
                    UpdatedDate = employee.UpdatedDate
                };
            }
            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            Employee employee = await _employeeRepository.GetById(id);
            if (employee != null)
            {
                bool result = _employeeRepository.Delete(employee);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();

        }

        [HttpGet, ActionName("Edit")]
        public async Task<ActionResult> Edit(Guid id)
        {
            EditEmployeeViewModel employeeViewModel = new EditEmployeeViewModel();
            Employee employee = await _employeeRepository.GetById(id);
            List<Department> departments = await _departmentRepository.GetAll();
            if (employee != null)
            {
                employeeViewModel = new EditEmployeeViewModel
                {
                    ID = employee.ID,
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Departments = departments,
                    DepartmentID = employee.Department.ID,
                    JoinDate = employee.JoinDate,
                    Active = employee.Active,
                };
            }
            return View(employeeViewModel);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid id, EditEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _employeeRepository.GetById(id);
                var keyCode = PasswordUtil.GenerateSaltKey(10);
                var password = PasswordUtil.EncodePassword(viewModel.ConfirmPassword, keyCode);
                if (employee != null)
                {
                    employee = new Employee
                    {
                        EmployeeID = viewModel.EmployeeID,
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Phone = viewModel.Phone,
                        DepartmentID = viewModel.DepartmentID,
                        JoinDate = viewModel.JoinDate,
                        Password = password,
                        KeyCode = keyCode,
                        Active = viewModel.Active,
                        UpdatedDate = DateTime.Now
                    };
                    bool result = _employeeRepository.Update(id, employee);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            List<Department> departments = await _departmentRepository.GetAll();
            viewModel.Departments = departments;
            return View(viewModel);
        }

        [HttpGet, ActionName("Details")]
        public async Task<ActionResult> Details(Guid id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            Employee employee = await _employeeRepository.GetById(id);
            if (employee != null)
            {
                employeeViewModel = new EmployeeViewModel
                {
                    ID = employee.ID,
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    DepartmentName = employee.Department.DepartmentName,
                    JoinDate = employee.JoinDate,
                    Active = employee.Active,
                    CreatedDate = employee.CreatedDate,
                    UpdatedDate = employee.UpdatedDate
                };
            }
            return View(employeeViewModel);
        }
    }
}