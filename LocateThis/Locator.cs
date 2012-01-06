using System.Collections.Generic;
using Castle.Windsor;
using ClassLocator;
using System.Reflection;

namespace LocateThis
{
    /// <summary>
    /// Locator class that should be used as a singleton within IoC.
    /// The class is designed to manage the Location, creation and analysis of Assemblies and their Types, for the given set of UsageRules.
    /// </summary>
    public class Locator
    {
// ReSharper disable InconsistentNaming
        private static Locator _instance = new Locator();
// ReSharper restore InconsistentNaming
        /// <summary>
        /// Singleton access for Object if not used via IoC.
        /// </summary>
        public static Locator Instance
        {
            get
            {
                SetupInstance();
                return _instance;
            }
        }

        private static void SetupInstance()
        {
            if (_instance != null)
                return;

            //HACK do NOT like this but at this point can not think of a better approach.  
            //Need to ensure that how ever they access this object the same instance is returned, i.e. IoC or static Instance reference
            var container = new WindsorContainer();
            _instance = container.Resolve<Locator>();
        }

        public Locator()
        {
            _instance = this;
        }


        //PROP for AssemblyLocator
        /*
         * get; set; (hopefully allow IoC from this - or maybe hard code IoC if is null.. if that returns null manually choose.)
         * 
         * should be able to do LocateThis.Locator.AssemblyLocator.Assemblies;
         * 
         */

        /// <summary>
        /// Assembly Locator.
        /// </summary>
        public AssemblyLocator.Locators.ILocator AssemblyLocator { get; set; }


        /*
         * Is a shortcut to assembly locator to ensure it has run all its bits.
         */
        /// <summary>
        /// Assemblies Found / To be used for Class Analysis.
        /// </summary>
        public IList<Assembly> Assemblies
        {
            get
            {
                if (AssemblyLocator.Assemblies == null)
                {
                    AssemblyLocator.ProcessLocatorData();
                }

                return AssemblyLocator.Assemblies;
            }
            set { AssemblyLocator.Assemblies = value; }
        }




        //PROP for ClassLocator
        /*
         * get; set; (hopefully allow IoC from this - or maybe hard code IoC if is null.. if that returns null manually choose.)
         * 
         * should be able to do LocateThis.Locator.ClassLocator.ClassCollection;
         * 
         */
        /// <summary>
        /// Class Locator
        /// </summary>
        public ClassLocator.Locators.ILocator ClassLocator { get; set; }


        //PROP for ClassCollection
        /*
         * This is basically a shortcut for running the whole process.  would check if LocateThis.Locator.ClassLocator.ClassCollection is null, if so run back through assembly and class locators.
         */
        /// <summary>
        /// The collection of base classes and implementations found.
        /// </summary>
        public ClassCollection<ClassSettings> ClassCollection
        {
            get
            {
                if (ClassLocator.ClassCollection == null)
                {
                    ClassLocator.AssembliesToBeProcessed = Assemblies;
                    ClassLocator.ProcessClassLocatorData();
                }

                return ClassLocator.ClassCollection;
            }
            set { ClassLocator.ClassCollection = value; }
        }





        /*
         * Compile time runs::
         * 
         * Locator gets instantiated as singleton
         * 
         * For that particular assembly it is running in it will want to find
         * all the files it has in its restriction list.
         * 
         * need to have a way to be able to specify that one assembly.
         * 
         * Once the assembly has been found need to find all classes that
         * match the restrictions for the classes.
         * 
         * Return these classes in ClassCollection from LocateThis.Locator
         * 
         * 
         * 
         * RunTime::
         * 
         * User code can access singleton before other code calls it.
         * They can override any of the settings
         * 
         * Consumer Code calls the singleton-
         * 
         * finds all assemblies for restrictions given
         * finds all classes in assemblies for given restrictions
         * returns ClassCollection to consumer.
         * 
         * 
         * If WCF consumer::
         * 
         * Would reference "using WCF.consumer;"
         * would call extension method to return bindings and endpoints.
         * 
         * 
         */
    }
}
