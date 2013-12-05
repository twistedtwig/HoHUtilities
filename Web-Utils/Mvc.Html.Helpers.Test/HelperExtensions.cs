using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Mvc.Html.Helpers;
using NUnit.Framework;

namespace HoHUtilities.Mvc.Html.Helpers.Test
{
    [TestFixture]
    public class HelperExtensions
    {
        private UrlHelper UrlHelper;
        private HtmlHelper Helper = null;

        [SetUp]
        public void Init()
        {
            var context = new MockHttpRequestContext();
            RequestContext requestContext = new RequestContext(context, new RouteData());
            UrlHelper = new UrlHelper(requestContext);
        }

        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectImgTagText()
        {
            MvcHtmlString htmlString = Helper.Image("this/is/my/source.jpg", "some alt text", UrlHelper);
            Assert.AreEqual("<img alt=\"some alt text\" src=\"this/is/my/source.jpg\" />", htmlString.ToHtmlString());
        }


        [Test]
        public void TestImageMvcHelperReturnsCorrecImagTagWithAllOptionalsEntered()
        {
            MvcHtmlString htmlString = Helper.Image("this/is/my/source.jpg", "some alt text", UrlHelper, new Dictionary<string, string>()
                                                                                                             {
                                                                                                                 { "class", "myclassname"},
                                                                                                                 { "id", "myid" },
                                                                                                                 { "style", "float: left; font-size: 1.5em"}
                                                                                                             });     
            Assert.AreEqual("<img alt=\"some alt text\" class=\"myclassname\" id=\"myid\" src=\"this/is/my/source.jpg\" style=\"float: left; font-size: 1.5em\" />", htmlString.ToHtmlString());
        }


        [Test]
        public void TestImageMvcHelperReturnsCorrecImagTagWitOnlyOptionalInlineStyleEntered()
        {
            MvcHtmlString htmlString = Helper.Image("this/is/my/source.jpg", "some alt text", UrlHelper, new Dictionary<string, string>()
                                                                                                             {
                                                                                                                 { "style", "float: left; font-size: 1.5em"}
                                                                                                             }); 
            Assert.AreEqual("<img alt=\"some alt text\" src=\"this/is/my/source.jpg\" style=\"float: left; font-size: 1.5em\" />", htmlString.ToHtmlString());
        }



        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectScriptTagText()
        {
            MvcHtmlString htmlString = Helper.JavaScript("this/is/my/source.js", UrlHelper);
            Assert.AreEqual("<script src=\"this/is/my/source.js\" type=\"text/javascript\"></script>", htmlString.ToHtmlString());
        }


        [Test]
        public void TestImageMvcHtmlHelperReturnsCorrectCSSTagText()
        {            
            MvcHtmlString htmlString = Helper.CSS("this/is/my/source.css", UrlHelper);
            Assert.AreEqual("<link href=\"this/is/my/source.css\" rel=\"stylesheet\" type=\"text/css\" />", htmlString.ToHtmlString());
        }
    }
}
