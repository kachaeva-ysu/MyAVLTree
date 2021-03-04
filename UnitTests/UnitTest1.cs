using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAVLTree;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CountIsZeroAfterCreating()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void CountIncreasesAfterAdding()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int i = 1; i <= n; i++)
            {
                tree.Add(i, "a");
                Assert.AreEqual(i, tree.Count);
            }
        }

        [TestMethod]
        public void CountDecreasesAfterRemoving()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int i = 1; i <= n; i++)
            {
                tree.Add(i, "a");
            }
            tree.Remove(50);
            Assert.AreEqual(99, tree.Count);
        }

        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            int i = 1;
            foreach (var a in tree)
            {
                Assert.AreEqual(i, a.Key);
                i++;
            }
        }

        [TestMethod]
        public void ItemsDoNotExistAfterRemoving()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            tree.Remove(50);
            int i = 1;
            foreach (var a in tree)
            {
                if (i < 50)
                    Assert.AreEqual(i, a.Key);
                else
                    Assert.AreEqual(i + 1, a.Key);
                i++;
            }
        }

        [TestMethod]
        public void RemoveReturnsFalseIfItemDoesNotExists()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            Assert.AreEqual(false, tree.Remove(5));
        }

        [TestMethod]
        public void ContainsWorksCorrectlyIfItemExists()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            Assert.AreEqual(true, tree.ContainsKey(45));
        }

        [TestMethod]
        public void ContainsWorksCorrectlyIfItemDoesNotExist()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            Assert.AreEqual(false, tree.ContainsKey(145));
        }

        [TestMethod]
        public void IndexatorReturnsCorrectValue()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            Assert.AreEqual("a", tree[45]);
        }

        [TestMethod]
        public void IndexatorSetsValueCorrectly()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            tree[45] = "b";
            Assert.AreEqual("b", tree[45]);
        }

        [TestMethod]
        public void EnumerationWorksCorrectly()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            int i = 1;
            foreach (var item in tree)
            {
                Assert.AreEqual(i, item.Key);
                i++;
            }
        }

        [TestMethod]
        public void AllTurnsWorkCorrectly()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int[] array = new int[] { 7, 13, 27, 3, 5, 1, 4, 8, 17, 22, 14, 41, 25 };
            foreach (var a in array)
            {
                tree.Add(a, "a");
            }
            Array.Sort(array);
            int i = 0;
            foreach(var pair in tree)
            {
                Assert.AreEqual(array[i], pair.Key);
                i++;
            }
            tree.Remove(27);
            tree.Remove(7);
            tree.Remove(13);
            tree.Remove(8);
            int[] array2 = new int[] { 3, 5, 1, 4, 17, 22, 14, 41, 25 };
            Array.Sort(array2);
            i = 0;
            foreach (var pair in tree)
            {
                Assert.AreEqual(array2[i], pair.Key);
                i++;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanNotGetWrongIndex()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            var a = tree[500];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanNotSetToWrongIndex()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            int n = 100;
            for (int j = 1; j <= n; j++)
            {
                tree.Add(j, "a");
            }
            tree[500] = "a";
        }
    }
}