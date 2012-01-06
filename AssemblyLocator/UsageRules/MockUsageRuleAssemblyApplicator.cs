using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AssemblyLocator.AssemblyFileLoaders;

namespace AssemblyLocator.UsageRules
{
    public class MockUsageRuleAssemblyApplicator : IUsageRuleAssemblyApplicator
    {
        public IAssemblyLoader AssemblyLoader { get; set; }

        public IList<Assembly> ApplyUsageRulesToFolders(IList<UsageRule> usageRules, IList<string> assemblyFolders)
        {
            IList<Assembly> assemblies = new List<Assembly>();
            FindAllAssembiesForGivenFolders(assemblyFolders, assemblies);
            return assemblies;
        }

        public void FindAllAssembiesForGivenFolders(IList<string> assemblyFolders, IList<Assembly> assemblies)
        {
            foreach (string file in Directory.GetFiles(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName))
            {
                try
                {
                    Assembly assembly = AssemblyLoader.GetAssembly(file);
                    if(assembly != null && !assemblies.Contains(assembly))
                    {
                        assemblies.Add(assembly);
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        public void FindAllAssembliesSpecifiedInUsageRules(IList<UsageRule> usageRules, IList<Assembly> assemblies)
        {
            
        }

        public void RemoveAllExplicityExcludedAssemblies(IList<UsageRule> usageRules, IList<Assembly> assemblies)
        {
            
        }
    }
}