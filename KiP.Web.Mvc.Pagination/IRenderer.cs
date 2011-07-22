using System.Collections.Generic;
using System.Web.Mvc;

namespace KiP.Web.Mvc.Pagination {
    /// <summary>
    /// Describes interface for pagination renderer.
    /// Such renderers render concrete HTML that represents pagination links
    /// </summary>
    public interface IRenderer {

        /// <summary>
        /// Paged collection to render pager for
        /// </summary>
        IPagedList Collection { get; set; }

        /// <summary>
        /// Instance of html helper
        /// </summary>
        HtmlHelper HtmlHelper { get; set; }

        /// <summary>
        /// Action name for links generation
        /// </summary>
        string ActionName { get; set; }

        /// <summary>
        /// Controller name for links generation
        /// </summary>
        string ControllerName { get; set; }

        /// <summary>
        /// Pager container
        /// </summary>
        string PagerContainer { get; set; }

        /// <summary>
        /// Pager html attributes
        /// </summary>
        IDictionary<string, object> PagerHtmlAttributes { get; set; }


        /// <summary>
        /// Caption for previous label
        /// </summary>
        string PreviousLabel { get; set; }

        /// <summary>
        /// Caption for next label
        /// </summary>
        string NextLabel { get; set; }

        /// <summary>
        /// How many links are shown around current page
        /// </summary>
        int InnerWindow { get; set; }

        /// <summary>
        /// How many links are shown around the first and the last page
        /// </summary>
        int OuterWindow { get; set; }

        /// <summary>
        /// String separator between page links
        /// </summary>
        string Separator { get; set; }

        /// <summary>
        /// When 'true', 'next', previous and page numbers should be rendered,
        /// when false - only 'next' and 'previous' should be rendered
        /// </summary>
        bool WindowLinks { get; set; }

        /// <summary>
        /// Actual heavy lifting method that generates the actual html
        /// </summary>
        /// <returns></returns>
        string GenerateHtml();
    }
}
