using System.Collections.Generic;
using System.Web.Mvc;

namespace Mvc.Html.Helpers
{
    public static class HelperExtensions
    {
        public static MvcHtmlString ActionImageLink(this HtmlHelper helper, string src, string altText, UrlHelper url, string actionName, string controllerName)
        {
            return ActionImageLink(helper, src, altText, url, actionName, controllerName, null, null);
        }

        public static MvcHtmlString ActionImageLink(this HtmlHelper helper, string src, string altText, UrlHelper url, string actionName, string controllerName, Dictionary<string, string> linkAttributes, Dictionary<string, string> imageAttributes)
        {
            return ActionImageLink(helper, src, altText, url, actionName, controllerName, null, linkAttributes, imageAttributes);
        }

        public static MvcHtmlString ActionImageLink(this HtmlHelper helper, string src, string altText, UrlHelper url, string actionName, string controllerName, dynamic routeValues, Dictionary<string, string> linkAttributes, Dictionary<string, string> imageAttributes)
        {
            var linkBuilder = new TagBuilder("a");
            linkBuilder.MergeAttribute("href", routeValues == null ? url.Action(actionName, controllerName) : url.Action(actionName, controllerName, routeValues));

            var imageBuilder = new TagBuilder("img");
            imageBuilder.MergeAttribute("src", url.Content(src));
            imageBuilder.MergeAttribute("alt", altText);
          
            if (linkAttributes != null)
            {
                foreach (KeyValuePair<string, string> attribute in linkAttributes)
                {
                    if (!string.IsNullOrWhiteSpace(attribute.Key) && !string.IsNullOrWhiteSpace(attribute.Value))
                    {
                        linkBuilder.MergeAttribute(attribute.Key, attribute.Value);
                    }
                }
            }

            if (imageAttributes != null)
            {
                foreach (KeyValuePair<string, string> attribute in imageAttributes)
                {
                    if (!string.IsNullOrWhiteSpace(attribute.Key) && !string.IsNullOrWhiteSpace(attribute.Value))
                    {
                        imageBuilder.MergeAttribute(attribute.Key, attribute.Value);
                    }
                }
            }

            linkBuilder.InnerHtml = MvcHtmlString.Create(imageBuilder.ToString(TagRenderMode.SelfClosing)).ToString();
            return MvcHtmlString.Create(linkBuilder.ToString());
        }
        
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
        /// <param name="attributes">Dictionary of attributes, such as class = myclass.</param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, UrlHelper url, Dictionary<string, string> attributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url.Content(src));
            builder.MergeAttribute("alt", altText);

            if (attributes != null)
            {
                foreach (KeyValuePair<string, string> attribute in attributes)
                {
                    if (!string.IsNullOrWhiteSpace(attribute.Key) && !string.IsNullOrWhiteSpace(attribute.Value))
                    {
                        builder.MergeAttribute(attribute.Key, attribute.Value);
                    }
                }
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