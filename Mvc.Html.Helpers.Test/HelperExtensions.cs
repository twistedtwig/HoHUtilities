using System.Web.Mvc;
using NUnit.Framework;

namespace HoHUtilities.Mvc.Html.Helpers.Test
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


        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectScriptTagText()
        {
            HtmlHelper helper = null;
            MvcHtmlString htmlString = helper.JavaScript("this/is/my/source.js");

            Assert.AreEqual("<script src=\"this/is/my/source.js\" type=\"text/javascript\" />", htmlString.ToHtmlString());
        }


        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectCSSTagText()
        {
            HtmlHelper helper = null;
            MvcHtmlString htmlString = helper.CSS("this/is/my/source.css");

            Assert.AreEqual("<link href=\"this/is/my/source.css\" rel=\"stylesheet\" type=\"text/css\" />", htmlString.ToHtmlString());
        }
    }
}
