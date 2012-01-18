using System.Collections.Generic;
using System.Reflection;
using HoHUtilities.ClassLocator.UsageRules;

namespace HoHUtilities.ClassLocator.Locators
{
    public interface ILocator
    {
        bool UseConfigFile { get; set; }
        string ConfigFile { get; set; }
        bool ReturnSingleImplementationForBaseClass { get; set; }
        bool ExcludeAllByDefault { get; set; }
        IList<UsageRule> Restrictions { get; set; }
        void ProcessClassLocatorData();
        ClassCollection<ClassSettings> ClassCollection { get; set; }
        IList<Assembly> AssembliesToBeProcessed { get; set; }
    }
}