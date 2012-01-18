using System.Collections.Generic;
using HoHUtilities.AssemblyLocator.UsageRules;

namespace HoHUtilities.AssemblyLocator.Configuration
{
    public interface IConfigurationController
    {
        string ConfigurationFile { get; set; }

        bool CanUseCurrentAssemblyConfiguration { get; set; }
        bool CanUseExecutingAssemblyConfiguration { get; set; }
        bool CanUseCustomConfiguration { get; set; }

        IList<UsageRule> ProcessConfigurationData();

    }
}
