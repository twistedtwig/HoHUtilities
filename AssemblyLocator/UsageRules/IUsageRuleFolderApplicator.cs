using System.Collections.Generic;
using System.IO;

namespace AssemblyLocator.UsageRules
{
    public interface IUsageRuleFolderApplicator
    {
        /// <summary>
        /// Will apply all usageRules given and return the final set of directories
        /// </summary>
        /// <param name="usageRules">IList<UsageRule> rules to be used.</param>
        /// <returns>IList<string> returned list of valid directories found.</returns>
        IList<string> ApplyAllUsageRules(IList<UsageRule> usageRules);
        
        bool MatchAssemblyByFullDirectory(string folderName, string valueToMatch);
        bool MatchAssemblyByDirectoryNameOnly(string folderName, string valueToMatch);

        /// <summary>
        /// Iterates through all the include usageRules and finds any recursive folder statements and applies the logic.
        /// </summary>
        void AddAllRecursiveIncludeUsagesToFolder(IList<UsageRule> usageRules, IList<string> assemblyFolders);

        /// <summary>
        /// Iterates through all the exclude usageRules and finds any recursive folder statements and applies the logic.  Will remove all instances found from the list given
        /// </summary>
        void RemoveAllRecursiveExcludeUsagesFromFolder(IList<UsageRule> usageRules, IList<string> assemblyFolders);

        void RescurivelyAddChildDirectoriesToList(DirectoryInfo directory, IList<string> directories, int depthCount, int depthLimit);
        IList<string> FindAllDirectoryOnlyExculsionFolderNames(IList<UsageRule> usageRules);
        IList<string> FindAllFullNameOnlyExclusionFolderNames(IList<UsageRule> usageRules);
    }
}