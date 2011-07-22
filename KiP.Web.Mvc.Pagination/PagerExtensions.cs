using System.Web.Mvc;
using System.Web.Routing;

namespace KiP.Web.Mvc.Pagination {

    /// <summary>
    /// Html extensions to assist constructing pagination related HTML in asp.net views
    /// </summary>
    public static class PagerExtensions {
        /// <summary>
        /// Generates a digg-like pager - div with page numbers as links, previous and next links(or spans)
        /// </summary>
        /// <param name="htmlHelper">Instance of html helper</param>
        /// <param name="collection">Paged collection to render pager for</param>
        /// <param name="actionName">Action name for links generation (optional: default = null)</param>
        /// <param name="controllerName">Controller name for links generation (optional: default = null)</param>
        /// <param name="pagerHtmlAttributes">Object with html attributes for pager container</param>
        /// <param name="pagerContainer">Pager container (optional: default = "div")</param>
        /// <param name="previousLabel">Caption for previous label (optinoal: default = '« Previous')</param>
        /// <param name="nextLabel">Caption for next label (optional: default = 'Next »')</param>
        /// <param name="innerWindow">How many links are shown around current page (optional: default = 4)</param>
        /// <param name="outerWindow">How many links are shown around the first and the last pages (optional: default = 1)</param>
        /// <param name="separator">String separator between page links or spans (optional: default ' ' - space)</param>
        /// <param name="windowLinks">
        /// When true, 'next', 'previous' and page numbers should be rendered,
        /// when false, only 'next' and 'previous' should be rendered.
        /// Optional: default = true
        /// </param>
        /// <returns>System.Web.Mvc.MvcHtmlString</returns>
        public static MvcHtmlString Pager(
            this HtmlHelper htmlHelper,
            IPagedList collection,
            string actionName = null,
            string controllerName = null,
            object pagerHtmlAttributes = null,
            string pagerContainer = "div",
            string previousLabel = "&larr;",
            string nextLabel = "&rarr;",
            int innerWindow = 4,
            int outerWindow = 1,
            string separator = " ",
            bool windowLinks = true
            ) {
            IRenderer renderer = new DiggStyleLinkRenderer {
                HtmlHelper = htmlHelper,
                Collection = collection,
                ActionName = actionName,
                ControllerName = controllerName,
                PagerContainer = pagerContainer,
                InnerWindow = innerWindow,
                NextLabel = nextLabel,
                OuterWindow = outerWindow,
                PreviousLabel = previousLabel,
                Separator = separator,
                WindowLinks = windowLinks
            };
            renderer.PagerHtmlAttributes = new RouteValueDictionary(pagerHtmlAttributes);
            return MvcHtmlString.Create(renderer.GenerateHtml());
        }
    }
}
