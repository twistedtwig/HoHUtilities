using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileOperationHelpers
{
    public static class FileHelper
    {

        public static void CopyContents(string currentDirectory, string outputPath)
        {
            CopyContents(currentDirectory, outputPath, new List<string>(), new List<string>());
        }

        public static void CopyContents(string currentDirectory, string outputPath, IList<string> includes,
                                        IList<string> excludes)
        {
            Directory.CreateDirectory(outputPath);

            if (includes.Count > 0 && excludes.Count > 0)
            {
                foreach (string exclude in excludes)
                {
                    includes.Remove(exclude);
                }
            }

            CopyActualContents(currentDirectory, outputPath, includes, excludes);
        }

        private static void CopyActualContents(string currentDirectory, string outputPath, IList<string> includes,
                                               IList<string> excludes)
        {
            DirectoryInfo currentDir = new DirectoryInfo(currentDirectory);
            if (!currentDir.Exists) return;

            FileInfo[] fileInfos = currentDir.GetFiles();

            foreach (FileInfo file in fileInfos)
            {
                if (includes.Count > 0)
                {
                    if (includes.Any(include => file.FullName.Contains(include)))
                    {
                        file.CopyTo(Path.Combine(outputPath, file.Name), true);
                    }
                }
                else if (excludes.Count > 0)
                {
                    if (!excludes.Any(exclude => file.FullName.Contains(exclude)))
                    {
                        file.CopyTo(Path.Combine(outputPath, file.Name), true);
                    }
                }
                else
                {
                    file.CopyTo(Path.Combine(outputPath, file.Name), true);
                }
            }

            DirectoryInfo[] childDirs = currentDir.GetDirectories();
            foreach (DirectoryInfo directoryInfo in childDirs)
            {
                Directory.CreateDirectory(Path.Combine(outputPath, directoryInfo.Name));

                if (Directory.Exists(Path.Combine(outputPath, directoryInfo.Name)))
                {
                    CopyContents(directoryInfo.FullName, Path.Combine(outputPath, directoryInfo.Name), includes,
                                 excludes);
                }
            }
        }
    }
}
