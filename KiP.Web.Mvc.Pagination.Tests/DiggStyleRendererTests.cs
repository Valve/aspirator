using System;
using System.Collections.Generic;
using System.Linq;
using KiP.Web.Mvc.Tests.Unit.Infrastructure;
using Xunit;

namespace KiP.Web.Mvc.Pagination.Tests.Unit {

    public class DiggStyleLinkRendererTests {

        #region GenerateHtml tests

        [Fact]
        public void GenerateHtml_ShouldThrowInvalidOperationException_When_GivenNullHtmlHelper() {

            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            renderer.HtmlHelper = null;
            Assert.Throws<InvalidOperationException>(() => renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldThrowInvalidOperationException_When_GivenNullCollection() {
            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            renderer.Collection = null;
            Assert.Throws<InvalidOperationException>(() => renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldThrowInvalidOperationException_When_GivenNullPagerContainer() {
            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            renderer.PagerContainer = null;
            Assert.Throws<InvalidOperationException>(() => renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldThrowInvalidOperationException_When_GivenEmptyPagerContainer() {
            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            renderer.PagerContainer = string.Empty;
            Assert.Throws<InvalidOperationException>(() => renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldThrowInvalidOperationException_When_GivenNullSeparator() {
            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            renderer.Separator = null;
            Assert.Throws<InvalidOperationException>(() => renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldReturnNull_When_GivenEmptyPagedCollection() {
            var renderer = CreateValidRenderer(CreateEmptyPagedList());
            Assert.Null(renderer.GenerateHtml());
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtml_When_GivenValidParameters() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldGeneratePagerWithoutWindowedLinks_When_WindowLinksIsFalse() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.WindowLinks = false;
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithoutHtmlAttributes_When_GivenValidParametersWithoutHtmlAttributes() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.PagerHtmlAttributes = null;
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificPagerContainer_When_GivenValidParametersWithSpecificPagerContainer() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.PagerContainer = "p";
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<p class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">next</a></p>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificPreviousLink_When_GivenValidParametersWithSpecificPreviousLink() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.PreviousLabel = "blaster!";
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">blaster!</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificNextLink_When_GivenValidParametersWithSpecificNextLink() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.NextLabel = "blaster next!";
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">blaster next!</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificNumberOfLinksAroundCurrentPage_When_GivenValidParametersWithSpecificNumberOfLinksAroundCurrentPageAndCurrentPageIsFirst() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.InnerWindow = 3;//3 links should be around current page on either side (totalling 6 links). If current page is not in the middle of the pager, 6 links should be displayed too.
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <a href=\"\">4</a> <a href=\"\">5</a> <a href=\"\">6</a> <a href=\"\">7</a> <span class=\"gap\">&hellip;</span> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificNumberOfLinksAroundCurrentPage_When_GivenValidParametersWithSpecificNumberOfLinksAroundCurrentPageAndCurrentPageIsInTheMiddle() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            //let's set current page = 5
            pagedCollection.PageIndex = 4;
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.InnerWindow = 3;//3 links should be around current page on either side (totalling 6 links). If current page is not in the middle of the pager, 6 links should be displayed too.
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><a href=\"\">previous</a> <a href=\"\">1</a> <a href=\"\">2</a> <a href=\"\">3</a> <a href=\"\">4</a> <span class=\"current\">5</span> <a href=\"\">6</a> <a href=\"\">7</a> <a href=\"\">8</a> <a href=\"\">9</a> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificOuterWindow_When_GivenValidParametersWithSpecificOuterWindowAndCurrentPageIsFirst() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.OuterWindow = 3;//4 links should be after first page and before last page, but only if current page is in the middle, if current page is first, the outer window only applies to pager tail and vice versa
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span> <span class=\"current\">1</span> <a href=\"\">2</a> <a href=\"\">3</a> <span class=\"gap\">&hellip;</span> <a href=\"\">7</a> <a href=\"\">8</a> <a href=\"\">9</a> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificOuterWindow_When_GivenValidParametersWithSpecificOuterWindowAndCurrentPageIsInTheMiddle() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            //let's set current page = 5
            pagedCollection.PageIndex = 4;
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.OuterWindow = 3;//4 links should be after first page and before last page, but only if current page is in the middle, if current page is first, the outer window only applies to pager tail and vice versa
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><a href=\"\">previous</a> <a href=\"\">1</a> <a href=\"\">2</a> <a href=\"\">3</a> <a href=\"\">4</a> <span class=\"current\">5</span> <a href=\"\">6</a> <a href=\"\">7</a> <a href=\"\">8</a> <a href=\"\">9</a> <a href=\"\">10</a> <a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }

        [Fact]
        public void GenerateHtml_ShouldReturnValidHtmlWithSpecificSeparator_When_GivenValidParametersWithSpecificSeparator() {
            //let's generate 100 items in a paged collection, with page size = 10 items
            var pagedCollection = CreatePagedList(Enumerable.Range(1, 100), 0, 10);
            var renderer = CreateValidRenderer(pagedCollection);
            renderer.Separator = "~";
            string actualHtml = renderer.GenerateHtml();
            string expectedHtml =
                "<div class=\"pagination\"><span class=\"disabled prev_page\">previous</span>~<span class=\"current\">1</span>~<a href=\"\">2</a>~<a href=\"\">3</a>~<span class=\"gap\">&hellip;</span>~<a href=\"\">10</a>~<a href=\"\">next</a></div>";
            Assert.Equal(actualHtml, expectedHtml);
        }


        #region Helpers

        private static IPagedList CreateEmptyPagedList() {
            return CreatePagedList(new int[] { }, 0, 10);
        }

        private static IPagedList CreatePagedList<T>(IEnumerable<T> items, int index, int pageSize) {
            return new PagedList<T>(items.AsQueryable(), index, pageSize);
        }

        private static DiggStyleLinkRenderer CreateValidRenderer(IPagedList collection) {
            return new DiggStyleLinkRenderer {
                Collection = collection,
                HtmlHelper = MvcHelper.GetHtmlHelper(),
                InnerWindow = 1,
                NextLabel = "next",
                OuterWindow = 0,
                PagerContainer = "div",
                PagerHtmlAttributes = new Dictionary<string, object> { { "class", "pagination" } },
                PreviousLabel = "previous",
                Separator = " ",
                WindowLinks = true
            };
        }

        #endregion

        #endregion

    }
}
