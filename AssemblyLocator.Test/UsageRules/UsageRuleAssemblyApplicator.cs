using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssemblyLocator.UsageRules;
using NUnit.Framework;

namespace HoHUtilities.AssemblyLocator.Test.UsageRules
{
    [TestFixture]
    public class UsageRuleAssemblyApplicator
    {
        private const string BaseFolder = @"C:\temp\";
        private const string RootFolder = BaseFolder + @"RecursiveFolderTest";

        private const string Dir1 = RootFolder + @"\sub1";
        private const string Dir2 = RootFolder + @"\sub1\sb1";
        private const string Dir3 = RootFolder + @"\sub1\sb1\ssb1";
        private const string Dir4 = RootFolder + @"\sub1\sb1\ssb2";
        private const string Dir5 = RootFolder + @"\sub1\sb2";
        private const string Dir6 = RootFolder + @"\sub1\sb3";
        private const string Dir7 = RootFolder + @"\sub1\sb3\ssb1";
        private const string Dir8 = RootFolder + @"\sub1\sb3\ssb1\ssbb1";
        private const string Dir9 = RootFolder + @"\sub2";

        private const string Textfile = RootFolder + @"\testfile.txt";

        //"C:\Windows\System32\browser.dll"
        //"C:\Windows\System32\cabinet.dll"
        //"C:\Windows\System32\netbios.dll"
        FileInfo browser = new FileInfo(@"C:\Windows\System32\browser.dll");
        FileInfo cabinet = new FileInfo(@"C:\Windows\System32\cabinet.dll");
        FileInfo netbios = new FileInfo(@"C:\Windows\System32\netbios.dll");

