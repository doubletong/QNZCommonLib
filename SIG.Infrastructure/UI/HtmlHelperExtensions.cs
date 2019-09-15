using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace SIG.Infrastructure.UI
{
    public static class HtmlHelperExtensions
    {
        private const string _jSViewDataName = "RenderJavaScript";
        private const string _styleViewDataName = "RenderStyle";

        /// <summary>
        /// 例： Html.AddJavaScript("http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js");
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="scriptURL"></param>
        public static void AddJavaScript(this HtmlHelper htmlHelper,string scriptURL)
        {
            if (htmlHelper.ViewContext.HttpContext
              .Items[HtmlHelperExtensions._jSViewDataName] is List<string> scriptList)
            {
                if (!scriptList.Contains(scriptURL))
                {
                    scriptList.Add(scriptURL);
                }
            }
            else
            {
                scriptList = new List<string>
                {
                    scriptURL
                };
                htmlHelper.ViewContext.HttpContext
                  .Items.Add(HtmlHelperExtensions._jSViewDataName, scriptList);
            }
        }
     
        public static MvcHtmlString RenderJavaScripts(this HtmlHelper HtmlHelper)
        {
            StringBuilder result = new StringBuilder();

            if (HtmlHelper.ViewContext.HttpContext.Items[HtmlHelperExtensions._jSViewDataName] is List<string> scriptList)
            {
                foreach (string script in scriptList)
                {
                    result.AppendLine(string.Format(
                      "<script type=\"text/javascript\" src=\"{0}\"></script>",
                      script));
                }
            }

            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// 例： Html.AddStyle("http://cdn.jquerytools.org/1.2.7/full/jquery.tools.min.js");
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="styleURL"></param>
        public static void AddStyle(this HtmlHelper htmlHelper, string styleURL)
        {
            if (htmlHelper.ViewContext.HttpContext
              .Items[HtmlHelperExtensions._styleViewDataName] is List<string> styleList)
            {
                if (!styleList.Contains(styleURL))
                {
                    styleList.Add(styleURL);
                }
            }
            else
            {
                styleList = new List<string>
                {
                    styleURL
                };
                htmlHelper.ViewContext.HttpContext
                  .Items.Add(HtmlHelperExtensions._styleViewDataName, styleList);
            }
        }

        public static MvcHtmlString RenderStyles(this HtmlHelper htmlHelper)
        {
            StringBuilder result = new StringBuilder();


            if (htmlHelper.ViewContext.HttpContext
              .Items[HtmlHelperExtensions._styleViewDataName] is List<string> styleList)
            {
                foreach (string script in styleList)
                {
                    result.AppendLine(string.Format(
                      "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />",
                      script));
                }
            }

            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}
