using System;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [Test]
        public void Filter_EvenNumberFilterFunkDelegateTest()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { 2, -10, 22 };

            actualArray = actualArray.Filter(this.IsEven);

            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [TestCase(new int[] { 2, 4, 6, 8, -10 }, ExpectedResult = true)]
        [TestCase(new int[] { 2 }, ExpectedResult = true)]
        [TestCase(new int[] { }, ExpectedResult = true)]
        [TestCase(new int[] { 3, 4, 6 }, ExpectedResult = false)]
        [TestCase(new int[] { 3, 5, -3 }, ExpectedResult = false)]
        public bool ForAll_EvenNumber(int[] source) => source.ForAll(this.IsEven);


        public bool IsEven(int item)
        {
            if (item % 2 == 0)
            {
                return true;
            }

            return false;
        }
    }
}