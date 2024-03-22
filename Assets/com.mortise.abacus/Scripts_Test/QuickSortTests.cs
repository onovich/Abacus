using System;
using System.Collections.Generic;
using NUnit.Framework;
using MortiseFrame.Abacus;

namespace MortiseFrame.Abacus.Tests {

    [TestFixture]
    public class QuickSortTests {
        [Test]
        public void QuickSortList_EmptyList_NoChange() {
            var list = new List<int>();
            QuickSortFunction.QuickSortList(list);
            Assert.IsEmpty(list);
        }

        [Test]
        public void QuickSortList_SingleElement_NoChange() {
            var list = new List<int> { 1 };
            QuickSortFunction.QuickSortList(list);
            Assert.AreEqual(new List<int> { 1 }, list);
        }

        [Test]
        public void QuickSortList_MultipleElementsAscending_CorrectlySorted() {
            var list = new List<int> { 3, 1, 4, 1, 5 };
            QuickSortFunction.QuickSortList(list);
            Assert.AreEqual(new List<int> { 1, 1, 3, 4, 5 }, list);
        }

        [Test]
        public void QuickSortList_MultipleElementsDescending_CorrectlySorted() {
            var list = new List<int> { 3, 1, 4, 1, 5 };
            QuickSortFunction.QuickSortList(list, false);
            Assert.AreEqual(new List<int> { 5, 4, 3, 1, 1 }, list);
        }

        [Test]
        public void QuickSortArray_EmptyArray_NoChange() {
            var array = new int[] { };
            QuickSortFunction.QuickSortArray(array);
            Assert.IsEmpty(array);
        }

        [Test]
        public void QuickSortArray_SingleElement_NoChange() {
            var array = new int[] { 1 };
            QuickSortFunction.QuickSortArray(array);
            Assert.AreEqual(new int[] { 1 }, array);
        }

        [Test]
        public void QuickSortArray_MultipleElementsAscending_CorrectlySorted() {
            var array = new int[] { 3, 1, 4, 1, 5 };
            QuickSortFunction.QuickSortArray(array);
            Assert.AreEqual(new int[] { 1, 1, 3, 4, 5 }, array);
        }

        [Test]
        public void QuickSortArray_MultipleElementsDescending_CorrectlySorted() {
            var array = new int[] { 3, 1, 4, 1, 5 };
            QuickSortFunction.QuickSortArray(array, false);
            Assert.AreEqual(new int[] { 5, 4, 3, 1, 1 }, array);
        }
    }
}
