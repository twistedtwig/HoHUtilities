using System;
using System.Reflection;

namespace HoHUtilities.AssemblyLocator.AssemblyFileLoaders
{
    public class BasicAssemblyLoader : IAssemblyLoader
    {
        public bool IsAnAssembly(string filePath)
        {
            return GetAssembly(filePath) != null;
        }

        public Assembly GetAssembly(string filePath)
        {
            try
            {
                return Assembly.LoadFrom(filePath);
            }
            catch (BadImageFormatException ex)
            {
                //could in theory log this, but normally happens when the assembly has dependencies.
            }

            return null;
        }
    }
}
