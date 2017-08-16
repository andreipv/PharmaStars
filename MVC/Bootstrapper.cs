using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using MVC.Services;
using MVC.Controllers;
using MVC.Models;

namespace MVC
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IService<SimpleProductModel>, ProductsService>();
            container.RegisterType<IService<ManufacturerModel>, ManufacturerService>();
            container.RegisterType<IService<CategoryModel>, CategoriesService>();

            return container;
        }
    }
}