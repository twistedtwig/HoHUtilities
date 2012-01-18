using System.Web.Mvc;
using NUnit.Framework;

namespace Mvc.Helpers.Test
{
    public class TestController : Controller
    {
        
    }
    
    public class NotTestController
    {
        
    }
    
    
    [TestFixture]
    public class Controllers
    {
        [Test]
        public void TestIsController()
        {
            TestController controller = new TestController();
            NotTestController notController = new NotTestController();

            Assert.IsTrue(Helpers.Controllers.IsController(controller.GetType()));
            Assert.IsFalse(Helpers.Controllers.IsController(notController.GetType()));
        }

    }


}
