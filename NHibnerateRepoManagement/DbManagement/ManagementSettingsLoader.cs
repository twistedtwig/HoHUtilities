using CustomConfigurations;

namespace DbManagement
{
    class ManagementSettingsLoader
    {
        public static ManagementSettings Load()
        {
            return new Config().GetSection("settings").Create<ManagementSettings>();
        }
    }
}
