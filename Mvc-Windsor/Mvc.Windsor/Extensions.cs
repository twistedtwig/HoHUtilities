using System;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace HoHUtilities.Mvc.Windsor
{
    public static class Extensions
    {
        public static IWindsorContainer RegisterController<T>(this IWindsorContainer container) where T : IController
        {
            container.RegisterControllers(typeof(T));

            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Type[] controllerTypes)
        {
            foreach (Type type in controllerTypes)
            {
                if (Helpers.Controllers.IsController(type))
                {
                    container.Register(Component.For(type).Named(type.FullName).LifeStyle.Is(LifestyleType.Transient));
                }
            }

            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                container.RegisterControllers(assembly.GetExportedTypes());
            }

            return container;
        }
    }
}
