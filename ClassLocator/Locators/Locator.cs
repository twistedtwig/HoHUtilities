using System;
using System.Collections.Generic;
using System.Reflection;
using ClassLocator.BaseClassLocators;
using ClassLocator.ImplementedClassLocators;
using ClassLocator.UsageRules;

namespace HoHUtilities.ClassLocator.Locators
{
    public class Locator : ILocator
    {
        public IBaseClassLocator BaseClassLocator { get; set; }
        public IImplementedClassLocator ImplementedClassLocator { get; set; }

        public IList<Assembly> AssembliesToBeProcessed { get; set; }

        public bool UseConfigFile { get; set; }
        public string ConfigFile { get; set; }
        public bool ReturnSingleImplementationForBaseClass { get; set; }
        public bool ExcludeAllByDefault { get; set; }
        public IList<UsageRule> Restrictions { get; set; }

        public Locator(){}

        public Locator(IList<Assembly> assemblies) : this(assemblies, null) { }

        public Locator(IList<Assembly> assemblies, IList<UsageRule> restrictions)
        {
            if (assemblies != null)
                AssembliesToBeProcessed = assemblies;
            
            if (restrictions != null)
                Restrictions = restrictions;

            //Setup defaults:
            UseConfigFile = true;
            ReturnSingleImplementationForBaseClass = false;
            ExcludeAllByDefault = false;



            //TODO: deal with the config file and load sections
        }


        public void ProcessClassLocatorData()
        {
            if (AssembliesToBeProcessed == null)
                throw new ArgumentException("Assemblies");

            //remove all invalid Assemblies as defined by the restrictions.
            //TODO apply the restrictions!!
            //            Assemblies = ApplyRestrictionsToAssemblies(Assemblies, Restrictions);

            IList<Type> interfacesInAssemblies = BaseClassLocator.FindAllBaseClassesInAssemblies(AssembliesToBeProcessed);


            IDictionary<Type, IList<Type>> implementations = ImplementedClassLocator.FindAllImplementations(AssembliesToBeProcessed, interfacesInAssemblies);

            //TODO: deal with the possiblity that they might limit the use of a baseclass to its own assembly
            //TODO: deal with class restictions
            //TODO: deal with attribute restictions


        }

        public ClassCollection<ClassSettings> ClassCollection
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        /// <summary>
        /// This method will find all classes that are in the given Assembly, it will then filter them for 
        /// any class that has the attributeType given.
        /// </summary>
        /// <param name="assembly"><c>Assembly</c> The assembly to search</param>
        /// <param name="attributesToSearchFor"><![CDATA[IList<Type>]]> A list of Typed attributes to search for</param>
        /// <returns><![CDATA[IList<string>]]> All the names of the found classes</returns>
        private IList<string> FindAllClassesInDll(Assembly assembly, IList<Type> attributesToSearchFor)
        {
            IList<string> classes = new List<string>();
            foreach (Type type in assembly.GetTypes())
            {
                MemberInfo info = type;
                foreach (object attribute in info.GetCustomAttributes(false))
                {
                    foreach (Type attrib in attributesToSearchFor)
                    {
                        if (attribute.GetType() == attrib)
                        {
                            classes.Add(type.FullName);
                        }
                    }

                }
            }

            return classes;
        }

        



       
    }
}
