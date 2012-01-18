using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssemblyLocator.AssemblyFileLoaders;


namespace HoHUtilities.AssemblyLocator.UsageRules
{
    public class UsageRuleAssemblyApplicator : IUsageRuleAssemblyApplicator
    {
        public IAssemblyLoader AssemblyLoader { get; set; }

        public IList<Assembly> ApplyUsageRulesToFolders(IList<UsageRule> usageRules, IList<string> assemblyFolders)
        {
            IList<Assembly> assemblies = new List<Assembly>();

            //find all assemblies in given folder and add to list
            FindAllAssembiesForGivenFolders(assemblyFolders, assemblies);

            //search all usages for all includes that are valid files
            FindAllAssembliesSpecifiedInUsageRules(usageRules, assemblies);
            
            //search all usages for all excludes that are valid files and remove assembly from list
            RemoveAllExplicityExcludedAssemblies(usageRules, assemblies);

            return assemblies;
        }
        

        public void FindAllAssembiesForGivenFolders(IList<string> assemblyFolders, IList<Assembly> assemblies)
        {
//Possble MEF example.                      
//            var directoryCatalog = new DirectoryCatalog(@"./");
//            AssemblySource.Instance.AddRange(
//                directoryCatalog.Parts
//                    .Select(part => ReflectionModelServices.GetPartType(part).Value.Assembly)
//                    .Where(assembly => !AssemblySource.Instance.Contains(assembly)));


            //search all usages for all includes that are valid folders
            foreach (string folder in assemblyFolders)
            {
                foreach (string file in Directory.GetFiles(folder))
                {
                    Assembly assembly = AssemblyLoader.GetAssembly(file);
                    if(assembly != null && !assemblies.Contains(assembly))
                    {
                        assemblies.Add(assembly);
                    }
                }
            }

        }

        public void FindAllAssembliesSpecifiedInUsageRules(IList<UsageRule> usageRules, IList<Assembly> assemblies)
        {
            foreach (UsageRule rule in usageRules.Where(x => 
                (x.MatchType ==UsageRuleNameMatch.FullName || x.MatchType == UsageRuleNameMatch.NameOnly)
                && x.UsageRuleFunction == UsageRuleFunction.Include))
            {
                if(File.Exists(rule.Name))
                {
                    //TODO use MEF to load Assembly add to list of assemblies if it is not already in list.
                    //Possible issue of equality comparison.. may need to foreach check the full assembly name or something.
                }
            }
        }

        public void RemoveAllExplicityExcludedAssemblies(IList<UsageRule> usageRules, IList<Assembly> assemblies)
        {
            foreach (UsageRule rule in usageRules.Where(x => x.UsageRuleFunction == UsageRuleFunction.Exclude))
            {
                if(File.Exists(rule.Name))
                {
                    //TODO use MEF to load Assembly 
//                    Assembly assemblyToRemove;
//                      //Possible issue of equality comparison.. may need to foreach check the full assembly name or something.                    
//                    if(assemblies.Contains(assemblyToRemove))
//                    {
//                        assemblies.Remove(assemblyToRemove);
//                    }
                }
            }
        }

//
//
//        public IList<Assembly> ApplyUsageRulesToAssemblies(bool excludeAllByDefault, IList<Assembly> assemblies, IList<UsageRule> usageRules)
//        {
//            IList<Assembly> validAssemblies = excludeAllByDefault ? new List<Assembly>(assemblies.Count) : assemblies;
//            IList<Assembly> inValidAssemblies = excludeAllByDefault ? assemblies : new List<Assembly>(assemblies.Count);
//
//            foreach (UsageRule usageRule in usageRules)
//            {
//                foreach (Assembly assembly in assemblies)
//                {
//                    if (MatchAssemblyName(usageRule, assembly))
//                    {
//                        switch (usageRule.UsageRuleFunction)
//                        {
//                            case UsageRuleFunction.Include:
//                                validAssemblies.AddIfNotInSecondList(assembly, inValidAssemblies);
//                                break;
//                            case UsageRuleFunction.Exclude:
//                                inValidAssemblies.AddIfNotInSecondList(assembly, validAssemblies);
//                                break;
//                        }
//                    }
//                }
//            }
//
//            return validAssemblies;
//        }
//
//        public bool MatchAssemblyName(UsageRule usageRule, Assembly assembly)
//        {
//            bool matched = false;
            //match assembly name
//            switch (usageRule.MatchType)
//            {
//                case UsageRuleNameMatch.Unspecified:
//                    matched = (MatchAssemblyByNameOnly(assembly, usageRule.Name)
//                        || MatchAssemblyByFullName(assembly, usageRule.Name)
//                        || MatchAssemblyByFullDirectory(assembly, usageRule.Name)
//                        || MatchAssemblyByDirectoryNameOnly(assembly, usageRule.Name));
//                    break;
//                case UsageRuleNameMatch.NameOnly:
//                    matched = MatchAssemblyByNameOnly(assembly, usageRule.Name);
//                    break;
//                case UsageRuleNameMatch.FullName:
//                    matched = MatchAssemblyByFullName(assembly, usageRule.Name);
//                    break;
//                case UsageRuleNameMatch.DirectoryFullName:
//                    matched = MatchAssemblyByFullDirectory(assembly, usageRule.Name);
//                    break;
//                case UsageRuleNameMatch.DirectoryNameOnly:
//                    matched = MatchAssemblyByDirectoryNameOnly(assembly, usageRule.Name);
//                    break;
//
//            }
//            return matched;
//        }
//
//        public bool MatchAssemblyByNameOnly(Assembly assembly, string valueToMatch)
//        {
//            return assembly.GetName().Equals(valueToMatch);
//        }
//
//        public bool MatchAssemblyByFullName(Assembly assembly, string valueToMatch)
//        {
//            return assembly.FullName.Equals(valueToMatch);
//        }
//
//        public bool MatchAssemblyByFullDirectory(Assembly assembly, string valueToMatch)
//        {
//            return new FileInfo(assembly.Location).Directory.FullName.Equals(valueToMatch);
//        }
//        
//        public bool MatchAssemblyByDirectoryNameOnly(Assembly assembly, string valueToMatch)
//        {
//            return new FileInfo(assembly.Location).Directory.Name.Equals(valueToMatch);
//        }
    }
}
