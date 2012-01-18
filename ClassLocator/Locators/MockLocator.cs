using System.Collections.Generic;
using System.Reflection;
using HoHUtilities.ClassLocator.UsageRules;

namespace HoHUtilities.ClassLocator.Locators
{
    public class MockLocator : ILocator
    {
        public bool UseConfigFile { get; set; }
        public string ConfigFile { get; set; }
        public bool ReturnSingleImplementationForBaseClass { get; set; }
        public bool ExcludeAllByDefault { get; set; }
        public IList<UsageRule> Restrictions { get; set; }

        public void ProcessClassLocatorData()
        {
            //TODO requires #515 task to be completed to be able to get that Assembly and return the correct stuff.

            //HACK for demo.



        }

        public ClassCollection<ClassSettings> ClassCollection { get; set; }
        public IList<Assembly> AssembliesToBeProcessed { get; set; }
    }
}
