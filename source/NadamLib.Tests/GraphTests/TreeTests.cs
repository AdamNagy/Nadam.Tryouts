using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nadam.Global.Lib.Tree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NadamLib.Tests
{
    class TreeTests
    {
        [TestClass]
        public class Add
        {
            [TestMethod]
            public void AddingRootToTree()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                Assert.AreEqual(1, tree.NodesCount());
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void AddingSecondRootMustThrowException()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddRoot(1);
            }

            [TestMethod]
            public void GivenATreeAdding3Leafs()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 11);
                tree.AddChildFor(1, 12);
                tree.AddChildFor(1, 13);

                Assert.AreEqual(4, tree.NodesCount());
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void GivenABasicTreeAddingBackwardEdgeMustThrowException()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 11);
                tree.AddChildFor(1, 12);
                tree.AddChildFor(1, 13);

                tree.AddChildFor(11, 1);
            }
        }

        [TestClass]
        public class Get
        {
            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void GetRootWhenDoesNotExistMustThrowException()
            {
                ITree<string> tree = new Tree<string>();

                tree.GetRoot();
            }

            [TestMethod]
            public void GetRootMustReturn1()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                var expectedRoot = tree.GetRoot();
                Assert.AreEqual(1, expectedRoot);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void GetChildrenForThrowException()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                tree.GetChildrenFor(5);
            }

            [TestMethod]
            public void GetChildrenForReturnEmptyList()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                var children = tree.GetChildrenFor(1);

                Assert.AreEqual(0, children.Count());
            }

            [TestMethod]
            public void GetChildrenForReturn3ItemedList()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 11);
                tree.AddChildFor(1, 12);
                tree.AddChildFor(1, 13);

                var children = tree.GetChildrenFor(1);

                Assert.AreEqual(3, children.Count());
            }
        }

        [TestClass]
        public class PreOrderIterationTest
        {
            [TestMethod]
            public void TreeWithOnlyRootNode()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                var iterationOrder = new List<int>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while(treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1 }, iterationOrder);
            }

            [TestMethod]
            public void TestA()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                var iterationOrder = new List<int>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, iterationOrder);
            }

            [TestMethod]
            public void TestB()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                tree.AddChildFor(2, 5);
                tree.AddChildFor(2, 6);
                tree.AddChildFor(2, 7);

                var iterationOrder = new List<int>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 5, 6, 7, 3, 4 }, iterationOrder);
            }

            [TestMethod]
            public void TestC()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                tree.AddChildFor(2, 5);
                tree.AddChildFor(2, 6);
                tree.AddChildFor(2, 7);

                tree.AddChildFor(6, 8);
                tree.AddChildFor(6, 9);
                tree.AddChildFor(6, 10);

                tree.AddChildFor(10, 14);
                tree.AddChildFor(10, 15);
                tree.AddChildFor(10, 16);

                tree.AddChildFor(3, 11);
                tree.AddChildFor(3, 12);
                tree.AddChildFor(3, 13);

                var iterationOrder = new List<int>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 5, 6, 8, 9, 10, 14, 15, 16, 7, 3, 11, 12, 13, 4 }, iterationOrder);
            }

            /// <summary>
            /// https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Sorted_binary_tree_preorder.svg/336px-Sorted_binary_tree_preorder.svg.png
            /// </summary>
            [TestMethod]
            public void BinaryTreeExampleFromWiki()
            {
                ITree<char> tree = new Tree<char>();
                tree.AddRoot('F');
                tree.AddChildFor('F', 'B');
                tree.AddChildFor('F', 'G');
                tree.AddChildFor('B', 'A');
                tree.AddChildFor('B', 'D');
                tree.AddChildFor('D', 'C');
                tree.AddChildFor('D', 'E');
                tree.AddChildFor('G', 'I');
                tree.AddChildFor('I', 'H');


                var iterationOrder = new List<char>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new char[] { 'F', 'B', 'A', 'D', 'C', 'E', 'G', 'I', 'H' }, iterationOrder);
            }

            [TestMethod]
            public void TreeLikeASinleRow()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(2, 3);
                tree.AddChildFor(3, 4);

                var iterationOrder = new List<int>();

                var treeIterator = tree.PreOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1,2,3,4 }, iterationOrder);
            }
        }

        [TestClass]
        public class PostOrderIterationTest
        {
            /// <summary>
            /// https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Sorted_binary_tree_preorder.svg/336px-Sorted_binary_tree_preorder.svg.png
            /// </summary>
            [TestMethod]
            public void BinaryTreeExampleFromWiki()
            {
                ITree<char> tree = new Tree<char>();
                tree.AddRoot('F');
                tree.AddChildFor('F', 'B');
                tree.AddChildFor('F', 'G');
                tree.AddChildFor('B', 'A');
                tree.AddChildFor('B', 'D');
                tree.AddChildFor('D', 'C');
                tree.AddChildFor('D', 'E');
                tree.AddChildFor('G', 'I');
                tree.AddChildFor('I', 'H');


                var iterationOrder = new List<char>();

                var treeIterator = tree.PostOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new char[] { 'A', 'C', 'E', 'D', 'B', 'H', 'I', 'G', 'F' }, iterationOrder);
            }
        }

        [TestClass]
        public class LevelOrderIterationTest
        {
            [TestMethod]
            public void TreeWithOnlyRootNode()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);

                var iterationOrder = new List<int>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1 }, iterationOrder);
            }

            [TestMethod]
            public void TestA()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                var iterationOrder = new List<int>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, iterationOrder);
            }

            [TestMethod]
            public void TestB()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                tree.AddChildFor(2, 5);
                tree.AddChildFor(2, 6);
                tree.AddChildFor(2, 7);

                var iterationOrder = new List<int>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7 }, iterationOrder);
            }

            [TestMethod]
            public void TestC()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(1, 3);
                tree.AddChildFor(1, 4);

                tree.AddChildFor(2, 5);
                tree.AddChildFor(2, 6);
                tree.AddChildFor(2, 7);

                tree.AddChildFor(6, 8);
                tree.AddChildFor(6, 9);
                tree.AddChildFor(6, 10);

                tree.AddChildFor(10, 14);
                tree.AddChildFor(10, 15);
                tree.AddChildFor(10, 16);

                tree.AddChildFor(3, 11);
                tree.AddChildFor(3, 12);
                tree.AddChildFor(3, 13);

                var iterationOrder = new List<int>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 11, 12, 13, 8, 9, 10, 14, 15, 16 }, iterationOrder);
            }

            /// <summary>
            /// https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Sorted_binary_tree_preorder.svg/336px-Sorted_binary_tree_preorder.svg.png
            /// </summary>
            [TestMethod]
            public void TreeExampleFromWiki()
            {
                ITree<char> tree = new Tree<char>();
                tree.AddRoot('F');
                tree.AddChildFor('F', 'B');
                tree.AddChildFor('F', 'G');
                tree.AddChildFor('B', 'A');
                tree.AddChildFor('B', 'D');
                tree.AddChildFor('D', 'C');
                tree.AddChildFor('D', 'E');
                tree.AddChildFor('G', 'I');
                tree.AddChildFor('I', 'H');


                var iterationOrder = new List<char>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new char[] { 'F', 'B', 'G', 'A', 'D', 'I', 'C', 'E', 'H' }, iterationOrder);
            }

            [TestMethod]
            public void TreeLikeASingleRow()
            {
                ITree<int> tree = new Tree<int>();
                tree.AddRoot(1);
                tree.AddChildFor(1, 2);
                tree.AddChildFor(2, 3);
                tree.AddChildFor(3, 4);

                var iterationOrder = new List<int>();

                var treeIterator = tree.LevelOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, iterationOrder);
            }
        }
    }
}
