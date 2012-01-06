using System.Collections.Generic;

namespace SimpleIoCContainer
{
    public interface IDependencyResolver
    {
        T Resolve<T>() where T : class;
        T Resolve<T>(IDictionary<string, object> extraParameters) where T : class;
        void Register<TContract, TImplementation>() where TContract : class;
    }
}
