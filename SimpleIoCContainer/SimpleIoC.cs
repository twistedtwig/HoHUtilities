using System.Collections.Generic;

namespace HoHUtilities.SimpleIoCContainer
{
    public static class SimpleIoC
    {
        private static IDependencyResolver InnerResolver;

        public static void Initialize(IDependencyResolver resolver)
        {
            InnerResolver = resolver;
        }

        public static void Register<TContract, TImplementation>() where TContract : class 
        {
            InnerResolver.Register<TContract, TImplementation>();
        }

        public static T Resolve<T>() where T : class 
        {
            return InnerResolver.Resolve<T>();
        }

        public static T Resolve<T>(IDictionary<string, object> extraParameters) where T : class 
        {
            return InnerResolver.Resolve<T>(extraParameters);
        }
    }
}
