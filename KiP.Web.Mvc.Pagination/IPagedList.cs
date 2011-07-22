
namespace KiP.Web.Mvc.Pagination {
    /// <summary>
    /// Determines the interface for paged list.
    /// A paged list is a subset of a list with additional info indicating total number of items,
    /// current item, etc.
    /// <remarks>
    /// Note to implementers, this interface assumes that pages start with 1
    /// </remarks>
    /// </summary>
    public interface IPagedList {
        int TotalCount { get; set; }
        /// <summary>
        /// Returns index of the previous page. If there is no previous page (current page is the first page), - null should be returned
        /// </summary>
        int? PreviousPage { get; }
        int CurrentPage { get; }
        /// <summary>
        /// Returns index of the next page. If there is no next page (current page is the last page), - null should be returned
        /// </summary>
        int? NextPage { get; }
        int LastPage { get; }
        int TotalPages { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsCurrentPage(int pageNumber);
    }
}
