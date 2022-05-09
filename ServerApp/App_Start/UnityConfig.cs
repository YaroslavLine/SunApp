using ServerApp.Controllers;
using ServerApp.DataModels;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace ServerApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ServerAppContext>(new TransientLifetimeManager());
            container.RegisterType<ICityRepository, CityRepository>(new TransientLifetimeManager());
            SeedData.Seed();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}