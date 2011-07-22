using System.Linq;
using Xunit;

namespace KiP.Web.Mvc.Pagination.Tests.Unit {
    public class RangeTests {

        [Fact]
        public void New_ShouldGenerateAscendingInclusiveSequence_WhenStartLessOrEqualToEndAndExcludeLastIsFalse() {
            //let's generate ascending inclusive range
            var range = Range.New(1, 10);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, range.ToArray());
        }

        [Fact]
        public void New_ShouldGenerateDescendingInclusiveSequence_WhenStartGreaterThanEndAndExcludeLastIsFalse() {
            //let's generate descending inclusive range
            var range = Range.New(10, 1);
            Assert.Equal(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, range.ToArray());
        }

        [Fact]
        public void New_ShouldGenerateAscendingExclusiveSequence_WhenStartLessOrEqualToEndAndExcludeLastIsTrue() {
            //let's generate ascending exclusive range
            var range = Range.New(1, 10, true);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, range.ToArray());
        }

        [Fact]
        public void New_ShouldGenerateDescendingExclusiveSequence_WhenStartGreaterThanEndAndExcludeLastIsTrue() {
            //let's generate descending inclusive range
            var range = Range.New(10, 1, true);
            Assert.Equal(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 }, range.ToArray());
        }

        [Fact]
        public void New_ShouldGenerateSequenceWithOneElement_WhenStartEqualsEndAndExcludeLastIsTrue() {
            var range = Range.New(1, 1, true);
            Assert.Equal(new[] { 1 }, range.ToArray());
        }

        [Fact]
        public void New_ShouldGenerateSequenceWithOneElement_WhenStartEqualsEndAndExcludeLastIsFalse() {
            var range = Range.New(1, 1);
            Assert.Equal(new[] { 1 }, range.ToArray());
        }
    }
}
