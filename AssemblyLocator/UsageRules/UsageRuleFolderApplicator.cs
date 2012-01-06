using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;

namespace AssemblyLocator.UsageRules
{
    public class UsageRuleFolderApplicator : IUsageRuleFolderApplicator
    {


        /// <summary>
        /// Will apply all usageRules given and return the final set of directories
        /// </summary>
        /// <param name="usageRules"><see cref="IList{UsageRule}"/> The rules to be used.</param>
        /// <returns><see cref="IList{String}"/> The returned list of valid directories found.</returns>
        public IList<string> ApplyAllUsageRules(IList<UsageRule> usageRules)
        {
            IList<string> assemblyFolders = new List<string>();

            //then search for all INCLUDE usageRules
            AddAllRecursiveIncludeUsagesToFolder(usageRules, assemblyFolders);

            //then search for all EXCLUDE usageRules (remove any that have already been included if in excludes)
            RemoveAllRecursiveExcludeUsagesFromFolder(usageRules, assemblyFolders);
            
            return assemblyFolders;
        }

        public bool MatchAssemblyByFullDirectory(string folderName, string valueToMatch)
        {
            return new FileInfo(folderName).Directory.FullName.Equals(valueToMatch);
        }

        public bool MatchAssemblyByDirectoryNameOnly(string folderName, string valueToMatch)
        {
            return new FileInfo(folderName).Directory.Name.Equals(valueToMatch);
        }

        /// <summary>
        /// Iterates through all the include usageRules and finds any recursive folder statements and applies the logic.
        /// </summary>
        public void AddAllRecursiveIncludeUsagesToFolder(IList<UsageRule> usageRules, IList<string> assemblyFolders)
        {
            foreach (UsageRule usageRule in usageRules.Where(x => x.UsageRuleFunction == UsageRuleFunction.Include))
            {
                if (Directory.Exists(usageRule.Name))
                {
                    assemblyFolders.Add(usageRule.Name, true);

                    if(usageRule.UsageRuleDepth == UsageRuleDepth.CurrentFolderOnly)
                        continue;

                    int depth = int.MinValue;
                    Int32.TryParse(usageRule.FunctionParameter, out depth);
                    RescurivelyAddChildDirectoriesToList(new DirectoryInfo(usageRule.Name), assemblyFolders, 0, 
                        (usageRule.UsageRuleDepth == UsageRuleDepth.RecursiveDepth && depth > 0) ? depth : int.MaxValue);
                }                
            }
        }

        /// <summary>
        /// Iterates through all the exclude usageRules and finds any recursive folder statements and applies the logic.  Will remove all instances found from the list given
        /// </summary>
        public void RemoveAllRecursiveExcludeUsagesFromFolder(IList<UsageRule> usageRules, IList<string> assemblyFolders)
        {
            IList<string> excludes = new List<string>();
            foreach (UsageRule usageRule in usageRules.Where(x => x.UsageRuleFunction == UsageRuleFunction.Exclude))
            {
                if (Directory.Exists(usageRule.Name))
                {
                    excludes.Add(usageRule.Name, true);

                    if (usageRule.UsageRuleDepth == UsageRuleDepth.CurrentFolderOnly)
                        continue;

                    int depth = int.MinValue;
                    Int32.TryParse(usageRule.FunctionParameter, out depth);
                    RescurivelyAddChildDirectoriesToList(new DirectoryInfo(usageRule.Name), excludes, 0,
                        (usageRule.UsageRuleDepth == UsageRuleDepth.RecursiveDepth && depth > 0) ? depth : int.MaxValue);
                }
            }

            assemblyFolders.RemoveAll(excludes);
        }


        public void RescurivelyAddChildDirectoriesToList(DirectoryInfo directory, IList<string> directories, int depthCount, int depthLimit)
        {
            if (directory != null && directory.Exists && directories != null && (depthCount < depthLimit))
            {                
                foreach (DirectoryInfo info in directory.GetDirectories())
                {
                        directories.Add(info.FullName, true);

                    RescurivelyAddChildDirectoriesToList(info, directories, depthCount + 1, depthLimit);
                }
            }
        }


        public IList<string> FindAllDirectoryOnlyExculsionFolderNames(IList<UsageRule> usageRules)
        {
            return usageRules.Where(x => x.MatchType == UsageRuleNameMatch.DirectoryNameOnly && x.UsageRuleFunction == UsageRuleFunction.Exclude).Select(usageRule => usageRule.Name).ToList();
        }

        public IList<string> FindAllFullNameOnlyExclusionFolderNames(IList<UsageRule> usageRules)
        {
            return usageRules.Where(x => x.MatchType == UsageRuleNameMatch.DirectoryFullName && x.UsageRuleFunction == UsageRuleFunction.Exclude).Select(usageRule => usageRule.Name).ToList();
        }

    }
}
