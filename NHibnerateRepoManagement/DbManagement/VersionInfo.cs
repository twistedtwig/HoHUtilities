using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DbManagement
{
    class VersionInfo
    {
        /// <summary>
        /// Get the full version number of the executing assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        public static bool DoesVersionMatchRegex(string pattern)
        {
            if(string.IsNullOrWhiteSpace(pattern)) { throw new NullReferenceException("pattern"); }

            return Regex.IsMatch(GetVersionNumber(), pattern);
        }
    }
}
