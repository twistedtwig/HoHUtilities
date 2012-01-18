using System;
using System.Collections.Generic;
using System.Reflection;

namespace HoHUtilities.ClassLocator.ImplementedClassLocators
{
    public interface IImplementedClassLocator
    {
        IDictionary<Type, IList<Type>> FindAllImplementations(IEnumerable<Assembly> assemblies, IList<Type> baseClasses);
        IList<Type> FindAllClassesThatInheritFrom(Assembly assembly, Type baseClass);
        IList<Type> FindAllClassesInAssembly(IList<Assembly> assemblies);
        IList<Type> FindAllClassesInAssembly(Assembly assembly);
    }
}