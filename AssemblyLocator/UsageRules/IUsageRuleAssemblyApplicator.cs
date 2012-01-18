using System.Collections.Generic;
using System.Reflection;
using AssemblyLocator.AssemblyFileLoaders;

namespace HoHUtilities.AssemblyLocator.UsageRules
{
    public interface IUsageRuleAssemblyApplicator
    {
//        IList<Assembly> ApplyUsageRulesToAssemblies(bool excludeAllByDefault, IList<Assembly> assemblies, IList<UsageRule> usageRules);
//        bool MatchAssemblyName(UsageRule usageRule, Assembly assembly);
//        bool MatchAssemblyByNameOnly(Assembly assembly, string valueToMatch);
//        bool MatchAssemblyByFullName(Assembly assembly, string valueToMatch);
//        bool MatchAssemblyByFullDirectory(Assembly assembly, string valueToMatch);
//        bool MatchAssemblyByDirectoryNameOnly(Assembly assembly, string valueToMatch);
//        IList<Assembly> ApplyUsageRulesToFolders(IList<UsageRule> usageRules, IList<string> assemblyFolders);

        IAssemblyLoader AssemblyLoader { get; set; }

        IList<Assembly> ApplyUsageRulesToFolders(IList<UsageRule> usageRules, IList<string> assemblyFolders);
   
        void FindAllAssembiesForGivenFolders(IList<string> assemblyFolders, IList<Assembly> assemblies);
        void FindAllAssembliesSpecifiedInUsageRules(IList<UsageRule> usageRules, IList<Assembly> assemblies);
        void RemoveAllExplicityExcludedAssemblies(IList<UsageRule> usageRules, IList<Assembly> assemblies);
    }
}