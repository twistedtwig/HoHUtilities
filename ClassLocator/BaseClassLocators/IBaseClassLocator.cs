using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassLocator.BaseClassLocators
{
    public interface IBaseClassLocator
    {
        IList<Type> FindAllBaseClassesInAssemblies(IEnumerable<Assembly> assemblies);
        IList<Type> FindAllBaseClassesInAssembly(Assembly assembly);
        IList<Type> FindAllInterfacesInAssembly(Assembly assembly);
        IList<Type> FindAllAbstractClassesInAssembly(Assembly assembly);
    }
}