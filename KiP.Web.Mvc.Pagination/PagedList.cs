using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace KiP.Web.Mvc.Pagination {
    /// <summary>
    /// IPagedList implementation. This class is a paged collection of items, where each page is a subset of a larger set of items.
    /// </summary>
    /// <remarks>
    /// This is a sample implementation of a paged list, customize it to your needs
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : Collection<T>, IPagedList {

        protected PagedList() { }

        public PagedList(IQueryable<T> source, int index, int pageSize) {
            TotalCount = source.Count();
            PageSize = pageSize;
            PageIndex = index;
            TotalPages = 1;
            CalcPages();
            AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }



        private void CalcPages() {
            if (PageSize > 0 && TotalCount > PageSize) {
                TotalPages = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                    TotalPages++;
            }
        }

        public void AddRange(IEnumerable<T> items) {
            foreach (T item in items) { Add(item); }
        }

        protected void AddRange(IEnumerable<T> source, int index, int pageSize, int totalCount) {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = index;
            TotalPages = 1;
            CalcPages();
            AddRange(source);
        }

        public int? PreviousPage {
            get {
                if (TotalPages < 2 || CurrentPage == 1) {
                    return null;
                }
                return CurrentPage - 1;
            }
        }

        public int CurrentPage {
            get { return PageIndex + 1; }
        }

        public int? NextPage {
            get {
                if (TotalPages == CurrentPage || TotalPages < 2) {
                    return null;
                }
                return CurrentPage + 1;
            }
        }

        public int LastPage {
            get { return CurrentPage - 1; }
        }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage {
            get { return (PageIndex * PageSize) <= TotalCount; }
        }
        public bool IsCurrentPage(int pageNumber) {
            return pageNumber == CurrentPage;
        }
    }
}
