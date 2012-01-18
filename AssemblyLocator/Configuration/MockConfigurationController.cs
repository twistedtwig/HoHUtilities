using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyLocator.UsageRules;

namespace HoHUtilities.AssemblyLocator.Configuration
{
    public class MockConfigurationController : IConfigurationController
    {
        public MockConfigurationController()
        {
            CanUseCurrentAssemblyConfiguration = true;
            CanUseExecutingAssemblyConfiguration = false;
            CanUseCustomConfiguration = false;
//            ExcludeAllByDefault = true;
//            IncludeCurrentAssembly = true;
//            IncludeExecutingAssembly = false;
        }

        public string ConfigurationFile { get; set; }

        public bool CanUseCurrentAssemblyConfiguration { get; set; }
        public bool CanUseExecutingAssemblyConfiguration { get; set; }
        public bool CanUseCustomConfiguration { get; set; }


        private IList<UsageRule> MockRules = new List<UsageRule>()
                {
                    new UsageRule() { MatchType = UsageRuleNameMatch.FullName, UsageRuleFunction = UsageRuleFunction.Include, Name = Assembly.GetExecutingAssembly().Location}
//                    new UsageRule() {MatchType = UsageRuleNameMatch.FullName, Name = "testApp", UsageRuleFunction = UsageRuleFunction.Exclude },
//                    new UsageRule() { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = @"C:\hoh_code\WCFCoC\testApp\", UsageRuleFunction = UsageRuleFunction.FullyRecursive}
                };

        public IList<UsageRule> ProcessConfigurationData()
        {
            //try and use custom config file

            //try and use current Assembly Config file

            //then executing config file


            return MockRules;
        }
    }
}