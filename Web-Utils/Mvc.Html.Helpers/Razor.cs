using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Mvc.Html.Helpers
{
    public static class Razor
    {
        /// <summary>
        /// Takes the given view and turns the output into a string to be used with Razor, partialviews and Json.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewData"></param>
        /// <param name="tempData"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderRazorViewToString(ControllerContext context, ViewDataDictionary viewData, TempDataDictionary tempData, string viewName, object model)
        {
            viewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static IHtmlString AddResource(this HtmlHelper htmlHelper, Func<object, HelperResult> template, string type)
        {
            return AddResource(htmlHelper, template(null).ToString(), type);
        }

        public static IHtmlString AddResource(this HtmlHelper htmlHelper, string text, string type)
        {
            if (htmlHelper.ViewContext.HttpContext.Items[type] != null)
            {
                var list = (List<string>)htmlHelper.ViewContext.HttpContext.Items[type];
                if (!list.Contains(text))
                {
                    list.Add(text);
                }
            }
            else
            {
                htmlHelper.ViewContext.HttpContext.Items[type] = new List<string> { text };
            }

            return new HtmlString(String.Empty);
        }


        public static IHtmlString RenderResources(this HtmlHelper htmlHelper, string type)
        {
            if (htmlHelper.ViewContext.HttpContext.Items[type] != null)
            {
                var resources = (List<string>)htmlHelper.ViewContext.HttpContext.Items[type];

                foreach (var resource in resources)
                {
                    if (resource != null) htmlHelper.ViewContext.Writer.Write(resource);
                }
            }

            return new HtmlString(String.Empty);
        }
    }
}