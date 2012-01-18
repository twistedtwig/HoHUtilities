using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HoHUtilities.AssemblyLocator.Configuration;
using HoHUtilities.AssemblyLocator.UsageRules;

namespace HoHUtilities.AssemblyLocator.Locators
{
    /// <summary>
    /// Mock Locator that will only get the executing assembly Folder and all the DLL's / assemblies in it.
    /// </summary>
    public class MockLocator : ILocator
    {
        public bool UseConfigFile { get; set; }
        public string ConfigFile { get; set; }

        public IList<UsageRule> UsageRules { get; set; }

        public void ProcessLocatorData()
        {
            Assemblies = new List<Assembly>();

            string location = Assembly.GetExecutingAssembly().Location;
            FileInfo file = new FileInfo(location);
            if(file.Exists)
            {
                string directory = file.Directory.FullName;
                DirectoryInfo dir = new DirectoryInfo(directory);

                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                    if(assembly != null)
                    {
                        Assemblies.Add(assembly);
                    }
                }
            }            
        }

        public IList<Assembly> Assemblies { get; set; }
        public IUsageRuleFolderApplicator UsageRuleFolerApplicator { get; set; }
        public IUsageRuleAssemblyApplicator UsageRuleAssemblyApplicator { get; set; }
        public IConfigurationController ConfigurationController { get; set; }
        public IList<string> AssemblyFolders { get; set; }
        public void AddFolder(string folder)
        {
            AssemblyFolders.Add(folder);
        }
    }
}