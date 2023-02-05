using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectAssignment.Interfaces;
using ProjectAssignment.Models;
using ProjectAssignment.ViewModels;

namespace ProjectAssignment.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository repository)
        {
            this._departmentRepository = repository;
        }

        public async Task<ActionResult> Index()
        {
            List<DepartmentViewModel> departmentList = new List<DepartmentViewModel>();
            List<Department> departments = await _departmentRepository.GetAll();
            foreach (Department department in departments)
            {
                departmentList.Add(
                    new DepartmentViewModel
                    {
                        ID = department.ID,
                        DepartmentName = department.DepartmentName,
                        Description = department.Description
                    });
            }
            return View(departmentList);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(CreateDepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Department department = new Department
                    {
                        ID = Guid.NewGuid(),
                        DepartmentName = viewModel.DepartmentName,
                        Description = viewModel.Description
                    };

                    bool result = _departmentRepository.Create(department);
                    if (result)
                    {
                        string message = "Created the record successfully";
                        ViewBag.Message = message;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(viewModel);
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Fail to create new Department");
                    return View(viewModel);
                }

            }
            return View(viewModel);
        }

        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            Department department = await _departmentRepository.GetById(id);
            if(department != null)
            {
                departmentViewModel = new DepartmentViewModel
                {
                    ID = department.ID,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description
                };
            }
            return View(departmentViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            Department department = await _departmentRepository.GetById(id);
            if(department != null)
            {
                bool result = _departmentRepository.Delete(department);
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
            EditDepartmentViewModel departmentViewModel = new EditDepartmentViewModel();
            Department department = await _departmentRepository.GetById(id);
            if (department != null)
            {
                departmentViewModel = new EditDepartmentViewModel
                {
                    ID = department.ID,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description
                };
            }
            return View(departmentViewModel);
        }
        

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid id, EditDepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                Department department = await _departmentRepository.GetById(id);
                if (department != null)
                {
                    department = new Department
                    {
                        DepartmentName = departmentViewModel.DepartmentName,
                        Description = departmentViewModel.Description,
                    };
                    bool result = _departmentRepository.Update(id, department);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        [HttpGet, ActionName("Details")]
        public async Task<ActionResult> Details(Guid id)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            Department department = await _departmentRepository.GetById(id);
            if (department != null)
            {
                departmentViewModel = new DepartmentViewModel
                {
                    ID = department.ID,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description
                };
            }
            return View(departmentViewModel);
        }
    }
}
