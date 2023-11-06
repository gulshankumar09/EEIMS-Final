using EEIMS.Controllers;
using EEIMS.Functionalities;
using EEIMS.Models;
using EEIMS.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace EEIMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<AdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IEquipmentRepository, EquipmentRepository>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}