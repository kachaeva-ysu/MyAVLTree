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
        public void RotationRightWorksCorrectly()
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();
            tree.Add(3, "a");
            tree.Add(1, "a");
            tree.Add(2, "a");
            int i = 1;
            foreach (var a in tree)
            {
                Assert.AreEqual(i, a.Key);
                i++;
            }
        }
    }
}