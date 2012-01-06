using System.Reflection;

namespace AssemblyLocator.AssemblyFileLoaders
{
    public interface IAssemblyLoader
    {
        /// <summary>
        /// Tests the given file path is a valid assembly and can be loaded.
        /// </summary>
        /// <param name="filePath">string the file path</param>
        /// <returns>bool</returns>
        bool IsAnAssembly(string filePath);

        /// <summary>
        /// Loads an assembly from a given file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Assembly GetAssembly(string filePath);

    }
}
