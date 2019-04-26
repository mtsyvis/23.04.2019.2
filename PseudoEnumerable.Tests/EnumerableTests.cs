using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests

        [TestCase(arg1: new int[] { 2, -10, 13, 55, -33, 22 }, arg2: new int[] { 2, -10, 22 })]
        [TestCase(arg1: new int[] { 2, -2, 4, 6 }, arg2: new int[] { 2, -2, 4, 6 })]
        [TestCase(arg1: new int[] { 3, -1, 5, 7 }, arg2: new int[] { })]
        public void FilterTest_EvenNumberFilterFunkDelegateTest(IEnumerable<int> actualArray, IEnumerable<int> expectedArray)
        {
            CollectionAssert.AreEqual(expectedArray, actualArray.Filter(x => x % 2 == 0));
        }

        [Test]
        public void FilterTest_PredicateIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(new int[] { }, null));
        }

        [Test]
        public void FilterTest_SourceIsNull_ThrowArgumentNullException()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(source, x => x > 0));
        }

        #endregion

        #region ForAll tests

        [TestCase(new int[] { 2, 4, 6, 8, -10 }, ExpectedResult = true)]
        [TestCase(new int[] { 2 }, ExpectedResult = true)]
        [TestCase(new int[] { }, ExpectedResult = true)]
        [TestCase(new int[] { 3, 4, 6 }, ExpectedResult = false)]
        [TestCase(new int[] { 3, 5, -3 }, ExpectedResult = false)]
        public bool ForAll_EvenNumber(int[] source) => source.ForAll(x => x % 2 == 0);

        [Test]
        public void ForAllTest_PredicateIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.ForAll(new int[] { 1, 3 }, null));
        }

        #endregion

        #region CastTo tests

        [TestCase(arg1: new object[] { 1, 2, 4 }, arg2: new int[] { 1, 2, 4 })]
        [TestCase(arg1: new int[] { 1, 2, 4 }, arg2: new int[] { 1, 2, 4 })]
        public void CustToTest_ObjectToInt(IEnumerable source, IEnumerable<int> expectedArray)
        {
            Assert.AreEqual(expectedArray, Enumerable.CastTo<int>(source));
        }

        [Test]
        public void CustToTest_ObjectToString()
        {
            var actualArray = new object[] { "string1", "str", "123" };
            var expectedArray = new string[] { "string1", "str", "123" };

            Assert.AreEqual(expectedArray, Enumerable.CastTo<string>(actualArray));
        }

        [TestCase(arg: new object[] { 34, 13, 18, 13D })]
        [TestCase(arg: new object[] { 34, 123, "sdf" })]
        [TestCase(arg: new object[] { "123", 34, 123D })]
        public void CustToTest_DifferentTypesToInt_ThrowInvalidCastException(IEnumerable source)
        {
            using (var iterator = Enumerable.CastTo<int>(source).GetEnumerator())
            {
                Assert.Throws<InvalidCastException>(() =>
                        {
                            while (iterator.MoveNext()) { }
                        });
            }
        }

        #endregion

        #region Transform tests

        [TestCase(arg1: new[] { 2, 3, 4 }, arg2: new string[] { "2", "3", "4" })]
        [TestCase(arg1: new[] { 234, -23, 0 }, arg2: new string[] { "234", "-23", "0" })]
        public void TransformTest_ConvertIntToString(IEnumerable<int> actualArray, IEnumerable<string> expectedArray)
        {
            CollectionAssert.AreEqual(expectedArray, actualArray.Transform(x => x.ToString()));
        }

        #endregion

        #region Generate sequece tests

        [TestCase(5, 3, ExpectedResult = new int[] { 5, 6, 7 })]
        [TestCase(0, 3, ExpectedResult = new int[] { 0, 1, 2 })]
        [TestCase(2, 8, ExpectedResult = new int[] { 2, 3, 4, 5, 6, 7, 8, 9 })]
        public IEnumerable<int> GenerateSequenceTest(int start, int count) => Enumerable.GenerateSequence(start, count);

        [Test]
        public void GenerateSequenceTest_StartLessThen0_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.GenerateSequence(-10, 5));
        }

        #endregion

        #region SortBy key

        [TestCase(arg: new int[] { 2, 4, 6, 6, 2, 3, 7 }, ExpectedResult = new int[] { 2, 2, 3, 4, 6, 6, 7 })]
        [TestCase(
            new int[] { -3, int.MinValue, int.MaxValue, 100, 0 },
            ExpectedResult = new int[] { int.MinValue, -3, 0, 100, int.MaxValue })]
        [TestCase(new int[] { }, ExpectedResult = new int[] { })]
        public IEnumerable<int> SortByTest_IntArrayByOneselfAscending(IEnumerable<int> source) => source.SortBy(x => x);

        [TestCase(arg: new int[] { 4, 5, 2, 3, 7 }, ExpectedResult = new int[] { 5, 3, 7, 4, 2 })]
        [TestCase(arg: new int[] { 5, 6, 6, 5 }, ExpectedResult = new int[] { 5, 5, 6, 6 })]
        public IEnumerable<int> SortByTest_EvenNumberAscending(IEnumerable<int> source) => source.SortBy(x => x % 2 == 0);

        [TestCase(
            arg: new string[] { "ccc", "bbb", "nnn", "aaa" },
            ExpectedResult = new string[] { "nnn", "ccc", "bbb", "aaa" })]
        public IEnumerable<string> SortByDescendingTest_StringArrayByOneself(IEnumerable<string> source) =>
            source.SortByDescending(s => s);

        [TestCase(arg: new int[] { 4, 5, 2, 3, 7 }, ExpectedResult = new int[] { 7, 5, 4, 3, 2 })]
        [TestCase(
            arg: new int[] { int.MinValue, 0, -100, int.MaxValue, 100, 100 },
            ExpectedResult = new int[] { int.MaxValue, 100, 100, 0, -100, int.MinValue })]
        public IEnumerable<int> SortByDescendingTest_IntegerArrayByOneself(IEnumerable<int> source) =>
            source.SortByDescending(x => x);

        [Test]
        public void SortByTest_ByStringLengthAscending_AndShowLazyExecution()
        {
            var actualArray = new List<string> { "12", "", "12345", "123", "1234", "1" };
            var expectedArray = new List<string> { "", "1", "12", "123", "1234", "12345", "123456" };

            var temp = actualArray.SortBy(str => str.Length);
            actualArray.Add("123456");

            Assert.AreEqual(expectedArray, temp);
        }

        #endregion

        #region Sort by key with comparer

        [TestCase(
            arg: new string[] { "1267", "222", "2342", "345", "2312222", "vasya" },
            ExpectedResult = new string[] { "345", "vasya", "1267", "2342", "222", "2312222" })]
        public IEnumerable<string> SortBy_StringArrayByOneselfUsingNumberOfEntriesComparerAscending(IEnumerable<string> source) =>
            source.SortBy(s => s, new NumberOfEntriesComparer('2'));

        [TestCase(arg: new int[] { 52, 22, 22222, 72322 }, ExpectedResult = new int[] { 22222, 72322, 22, 52 })]
        public IEnumerable<int> SortByDescending_IntegerArrayByStringRepresentationUsingNumberOfEntriesComparer(IEnumerable<int> source) =>
            source.SortByDescending(x => x.ToString(), new NumberOfEntriesComparer('2'));

        #endregion
    }
}