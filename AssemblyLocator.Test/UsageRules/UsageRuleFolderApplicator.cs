using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssemblyLocator.UsageRules;
using NUnit.Framework;

namespace AssemblyLocator.Test.UsageRules
{
    [TestFixture]
    public class UsageRuleFolderApplicator
    {
        private const string BaseFolder = @"C:\temp\";
        private const string RootFolder = BaseFolder + @"RecursiveFolderTest";
        private const int AllFoldersCount = 10;

        private const string Dir1 = RootFolder + @"\sub1";
        private const string Dir2 = RootFolder + @"\sub1\sb1";
        private const string Dir3 = RootFolder + @"\sub1\sb1\ssb1";
        private const string Dir4 = RootFolder + @"\sub1\sb1\ssb2";
        private const string Dir5 = RootFolder + @"\sub1\sb2";
        private const string Dir6 = RootFolder + @"\sub1\sb3";
        private const string Dir7 = RootFolder + @"\sub1\sb3\ssb1";
        private const string Dir8 = RootFolder + @"\sub1\sb3\ssb1\ssbb1";
        private const string Dir9 = RootFolder + @"\sub2";

        private AssemblyLocator.UsageRules.UsageRuleFolderApplicator RuleApplicator;

        [SetUp]
        protected void Init()
        {
            if(!Directory.Exists(BaseFolder)) { Directory.CreateDirectory(BaseFolder); }
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

            RuleApplicator = new AssemblyLocator.UsageRules.UsageRuleFolderApplicator();
        }

        [TearDown]
        protected void CleanUp()
        {
            if (Directory.Exists(RootFolder))
            {
                DirectoryInfo directory = new DirectoryInfo(RootFolder);
                directory.Delete(true);    
            }            
        }

        [Test]
        public void AddAllRecursiveIncludeUsagesToFolderFullyRecursive()
        {
            //IList<UsageRule> usageRules, IList<string> assemblyFolders
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Include, UsageRuleDepth = UsageRuleDepth.FullyRecursive }
            };

            IList<string> assemblyFolders = new List<string>();

            RuleApplicator.AddAllRecursiveIncludeUsagesToFolder(usageRules, assemblyFolders);

