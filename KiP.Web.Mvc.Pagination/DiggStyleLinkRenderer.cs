using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace KiP.Web.Mvc.Pagination {
    /// <summary>
    /// Concrete renderer implementation. Generates HTML pager as a series of 
    /// links and span(s) for inactive links
    /// </summary>
    public class DiggStyleLinkRenderer : IRenderer {

        #region Fields

        /// <summary>
        /// Gap marker is an ellipsis sign (...) for "more pages"
        /// </summary>
        private const string GapMarker = @"<span class=""gap"">&hellip;</span>";

        #endregion

        #region Properties

        /// <summary>
        /// Paged collection to render pager for
        /// </summary>
        public IPagedList Collection { get; set; }

        /// <summary>
        /// ASP.NET MVC Html helper instance
        /// </summary>
        public HtmlHelper HtmlHelper { get; set; }

        /// <summary>
        /// Action name for links generation
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Controller name for links generation
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Pager container (for example - div)
        /// </summary>
        public string PagerContainer { get; set; }

        /// <summary>
        /// Pager html attributes
        /// </summary>
        public IDictionary<string, object> PagerHtmlAttributes { get; set; }

        /// <summary>
        /// Caption for previous label
        /// </summary>
        public string PreviousLabel { get; set; }

        /// <summary>
        /// Caption for next label
        /// </summary>
        public string NextLabel { get; set; }

        /// <summary>
        /// How many links are shown around current page
        /// </summary>
        public int InnerWindow { get; set; }

        /// <summary>
        /// How many links are shown around the first and the last page
        /// </summary>
        public int OuterWindow { get; set; }

        /// <summary>
        /// String separator between page links or spans
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// When true, 'next', 'previous' and page numbers should be rendered,
        /// when false - only 'next' and 'previous' should be rendered
        /// </summary>
        public bool WindowLinks { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns array of visible page numbers. 
        /// </summary>
        /// <example>
        /// For a paged collection with following characteristics:
        /// Page size: 10
        /// Total count: 1000
        /// Page count: 100
        /// Inner window: 4
        /// Outer window: 1
        /// this array will be returned [1,2,3,4,5,99,100], assuming that current page = 1.
        /// Assuming that current page = 7, this array will be returned [1,2,3,4,5,6,7,8,9,10,11,99,100]
        /// </example>
        /// <returns></returns>
        private IEnumerable<int> VisiblePageNumbers() {
            int windowFrom = Collection.CurrentPage - InnerWindow;
            int windowTo = Collection.CurrentPage + InnerWindow;

            if (windowTo > Collection.TotalPages) {
                windowFrom -= (windowTo - Collection.TotalPages);
                windowTo = Collection.TotalPages;
            }

            if (windowFrom < 1) {
                windowTo += 1 - windowFrom;
                windowFrom = 1;
                if (windowTo > Collection.TotalPages) {
                    windowTo = Collection.TotalPages;
                }
            }
            var visible = Range.New(1, Collection.TotalPages);
            var leftGap = Range.New(2 + OuterWindow, windowFrom, true);
            var rightGap = Range.New(windowTo + 1, (Collection.TotalPages - OuterWindow), true);
            int leftGapLast = (leftGap.First() > leftGap.Last()) ? leftGap.Last() - 1 : leftGap.Last() + 1;
            int rightGapLast = (rightGap.First() > rightGap.Last()) ? rightGap.Last() - 1 : rightGap.Last() + 1;
            if (leftGapLast - leftGap.First() > 1) {
                visible = visible.Except(leftGap);
            }
            if (rightGapLast - rightGap.First() > 1) {
                visible = visible.Except(rightGap);
            }
            return visible;
        }

        /// <summary>
        /// Returns collection of strings that represent generated links [and] span(s)
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> WindowedLinks() {
            var links = new List<string>();
            int? prev = null;
            foreach (int page in VisiblePageNumbers()) {
                if (prev != null && page > prev + 1) {
                    links.Add(GapMarker);
                }
                links.Add(LinkOrSpan(page, "current"));
                prev = page;
            }
            return links;
        }

        /// <summary>
        /// Generates a string for a link or for a span, depending on whether current page == given page.
        /// If current page == given page => span is generated,
        /// If current page != given page => link is generated.
        /// </summary>
        /// <param name="page">Page number (starting from 1) for which to generate either link or span</param>
        /// <param name="spanClass">Css class for span</param>
        /// <param name="text">Text for link or span</param>
        /// <returns></returns>
        private string LinkOrSpan(int? page, string spanClass, string text = null) {
            text = text ?? page.ToString();
            if (page.HasValue && page != Collection.CurrentPage) {
                return Link(page, text);
            }
            return Span(text, spanClass);
        }

        /// <summary>
        /// Generates html for a link
        /// </summary>
        /// <param name="page">page number to go to</param>
        /// <param name="linkText">link text</param>
        /// <returns></returns>
        private string Link(int? page, string linkText) {
            //url helper is used here instead of html helper because html helper doesn't allow to use html as link text
            //for example, this wouldn't work htmlHelper.ActionLink("&laquo;", ..,..
            var helper = new UrlHelper(HtmlHelper.ViewContext.RequestContext);
            var queryString = HtmlHelper.ViewContext.HttpContext.Request.QueryString;
            var routeValues = new RouteValueDictionary { { "page", page } };
            if (queryString != null) {
                foreach (string key in queryString.Keys) {
                    if (!routeValues.ContainsKey(key))
                        routeValues.Add(key, queryString[key]);
                }
            }
            return string.Format("<a href=\"{0}\">{1}</a>", helper.Action(ActionName, ControllerName, routeValues), linkText);
        }

        /// <summary>
        /// Generates html for a span
        /// </summary>
        /// <param name="text">span text</param>
        /// <param name="cssClass">span css class</param>
        /// <returns></returns>
        private string Span(string text, string cssClass) {
            return string.Format("<span class=\"{1}\">{0}</span>", text, cssClass);
        }

        private void CheckArgumentsAndThrow() {
            if (HtmlHelper == null) throw new InvalidOperationException("HtmlHelper can't be null");
            if (Collection == null) throw new InvalidOperationException("IPagedList can't be null");
            if (string.IsNullOrEmpty(PagerContainer)) throw new InvalidOperationException("PagerContainer can't be null or empty");
            if (Separator == null) throw new InvalidOperationException("Separator can't be null");
        }

        /// <summary>
        /// Actual heavy lifting method that generates the actual html for a complete html pager
        /// </summary>
        /// <returns></returns>
        public string GenerateHtml() {
            CheckArgumentsAndThrow();
            if (Collection.TotalPages < 2) return null;
            var links = WindowLinks ? WindowedLinks().ToList() : new List<string>();
            links.Insert(0, LinkOrSpan(Collection.PreviousPage, "disabled prev_page", PreviousLabel));
            links.Add(LinkOrSpan(Collection.NextPage, "disabled next_page", NextLabel));
            var tagBuilder = new TagBuilder(PagerContainer);
            tagBuilder.MergeAttributes(PagerHtmlAttributes);
            tagBuilder.InnerHtml = string.Join(Separator, links);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion
    }
}
