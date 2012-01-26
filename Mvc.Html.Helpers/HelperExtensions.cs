using System.Web.Mvc;

namespace HoHUtilities.Mvc.Html.Helpers
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Extension method on the HTML helper for razor that will allow for the creation of an html img link without having to type it all manually
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="altText"></param>
        /// <param name="url"> </param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, UrlHelper url)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url.Content(src));
            builder.MergeAttribute("alt", altText);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }


        /// <summary>
        /// Extension method on the HTML helper for razor that will allow for the creation of an html img link without having to type it all manually
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="altText"></param>
        /// <param name="url"> </param>
        /// <param name="className">name of the CSS class to be used for the image tag</param>
        /// <param name="idName">name of the CSS ID to be used for the image tag</param>
        /// <param name="inlineStyle">The inline CSS styling to be used with the image tag</param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, UrlHelper url, string className = "", string idName = "", string inlineStyle = "")
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url.Content(src));
            builder.MergeAttribute("alt", altText);

            if (!string.IsNullOrWhiteSpace(className))
            {
                builder.MergeAttribute("class", className);
            }

            if (!string.IsNullOrWhiteSpace(idName))
            {
                builder.MergeAttribute("id", idName);
            }

            if (!string.IsNullOrWhiteSpace(inlineStyle))
            {
                builder.MergeAttribute("style", inlineStyle);
            }

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Extension method on the HTML helper for razor that will allow for the creation of an html script tag without having to type it all manually
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="url"> </param>
        /// <returns></returns>
        public static MvcHtmlString JavaScript(this HtmlHelper helper, string src, UrlHelper url)
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            builder.MergeAttribute("src", url.Content(src));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }


        /// <summary>
        /// Extension method on the HTML helper for razor that will allow for the creation of an html CSS tag without having to type it all manually
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="url"> </param>
        /// <returns></returns>
        public static MvcHtmlString CSS(this HtmlHelper helper, string src, UrlHelper url)
        {
            var builder = new TagBuilder("link");
            builder.MergeAttribute("rel", "stylesheet");
            builder.MergeAttribute("type", "text/css");
            builder.MergeAttribute("href", url.Content(src));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
