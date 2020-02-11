using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs.Trees;
using Graphs.Trees.LinkedTree;

namespace Graphs.Tests
{
    [TestClass]
    class MediatorTreeTests
    {
        
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

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, iterationOrder);
            }
        }

      
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
    
    class LinkedTreeTests
    {
        [TestClass]
        public class DefaultTest
        {
            [TestMethod]
            public void AddingElementsAndCount()
            {
                var root = new LinkedTreeNode<int>(1);
                root.Add(2);
                root.Add(2);
                root.Add(3);

                Assert.IsNotNull(root);
                Assert.AreEqual(3, root.Count());
            }

            [TestMethod]
            public void Enumeration_EmptyNode()
            {
                var root = new LinkedTreeNode<int>(1);

                var iterations = 0;
                var stringBuilder = new StringBuilder();
                foreach (var node in root)
                {
                    stringBuilder.Append($"{node.Value},");
                    ++iterations;
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("", result);
                Assert.AreEqual(0, iterations);
            }

            [TestMethod]
            public void Enumeration_OneChild()
            {
                var root = new LinkedTreeNode<int>(1);
                root.Add(1);

                var iterations = 0;
                var stringBuilder = new StringBuilder();
                foreach (var node in root)
                {
                    stringBuilder.Append($"{node.Value},");
                    ++iterations;
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("1,", result);
                Assert.AreEqual(1, iterations);
            }

            [TestMethod]
            public void Enumeration_ThreeChild()
            {
                var root = new LinkedTreeNode<int>(1);
                root.Add(1);
                root.Add(2);
                root.Add(3);

                var iterations = 0;
                var stringBuilder = new StringBuilder();
                foreach (var node in root)
                {
                    stringBuilder.Append($"{node.Value},");
                    ++iterations;
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("1,2,3,", result);
                Assert.AreEqual(3, iterations);
            }

            [TestMethod]
            public void Indexing()
            {
                var root = new LinkedTreeNode<int>(1);
                root.Add(1);
                root.Add(2);
                root.Add(3);

                Assert.AreEqual(2, root[1].Value);
            }

            [TestMethod]
            public void Indexing_Child()
            {
                var root = new LinkedTreeNode<int>(1);
                var child = root.Add(1);
                child.Add(11);
                child.Add(12);

                Assert.AreEqual(11, child[0].Value);
            }

            [TestMethod]
            public void FindFirstLevel()
            {
                var root = new LinkedTreeNode<int>(1);
                root.Add(1);
                root.Add(2);
                root.Add(3);

                var found = root.Find(3);
                Assert.IsNotNull(found);
                Assert.AreEqual(3, found.Value);
            }

            [TestMethod]
            public void FindSecondLevel()
            {
                var root = new LinkedTreeNode<int>(1);
                var child1 = root.Add(1);
                child1.Add(11);
                child1.Add(12);
                child1.Add(13);

                var child2 = root.Add(2);
                child2.Add(21);
                child2.Add(22);
                child2.Add(23);

                var child3 = root.Add(1);
                child3.Add(31);
                child3.Add(32);
                child3.Add(33);

                var found = root.Find(22);
                Assert.AreEqual(22, found.Value);
            }

            [TestMethod]
            public void Preorder()
            {
                var root = new LinkedTreeNode<int>(1);
                var child1 = root.Add(2);
                child1.Add(4);
                child1.Add(5);
                // child1.Add(13);

                var child2 = root.Add(3);
                //child2.Add(21);
                //child2.Add(22);
                //child2.Add(23);

                //var child3 = root.Add(3);
                //child3.Add(31);
                //child3.Add(32);
                //child3.Add(33);

                var stringBuilder = new StringBuilder();
                foreach (var node in root.PreOrder())
                {
                    stringBuilder.Append($"{node.Value},");
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("1,2,4,5,3,", result);
            }

            [TestMethod]
            public void Postorder()
            {
                var root = new LinkedTreeNode<int>(1);
                var child1 = root.Add(2);
                child1.Add(4);
                child1.Add(5);
                // child1.Add(13);

                var child2 = root.Add(3);
                //child2.Add(21);
                //child2.Add(22);
                //child2.Add(23);

                //var child3 = root.Add(3);
                //child3.Add(31);
                //child3.Add(32);
                //child3.Add(33);

                var stringBuilder = new StringBuilder();
                foreach (var node in root.PostOrder())
                {
                    stringBuilder.Append($"{node.Value},");
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("4,5,2,3,1,", result);
            }

            [TestMethod]
            public void BreadthFirstTest()
            {
                var root = new LinkedTreeNode<int>(1);
                var child1 = root.Add(2);
                child1.Add(4);
                child1.Add(5);
                root.Add(3);

                var stringBuilder = new StringBuilder();
                foreach (var node in root.BreadthFirst())
                {
                    stringBuilder.Append($"{node.Value},");
                }

                var result = stringBuilder.ToString();
                Assert.AreEqual("1,2,3,4,5,", result);
            }
        }
    }
}