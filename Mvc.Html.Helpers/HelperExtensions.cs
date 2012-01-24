using System.Web.Mvc;

namespace Mvc.Html.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Extension method on the HTML helper for razor that will allow for the creation of an html img link without having to type it all manually
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="altText"></param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
