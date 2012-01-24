using System.Web.Mvc;
using NUnit.Framework;

namespace Mvc.Html.Helpers.Test
{
    [TestFixture]
    public class HelperExtensions
    {

        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectImgTagText()
        {
            HtmlHelper helper = null;
            MvcHtmlString htmlString = helper.Image("this/is/my/source.jpg", "some alt text");

            Assert.AreEqual("<img alt=\"some alt text\" src=\"this/is/my/source.jpg\" />", htmlString.ToHtmlString());
        }
    }
}
