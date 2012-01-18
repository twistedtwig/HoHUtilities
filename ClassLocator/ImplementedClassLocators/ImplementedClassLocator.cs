using System;
using System.Collections.Generic;
using System.Reflection;
using Extensions;

namespace HoHUtilities.ClassLocator.ImplementedClassLocators
{
    public class ImplementedClassLocator : IImplementedClassLocator
    {


        public IDictionary<Type, IList<Type>> FindAllImplementations(IEnumerable<Assembly> assemblies, IList<Type> baseClasses)
        {
            IDictionary<Type, IList<Type>> dictionaryOfTypes = new Dictionary<Type, IList<Type>>();


            foreach (Type baseClass in baseClasses)
            {
                foreach (Assembly assembly in assemblies)
                {
                    IList<Type> allClassesThatInheritFrom = FindAllClassesThatInheritFrom(assembly, baseClass);
                    if (dictionaryOfTypes.ContainsKey(baseClass))
                    {
                        dictionaryOfTypes[baseClass].AddRange(allClassesThatInheritFrom);
                    }
                    else
                    {
                        dictionaryOfTypes.Add(baseClass, allClassesThatInheritFrom);
                    }
                }
            }

            return dictionaryOfTypes;
        }


        public IList<Type> FindAllClassesThatInheritFrom(Assembly assembly, Type baseClass)
        {
            IList<Type> classes = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetInterface(baseClass.FullName) != null)
                {
                    classes.Add(type);
                }
            }

            return classes;
        }

        public IList<Type> FindAllClassesInAssembly(IList<Assembly> assemblies)
        {
            IList<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                types.AddRange(FindAllClassesInAssembly(assembly));
            }

            return types;
        }

        public IList<Type> FindAllClassesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes();
        }

    }
}
