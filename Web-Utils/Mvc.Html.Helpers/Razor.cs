using System.IO;
using System.Web.Mvc;

namespace Mvc.Html.Helpers
{
    public class Razor
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
    }
}