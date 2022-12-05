using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MortiseFrame.Abacus;

namespace MortiseFrame.Abacus.Test {
    public class SmartListTests {

        [Test]
        public void Add_ShouldAddElementToList() {
            // Arrange
            var list = new SmartList<int>(10);

            // Act
            list.Add(5);

            // Assert
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(5, list[0]);
        }

        [Test]
        public void RemoveAt_ShouldRemoveElementAtSpecifiedIndex() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act
            list.RemoveAt(1);

            // Assert
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(15, list[1]);
        }

        [Test]
        public void RemoveAll_ShouldRemoveAllMatchingElements() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);
            list.Add(20);

            // Act & Assert
            int removed = list.RemoveAll(x => x > 10);
            Assert.That(removed, Is.EqualTo(2));
            Assert.That(list.Length, Is.EqualTo(2));
            Assert.That(list[0], Is.EqualTo(5));
            Assert.That(list[1], Is.EqualTo(10));

            // 测试移除所有偶数元素
            removed = list.RemoveAll(x => x % 2 == 0);
            Assert.That(removed, Is.EqualTo(1));
            Assert.That(list.Length, Is.EqualTo(1));
            Assert.That(list[0], Is.EqualTo(5));

            // 测试移除所有元素
            removed = list.RemoveAll(x => true);
            Assert.That(removed, Is.EqualTo(1));
            Assert.That(list.Length, Is.EqualTo(0));
        }

        [Test]
        public void Sort_ShouldSortList() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(15);
            list.Add(10);
            list.Add(5);

            // Act
            list.Sort();

            // Assert
            Assert.AreEqual(3, list.Length);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(10, list[1]);
            Assert.AreEqual(15, list[2]);
        }

        [Test]
        public void Insert_ShouldInsertElementAtSpecifiedIndex() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act
            list.Insert(1, 12);

            // Assert
            Assert.AreEqual(4, list.Length);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(12, list[1]);
            Assert.AreEqual(10, list[2]);
            Assert.AreEqual(15, list[3]);
        }

        [Test]
        public void IndexOf_ShouldReturnIndexOfSpecifiedElement() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act & Assert
            Assert.AreEqual(0, list.IndexOf(5));
            Assert.AreEqual(1, list.IndexOf(10));
            Assert.AreEqual(2, list.IndexOf(15));
            Assert.AreEqual(-1, list.IndexOf(20));
        }

        [Test]
        public void Clear_ShouldRemoveAllElementsFromList() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act
            list.Clear();

            // Assert
            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public void Contains_ShouldReturnTrueIfListContainsSpecifiedElement() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act & Assert
            Assert.IsTrue(list.Contains(5));
            Assert.IsTrue(list.Contains(10));
            Assert.IsTrue(list.Contains(15));
            Assert.IsFalse(list.Contains(20));
        }

        [Test]
        public void Foreach_ShouldIterateThroughAllElementsInList() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act
            var expected = new List<int>() { 5, 10, 15 };
            var actual = new List<int>();
            list.Foreach(x => actual.Add(x));

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void MoveNext_ShouldMoveToNextElement() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);
            // Act
            list.Reset();
            Assert.IsTrue(list.MoveNext());
            Assert.AreEqual(5, list.Current);
            Assert.IsTrue(list.MoveNext());
            Assert.AreEqual(10, list.Current);
            Assert.IsTrue(list.MoveNext());
            Assert.AreEqual(15, list.Current);
            Assert.IsFalse(list.MoveNext());
            Assert.AreEqual(15, list.Current);
        }

        [Test]
        public void Reset_ShouldResetEnumerator() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act
            list.Reset();
            Assert.AreEqual(-1, list.currentIndex);

        }

        [Test]
        public void Current_ShouldReturnCurrentElement() {
            // Arrange
            var list = new SmartList<int>(10);
            list.Add(5);
            list.Add(10);
            list.Add(15);

            // Act & Assert
            list.Reset();
            Assert.AreEqual(5, list.Current);
            list.MoveNext();
            Assert.AreEqual(10, list.Current);
            list.MoveNext();
            Assert.AreEqual(15, list.Current);

        }

    }
}