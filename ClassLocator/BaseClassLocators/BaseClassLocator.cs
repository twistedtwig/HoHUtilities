using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HoHUtilities.Extensions;

namespace HoHUtilities.ClassLocator.BaseClassLocators
{
    public class BaseClassLocator : IBaseClassLocator
    {

        public IList<Type> FindAllBaseClassesInAssemblies(IEnumerable<Assembly> assemblies)
        {
            IList<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in FindAllBaseClassesInAssembly(assembly))
                {
                    if (!types.Contains(type))
                    {
                        types.Add(type);
                    }
                }
            }

            return types;
        }

        public IList<Type> FindAllBaseClassesInAssembly(Assembly assembly)
        {
            IList<Type> assemblies = FindAllInterfacesInAssembly(assembly);
            assemblies.AddRange(FindAllAbstractClassesInAssembly(assembly));
            return assemblies;
        }

        public IList<Type> FindAllInterfacesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsInterface).ToList();
        }

        public IList<Type> FindAllAbstractClassesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsAbstract).ToList();
        }


    }
}