        [SetUp]
        protected void Init()
        {
            if (!Directory.Exists(BaseFolder)) { Directory.CreateDirectory(BaseFolder); }
            if (Directory.Exists(RootFolder)) { CleanUp(); }

            Directory.CreateDirectory(RootFolder);

            Directory.CreateDirectory(Dir1);
            Directory.CreateDirectory(Dir2);
            Directory.CreateDirectory(Dir3);
            Directory.CreateDirectory(Dir4);
            Directory.CreateDirectory(Dir5);
            Directory.CreateDirectory(Dir6);
            Directory.CreateDirectory(Dir7);
            Directory.CreateDirectory(Dir8);
            Directory.CreateDirectory(Dir9);

            browser.CopyTo(string.Format(@"{0}\{1}", Dir1, browser.Name));
            cabinet.CopyTo(string.Format(@"{0}\{1}", Dir4, cabinet.Name));
            netbios.CopyTo(string.Format(@"{0}\{1}", RootFolder, netbios.Name));
            FileInfo file = new FileInfo(Textfile);
            if(!file.Exists)
            {
                StreamWriter streamWriter = file.CreateText();
                streamWriter.WriteLine("this is a test file");
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

//        [TearDown]
        protected void CleanUp()
        {
            if (Directory.Exists(RootFolder))
            {
                DirectoryInfo directory = new DirectoryInfo(RootFolder);
                directory.Delete(true);
            }  
        }


        [Test]
        public void ApplyingUsageRulesToAssemblyFoldersReturnsCorrectAssemblies()
        {
            //include dir2
            //include browser file directly
            //include textfile directly

            IList<UsageRule> usageRules = new List<UsageRule>
                    {
                      new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = Dir2, UsageRuleFunction = UsageRuleFunction.Include },
                      new UsageRule { MatchType = UsageRuleNameMatch.FullName, Name = browser.FullName, UsageRuleFunction = UsageRuleFunction.Include },
                      new UsageRule { MatchType = UsageRuleNameMatch.FullName, Name = Textfile, UsageRuleFunction = UsageRuleFunction.Include },
                    };

            IList<string> assemblyFolders = new List<string>();

            AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator assemblyApplicator = new AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator();
            IList<Assembly> assemblies = assemblyApplicator.ApplyUsageRulesToFolders(usageRules, assemblyFolders);

            Assert.AreEqual(2, assemblies.Count);
            IList<string> assemblyNames = assemblies.Select(x => x.FullName).ToList();

            Assert.Contains(cabinet.FullName, assemblyNames.ToArray());
            Assert.Contains(browser.FullName, assemblyNames.ToArray());
            
            //include file
            //exclude file

            //find all assemblies in given folder and add to list

            //search all usages for all includes that are valid files
            //check file is a valid assembly and add to list

            //search all usages for all excludes that are valid files
            //remove assembly from list


            //"C:\Windows\System32\browser.dll"
            //"C:\Windows\System32\cabinet.dll"
            //"C:\Windows\System32\netbios.dll"
        }


        [Test]
        public void ApplyUsageRulesToFoldersToFindAssemblies()
        {
            //this can fail due to restrictions or depenendcies... need to look at using MEF to load them all.
//            Assembly s = Assembly.LoadFrom(@"C:\hoh_code\Bailey\bailey\trunk\source\Bailey.DAL\bin\Debug\Bailey.Common.dll");
//            Assembly.UnsafeLoadFrom(@"C:\Windows\System32\browser.dll");

            IList<string> assemblyFolders = new List<string>() { Dir4, Dir8, Dir9 };
            IList<Assembly> assemblies = new List<Assembly>();

            AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator assemblyApplicator = new AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator();
            assemblyApplicator.FindAllAssembiesForGivenFolders(assemblyFolders, assemblies);

            Assert.AreEqual(1, assemblies.Count);
            IList<string> assemblyNames = assemblies.Select(x => x.FullName).ToList();
            Assert.Contains(cabinet.FullName, assemblyNames.ToArray());
        }

        [Test]
        public void FindAllAssembliesSpecifiedInUsageRules()
        {
            IList<UsageRule> usageRules = new List<UsageRule>
                    {
                      new UsageRule { MatchType = UsageRuleNameMatch.FullName, Name = browser.FullName, UsageRuleFunction = UsageRuleFunction.Include },
                      new UsageRule { MatchType = UsageRuleNameMatch.FullName, Name = Textfile, UsageRuleFunction = UsageRuleFunction.Include },
                    };

            IList<Assembly> assemblies = new List<Assembly>();

            AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator assemblyApplicator = new AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator();
            assemblyApplicator.FindAllAssembliesSpecifiedInUsageRules(usageRules, assemblies);

            Assert.AreEqual(1, assemblies.Count);
            IList<string> assemblyNames = assemblies.Select(x => x.FullName).ToList();
            Assert.Contains(browser.FullName, assemblyNames.ToArray());
        }


        [Test]
        public void RemoveAllExplicityExcludedAssemblies()
        {
            //TODO some how (probably using MEF) load an assembly
            IList<Assembly> assemblies = new List<Assembly>(); //load an assembly into list (browser)

            Assert.AreEqual(1, assemblies.Count);

            IList<UsageRule> usageRules = new List<UsageRule>
                    {
                      new UsageRule { Name = browser.FullName, UsageRuleFunction = UsageRuleFunction.Exclude },
                    };


            AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator assemblyApplicator = new AssemblyLocator.UsageRules.UsageRuleAssemblyApplicator();
            assemblyApplicator.RemoveAllExplicityExcludedAssemblies(usageRules, assemblies);

            Assert.AreEqual(0, assemblies.Count);
        }



//        public IList<Assembly> ApplyUsageRulesToAssemblies(bool excludeAllByDefault, IList<Assembly> assemblies, IList<UsageRule> usageRules)
//
//
//        public bool MatchAssemblyName(UsageRule usageRule, Assembly assembly)
//
//
//        public bool MatchAssemblyByNameOnly(Assembly assembly, string valueToMatch)
//        {
//            return assembly.GetName().Equals(valueToMatch);
//        }
//
//        public bool MatchAssemblyByFullName(Assembly assembly, string valueToMatch)
//        {
//            return assembly.FullName.Equals(valueToMatch);
//        }
//
//        public bool MatchAssemblyByFullDirectory(Assembly assembly, string valueToMatch)
//        {
//            return MatchAssemblyByFullDirectory(assembly.Location, valueToMatch);
//        }
//
//        public bool MatchAssemblyByDirectoryNameOnly(Assembly assembly, string valueToMatch)
//        {
//            return MatchAssemblyByDirectoryNameOnly(assembly.Location, valueToMatch);
//        }

    }
}
