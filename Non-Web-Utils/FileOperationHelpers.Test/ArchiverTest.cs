using System;
using System.IO;
using NUnit.Framework;

namespace FileOperationHelpers.Test
{
    [TestFixture]
    public class ArchiverTest
    {
        private static string ExecutingFolderLocation = string.Empty;
        private static string RootArchiveLocation = string.Empty;
        private static string BuildLocation = string.Empty;

        

        private static void CopyAllBuildFilesAndFoldersTobin(DirectoryInfo originalFolder, DirectoryInfo outputFolder)
        {
            if (originalFolder == null) return;
            if (outputFolder == null) return;


            foreach (FileInfo file in originalFolder.GetFiles())
            {
                file.CopyTo(Path.Combine(outputFolder.FullName, file.Name));
            }

            foreach (DirectoryInfo directory in originalFolder.GetDirectories())
            {
                Directory.CreateDirectory(Path.Combine(outputFolder.FullName, directory.Name));
                CopyAllBuildFilesAndFoldersTobin(directory, new DirectoryInfo(Path.Combine(outputFolder.FullName, directory.Name)));
            }
        }

        [SetUp]
        public void TestInitialize()
        {
            //setup paths so we are not in bin debug folder.
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
            string buildLocation = Path.Combine(ExecutingFolderLocation, "ArtifactArhiveTestLocation", "build");

            if (d.Parent != null && d.Parent.Parent != null)
            {
                ExecutingFolderLocation = d.FullName;

                DirectoryInfo testFolder = d.Parent.Parent;
                DirectoryInfo[] directories = testFolder.GetDirectories(@"ArtifactArhiveTestLocation\build");
                if (directories.Length != 1)
                {
                    throw new ArgumentException("wrong number of build folders found, " + directories.Length);
                }

                if (Directory.Exists(buildLocation))
                {
                    Directory.Delete(buildLocation, true);
                }
                Directory.CreateDirectory(buildLocation);

                DirectoryInfo directoryInfo = new DirectoryInfo(buildLocation);
                directoryInfo.Attributes &= ~FileAttributes.ReadOnly;
                CopyAllBuildFilesAndFoldersTobin(directories[0], directoryInfo);
            }
            else
            {
                throw new ArgumentException("could not get back up to executing folder");
            }

            BuildLocation = buildLocation;
            RootArchiveLocation = Path.Combine(ExecutingFolderLocation, "ArtifactArhiveTestLocation", "StreamArchive");
            //ensure the archive folder is empty

            if (Directory.Exists(RootArchiveLocation)) Directory.Delete(RootArchiveLocation, true);
            if (!Directory.Exists(BuildLocation)) throw new ArgumentException("no build location found.");
        }

        [Test]
        public void TestDirectoryAndFilesAreValidForExclusions(string archiveLocation)
        {
            Assert.IsTrue(Directory.Exists(Path.Combine(archiveLocation, "bin")));
            Assert.IsTrue(Directory.Exists(Path.Combine(archiveLocation, "bin", "Debug")));
            Assert.IsTrue(Directory.Exists(Path.Combine(archiveLocation, "bin", "Release")));

            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "Archiver.cs")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "ArtifactArchivingConfigSettings.cs")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "ArtifactSolutionConfigSettings.cs")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "ConfigurationLoader.cs")));
            Assert.IsFalse(File.Exists(Path.Combine(archiveLocation, "ArtifactArchivier.csproj")));

            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "bin", "Debug", "ArtifactArchivier.dll")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "bin","Debug", "ArtifactArchivier.pdb")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "bin", "Debug", "CustomConfigurations.dll")));
            Assert.IsTrue(File.Exists(Path.Combine(archiveLocation, "bin", "Debug", "CustomConfigurations.pdb")));
        }

        
    }
}
