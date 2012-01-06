using System.Collections.Generic;
using System.Reflection;
using AssemblyLocator.Configuration;
using AssemblyLocator.UsageRules;

namespace AssemblyLocator.Locators
{
    public interface ILocator
    {
        bool UseConfigFile { get; set; }
        string ConfigFile { get; set; }
        IList<UsageRule> UsageRules { get; set; }
        void ProcessLocatorData();
        IList<Assembly> Assemblies { get; set; }
        IUsageRuleFolderApplicator UsageRuleFolerApplicator { get; set; }
        IUsageRuleAssemblyApplicator UsageRuleAssemblyApplicator { get; set; }
        IConfigurationController ConfigurationController { get; set; }
        IList<string> AssemblyFolders { get; set; }
        /// <summary>
        /// Will add a folder path to the list (doesn't check if valid path), will only add unique and none null or empty strings.
        /// </summary>
        /// <param name="folder">folder path</param>
        void AddFolder(string folder);
    }
}