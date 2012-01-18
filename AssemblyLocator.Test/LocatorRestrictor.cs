using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyLocator.Locators;
using NUnit.Framework;

namespace HoHUtilities.AssemblyLocator.Test
{
    [TestFixture]
    public class LocatorRestrictor
    {
        private Locators.ILocator Locator;

        [SetUp]
        protected void Setup()
        {
            Locator = new Locator();
        }


        [Test]
        public void AddFoldersToLocator()
        {
            Assert.AreEqual(0, Locator.AssemblyFolders.Count);

            Locator.AddFolder(@"C:\temp");
            Locator.AddFolder(@"C:\temp\dir1");
            Locator.AddFolder(@"C:\temp\dir1");
            Locator.AddFolder(@"C:\temp2\dir12");

            Assert.AreEqual(3, Locator.AssemblyFolders.Count);
        }


        //TODO: test that process data will find the assemblies (and only the assemblies given in the useagerules.
        //TODO: how to find assemblies, i.e. what assemblies can I setup in the test to ensure they are there to be found (can not use just these as that is not good test.. should use dependency folder

        
    }
}
