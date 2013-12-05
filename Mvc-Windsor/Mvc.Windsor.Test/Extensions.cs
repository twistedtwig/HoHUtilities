using System.Reflection;
using System.Web.Mvc;
using Castle.Windsor;
using NUnit.Framework;

namespace HoHUtilities.Mvc.Windsor.Test
{
    public class IsController : Controller { }
    public class IsAnotherController : Controller { }
    public class IsNotController { }

    [TestFixture]
    public class Extensions
    {
        private IWindsorContainer Container;

        [SetUp]
        public void Setup()
        {
            Container = new WindsorContainer();
        }

        [Test]
        public void TestRegisterSingleController()
        {
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));

            Container.RegisterController<IsController>();

            Assert.IsTrue(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));
        }


        [Test]
        public void TestRegisterMulitpleControllers()
        {
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));

            Container.RegisterControllers(new [] { typeof(IsController), typeof(IsAnotherController)});

            Assert.IsTrue(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsTrue(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));
        }


        [Test]
        public void TestRegisterAssemblyControllers()
        {
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));

            Container.RegisterControllers(new [] { Assembly.GetExecutingAssembly() });

            Assert.IsTrue(Container.Kernel.HasComponent(typeof(IsController)));
            Assert.IsTrue(Container.Kernel.HasComponent(typeof(IsAnotherController)));
            Assert.IsFalse(Container.Kernel.HasComponent(typeof(IsNotController)));
        }
    }
}
