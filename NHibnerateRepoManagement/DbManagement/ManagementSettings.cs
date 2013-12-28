
namespace DbManagement
{
    public class ManagementSettings
    {

//        1) script folder root location
//        2) version format (regex) when should do a major upgrade
//        3) flag for force major upgrade
//        4) auto create major folder structure
//        5) folder structure 

        public string RootFolderLocation { get; set; }
        public string VersionFormatRegex { get; set; }
        public bool ForceMajorUpgrade { get; set; }
        public bool CreateMajorFolderStructure { get; set; }
    }
}
