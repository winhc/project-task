using ProjectAssignment.Interfaces;
using ProjectAssignment.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ProjectAssignment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}