using System;

namespace HoHUtilities.AssemblyLocator.UsageRules
{
    
    /// <summary>
    /// How should the usageRule be applied? What affect will this have?  Should it be included, excluded, 
    /// </summary>
    public enum UsageRuleFunction
    {
        Include,
        Exclude,        
    }    

    /// <summary>
    /// How far should the Function rule be applied to that folder?
    /// </summary>
    public enum UsageRuleDepth
    {
        CurrentFolderOnly,
        FullyRecursive,
        RecursiveDepth,
    }

    /// <summary>
    /// When should this usageRule be applied, i.e. only at Compile time, Runtime or both?
    /// </summary>
    public enum UsageRulePoint
    {
        Compile,
        Runtime,
        Both
    }

    /// <summary>
    /// How should the usageRule be applied?  Should it be an exact match or a partial match?
    /// </summary>
    public enum UsageRuleNameMatch
    {
        Unspecified,
        FullName,
        NameOnly,
        DirectoryFullName,
        DirectoryNameOnly
    }

    public class UsageRule
    {
        public UsageRuleFunction UsageRuleFunction { get; set; }
        public UsageRuleDepth UsageRuleDepth { get; set; }
        public String Name { get; set; }
        public UsageRuleNameMatch MatchType { get; set; }
        public String FunctionParameter { get; set; }
    }

}
