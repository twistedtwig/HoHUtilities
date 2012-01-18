using System;
using System.Collections.Generic;
using System.Reflection;
using HoHUtilities.AssemblyLocator.Configuration;
using HoHUtilities.AssemblyLocator.UsageRules;
using HoHUtilities.Extensions;

namespace HoHUtilities.AssemblyLocator.Locators
{
    public class Locator : ILocator
    {
        public IUsageRuleFolderApplicator UsageRuleFolerApplicator { get; set; }
        public IUsageRuleAssemblyApplicator UsageRuleAssemblyApplicator { get; set; }
        public IConfigurationController ConfigurationController { get; set; }

        private IList<string> assemblyFolders = new List<string>();
        public IList<string> AssemblyFolders
        {
            get { return assemblyFolders; }
            set { assemblyFolders = value; }
        }

        public IList<Assembly> Assemblies { get; set; }

        public bool UseConfigFile { get; set; }
        public string ConfigFile { get; set; }
        public IList<UsageRule> UsageRules { get; set; }

        public Locator()
        {
            //setup defaults
            UseConfigFile = true;
        }

        public Locator(string folder) : this(new List<string> { folder }) { }

        public Locator(IEnumerable<string> folders) : this()
        {
            if (folders != null)
            {
                foreach (string folder in folders)
                {
                    AddFolder(folder);
                }
            }
        }

        /// <summary>
        /// Will add a folder path to the list (doesn't check if valid path), will only add unique and none null or empty strings.
        /// </summary>
        /// <param name="folder">folder path</param>
        public void AddFolder(string folder)
        {
            if (!String.IsNullOrWhiteSpace(folder) && !AssemblyFolders.Contains(folder))
            {
                AssemblyFolders.Add(folder);
            }
        }

        /// <summary>
        /// Will process the usage rules to find all valid assemblies, will load values from configuration file is UseConfigFile is true.
        /// </summary>
        public void ProcessLocatorData()
        {
            if(UseConfigFile)
            {
                if(!string.IsNullOrWhiteSpace(ConfigFile))
                    ConfigurationController.ConfigurationFile = ConfigFile;

                UsageRules.AddRange(ConfigurationController.ProcessConfigurationData(), true);
            }
            //find all valid assembly folders then all valid assemblies in those folders.
            Assemblies = UsageRuleAssemblyApplicator.ApplyUsageRulesToFolders(UsageRules, UsageRuleFolerApplicator.ApplyAllUsageRules(UsageRules));
        }

        
    }
}
