using System.Collections.Generic;

namespace KiP.Web.Mvc.Pagination {

    /// <summary>
    /// Static class to generate enumerable ranges
    /// </summary>
    public static class Range {


        /// <summary>
        /// Generates a range.
        /// If first element is not greater than the second, a non-descending range is generated.
        /// If first element is not less than the second, a descending range is generated.
        /// </summary>
        /// <remarks>
        /// Range doens't contain all elements in memory, it yields them as needeed as IEnumerable.
        /// If start == end, the range will yield only one element (start)
        /// </remarks>
        /// <param name="start">Starting element of the range</param>
        /// <param name="end">Ending element of the range</param>
        /// <param name="excludeLast">Should last element be excluded from the range (optional: default = false)</param>
        /// <returns></returns>
        public static IEnumerable<int> New(int start, int end, bool excludeLast = false) {
            if (start <= end) return NewAscending(start, end, excludeLast);
            return NewDescending(start, end, excludeLast);
        }

        /// <summary>
        /// Generates ascending range.
        /// If start == end, the range will yield only one element (start)
        /// </summary>
        /// <param name="start">Starting element of the range</param>
        /// <param name="end">Ending element of the range</param>
        /// <param name="excludeLast">Should last element be excluded from the range (optional: default = false)</param>
        /// <returns></returns>
        private static IEnumerable<int> NewAscending(int start, int end, bool excludeLast = false) {
            if (start == end) yield return start;
            else {
                for (int i = start; i <= (excludeLast ? end - 1 : end); i++) {
                    yield return i;
                }
            }
        }
        /// <summary>
        /// Generates descending range.
        /// If start == end, the range will yield only one element (start)
        /// </summary>
        /// <param name="start">Starting element of the range</param>
        /// <param name="end">Ending element of the range</param>
        /// <param name="excludeLast">Should last element be excluded from the range (optional: default = false)</param>
        /// <returns></returns>
        private static IEnumerable<int> NewDescending(int start, int end, bool excludeLast = false) {
            if (start == end) yield return start;
            else {
                for (int i = start; i >= (excludeLast ? end + 1 : end); i--) {
                    yield return i;
                }
            }
        }
    }
}
