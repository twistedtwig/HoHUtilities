using System;

namespace HoHUtilities.ClassLocator.UsageRules
{
    /// <summary>
    /// What is the restriction looking for, attribute, class or assembly. I.E. What should it be applied to?
    /// </summary>
    public enum UsageRuleType
    {
        Attribute,
        BaseClass,
        ImplementationClass,
        Assembly
    }
    
    /// <summary>
    /// How should the restriction be applied? What affect will this have?  Should it be included, excluded, 
    /// only classes from their own Assembly be matched together?
    /// </summary>
    public enum UsageRuleFunction
    {
        Include, 
        IncludeAll,
        Exclude,
        ExcludeAll,
        LimitImplementationToOwnAssembly,
        SingleReturnTypeOnly,
        InterfacesOnly,
        AbstractsOnly,
    }

    /// <summary>
    /// Where the Restriction has been applied to.  I.e. should this restriction affect
    /// everything within a Class, assembly or globally? How widely should it be applied?
    /// </summary>
    public enum UsageRuleScope
    {
        Method,
        BaseClass,
        ImplementationClass,
        Assembly,
        Global
    }

    /// <summary>
    /// How should the restriction be applied?  Should it be an exact match or a partial match?
    /// </summary>
    public enum UsageRuleNameMatch
    {
        Unspecified,
        FullName,
        NameOnly
    }

    public abstract class UsageRule
    {
        public UsageRuleFunction UsageRuleFunction { get; set; }
        public UsageRuleScope UsageRuleScope { get; set; }
        public UsageRuleType UsageRuleType { get; set; }
        public String Name { get; set; }
        public UsageRuleNameMatch MatchType { get; set; }
        public String FunctionParameter { get; set; }
    }

}
