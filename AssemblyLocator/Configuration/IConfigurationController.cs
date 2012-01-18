using System.Collections.Generic;
using AssemblyLocator.UsageRules;

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