            Assert.AreEqual(AllFoldersCount, assemblyFolders.Count);
            Assert.Contains(RootFolder, assemblyFolders.ToArray());
            Assert.Contains(Dir1, assemblyFolders.ToArray());
            Assert.Contains(Dir2, assemblyFolders.ToArray());
            Assert.Contains(Dir3, assemblyFolders.ToArray());
            Assert.Contains(Dir4, assemblyFolders.ToArray());
            Assert.Contains(Dir5, assemblyFolders.ToArray());
            Assert.Contains(Dir6, assemblyFolders.ToArray());
            Assert.Contains(Dir7, assemblyFolders.ToArray());
            Assert.Contains(Dir8, assemblyFolders.ToArray());
            Assert.Contains(Dir9, assemblyFolders.ToArray());
        }


        [Test]
        public void AddAllRecursiveIncludeUsagesToFolderWithDepthGiven()
        {
            //IList<UsageRule> usageRules, IList<string> assemblyFolders
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Include, UsageRuleDepth = UsageRuleDepth.RecursiveDepth, FunctionParameter = "3"}
            };

            IList<string> assemblyFolders = new List<string>();

            RuleApplicator.AddAllRecursiveIncludeUsagesToFolder(usageRules, assemblyFolders);

            Assert.AreEqual(AllFoldersCount - 1, assemblyFolders.Count);
            Assert.Contains(Dir1, assemblyFolders.ToArray());            
            Assert.Contains(Dir2, assemblyFolders.ToArray());            
            Assert.Contains(Dir3, assemblyFolders.ToArray());            
            Assert.Contains(Dir4, assemblyFolders.ToArray());            
            Assert.Contains(Dir5, assemblyFolders.ToArray());            
            Assert.Contains(Dir6, assemblyFolders.ToArray());            
            Assert.Contains(Dir7, assemblyFolders.ToArray());            
            Assert.Contains(Dir9, assemblyFolders.ToArray());            
        }


        [Test]    
        public void AddAllRecursiveIncludeUsagesToFolderCurrentFolderOnly()
        {
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Include, UsageRuleDepth = UsageRuleDepth.CurrentFolderOnly }
            };

            IList<string> assemblyFolders = new List<string>();

            RuleApplicator.AddAllRecursiveIncludeUsagesToFolder(usageRules, assemblyFolders);

            Assert.AreEqual(1, assemblyFolders.Count);
            Assert.Contains(RootFolder, assemblyFolders.ToArray());
        }


        [Test]
        public void RemoveAllRecursiveExcludeUsagesFromFolderFullyRecursive()
        {
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = Dir2, UsageRuleFunction = UsageRuleFunction.Exclude, UsageRuleDepth = UsageRuleDepth.FullyRecursive }
            };

            IList<string> assemblyFolders = new List<string> { RootFolder, Dir1, Dir2, Dir3, Dir4, Dir5, Dir6, Dir7, Dir8, Dir9, };

            RuleApplicator.RemoveAllRecursiveExcludeUsagesFromFolder(usageRules, assemblyFolders);

            Assert.AreEqual(AllFoldersCount - 3, assemblyFolders.Count);
            Assert.Contains(RootFolder, assemblyFolders.ToArray());
            Assert.Contains(Dir1, assemblyFolders.ToArray());
            Assert.Contains(Dir5, assemblyFolders.ToArray());
            Assert.Contains(Dir6, assemblyFolders.ToArray());
            Assert.Contains(Dir7, assemblyFolders.ToArray());
            Assert.Contains(Dir8, assemblyFolders.ToArray());
            Assert.Contains(Dir9, assemblyFolders.ToArray());


            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir2));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir3));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir4));
        }

        [Test]
        public void RemoveAllRecursiveExcludeUsagesFromFolderWithDepthGiven()
        {
            //IList<UsageRule> usageRules, IList<string> assemblyFolders
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Exclude, UsageRuleDepth = UsageRuleDepth.RecursiveDepth, FunctionParameter = "2" }
            };

            IList<string> assemblyFolders = new List<string> { RootFolder, Dir1, Dir2, Dir3, Dir4, Dir5, Dir6, Dir7, Dir8, Dir9, };

            RuleApplicator.RemoveAllRecursiveExcludeUsagesFromFolder(usageRules, assemblyFolders);

            Assert.AreEqual(AllFoldersCount - 6, assemblyFolders.Count);
            Assert.Contains(Dir3, assemblyFolders.ToArray());
            Assert.Contains(Dir4, assemblyFolders.ToArray());
            Assert.Contains(Dir7, assemblyFolders.ToArray());
            Assert.Contains(Dir8, assemblyFolders.ToArray());

            Assert.That(assemblyFolders.ToArray(), Has.No.Member(RootFolder));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir1));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir2));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir5));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir6));
            Assert.That(assemblyFolders.ToArray(), Has.No.Member(Dir9));
        }

        [Test]
        public void RemoveAllRecursiveExcludeUsagesFromFolderCurrentFolderOnly()
        {
            //IList<UsageRule> usageRules, IList<string> assemblyFolders
            IList<UsageRule> usageRules = new List<UsageRule> 
            { 
                new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Exclude, UsageRuleDepth = UsageRuleDepth.CurrentFolderOnly }
            };

            IList<string> assemblyFolders = new List<string> { RootFolder, Dir1, Dir2, Dir3, Dir4, Dir5, Dir6, Dir7, Dir8, Dir9, };

            RuleApplicator.RemoveAllRecursiveExcludeUsagesFromFolder(usageRules, assemblyFolders);

            Assert.AreEqual(AllFoldersCount - 1, assemblyFolders.Count);
            Assert.Contains(Dir1, assemblyFolders.ToArray());
            Assert.Contains(Dir2, assemblyFolders.ToArray());
            Assert.Contains(Dir3, assemblyFolders.ToArray());
            Assert.Contains(Dir4, assemblyFolders.ToArray());
            Assert.Contains(Dir5, assemblyFolders.ToArray());
            Assert.Contains(Dir6, assemblyFolders.ToArray());
            Assert.Contains(Dir7, assemblyFolders.ToArray());
            Assert.Contains(Dir8, assemblyFolders.ToArray());
            Assert.Contains(Dir9, assemblyFolders.ToArray());

            Assert.That(assemblyFolders.ToArray(), Has.No.Member(RootFolder));            
        }

        [Test]
        public void TestExplicitExcludeOverRidesExplicitIncludeUsageRules()
        {
            //IList<UsageRule> usageRules, IList<string> assemblyFolders
            UsageRule excludeUsageRule = new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Exclude, UsageRuleDepth = UsageRuleDepth.CurrentFolderOnly };
            UsageRule includeUsageRule= new UsageRule { MatchType = UsageRuleNameMatch.DirectoryFullName, Name = RootFolder, UsageRuleFunction = UsageRuleFunction.Include, UsageRuleDepth = UsageRuleDepth.CurrentFolderOnly };
            IList<UsageRule> combinedUsageRules = new List<UsageRule>  {  includeUsageRule, excludeUsageRule, };

            IList<string> assemblyFolders = new List<string>();

            //prove that it will find the folder for include
            RuleApplicator.AddAllRecursiveIncludeUsagesToFolder(new List<UsageRule> { includeUsageRule }, assemblyFolders );
            Assert.AreEqual(1, assemblyFolders.Count);
            Assert.Contains(RootFolder, assemblyFolders.ToArray());

            //prove that it will Not get it the folder for exclude
            assemblyFolders = new List<string> { RootFolder };
            RuleApplicator.RemoveAllRecursiveExcludeUsagesFromFolder(new List<UsageRule> { excludeUsageRule}, assemblyFolders );
            Assert.AreEqual(0, assemblyFolders.Count);

            assemblyFolders = new List<string> { RootFolder, Dir6 };
            RuleApplicator.RemoveAllRecursiveExcludeUsagesFromFolder(new List<UsageRule> { excludeUsageRule }, assemblyFolders);
            Assert.AreEqual(1, assemblyFolders.Count);
            Assert.Contains(Dir6, assemblyFolders.ToArray());

            //prove that used in combined list exclude will override include
            assemblyFolders = RuleApplicator.ApplyAllUsageRules(combinedUsageRules);
            Assert.AreEqual(0, assemblyFolders.Count);
        }


        [Test]
        public void RescurivelyAddChildDirectoriesToListFullyRecursive()
        {
            IList<string> assemblyFolders = new List<string>();
            RuleApplicator.RescurivelyAddChildDirectoriesToList(new DirectoryInfo(RootFolder), assemblyFolders, 0, int.MaxValue);

            Assert.AreEqual(AllFoldersCount - 1, assemblyFolders.Count);
            Assert.Contains(Dir1, assemblyFolders.ToArray());
            Assert.Contains(Dir2, assemblyFolders.ToArray());
            Assert.Contains(Dir3, assemblyFolders.ToArray());
            Assert.Contains(Dir4, assemblyFolders.ToArray());
            Assert.Contains(Dir5, assemblyFolders.ToArray());
            Assert.Contains(Dir6, assemblyFolders.ToArray());
            Assert.Contains(Dir7, assemblyFolders.ToArray());
            Assert.Contains(Dir8, assemblyFolders.ToArray());
            Assert.Contains(Dir9, assemblyFolders.ToArray());
        }

        [Test]
        public void RescurivelyAddChildDirectoriesToListRecursiveDepthTest()
        {
            IList<string> assemblyFolders = new List<string>();
            RuleApplicator.RescurivelyAddChildDirectoriesToList(new DirectoryInfo(RootFolder), assemblyFolders, 0, 2);

            Assert.AreEqual(AllFoldersCount - 5, assemblyFolders.Count);
            Assert.Contains(Dir1, assemblyFolders.ToArray());
            Assert.Contains(Dir2, assemblyFolders.ToArray());
            Assert.Contains(Dir5, assemblyFolders.ToArray());
            Assert.Contains(Dir6, assemblyFolders.ToArray());
            Assert.Contains(Dir9, assemblyFolders.ToArray());
        }

    }
}
