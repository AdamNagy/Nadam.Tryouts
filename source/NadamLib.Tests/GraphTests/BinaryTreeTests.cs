using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Nadam.Global.Lib.BinaryTree;

namespace NadamLib.Tests.GraphTests
{
    class BinaryTreeTests
    {
        [TestClass]
        public class Add
        {
            [TestMethod]
            public void NewlyCreatedBSTNodeCountMustBe0()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();

                // Assert
                Assert.AreEqual(0, bst.NodesCount());
            }

            [TestMethod]
            public void AddingOneElementMustMakeNodeCount1()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();

                // Act
                bst.AddNode(10);

                // Assert
                Assert.AreEqual(1, bst.NodesCount());
            }

            [TestMethod]
            public void AddingOneElementAndItsIdMustBe0()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();

                // Act
                var newNode = bst.AddNode(10);

                // Assert
                Assert.AreEqual(0, newNode.NodeId);
            }

            [TestMethod]
            public void Adding3ElementMustMakeNodeCount3()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();

                // Act
                bst.AddNode(10);
                bst.AddNode(15);
                bst.AddNode(3);

                // Assert
                Assert.AreEqual(3, bst.NodesCount());
            }
            
            [TestMethod]
            public void Adding3ElementAndItsIdMustBe()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();

                // Act
                var newNodesIds = new List<int>(3);
                newNodesIds.Add(bst.AddNode(10).NodeId);
                newNodesIds.Add(bst.AddNode(12).NodeId);
                newNodesIds.Add(bst.AddNode(32).NodeId);

                // Assert
                CollectionAssert.AreEqual(new List<int> { 0, 1, 2 }, newNodesIds);
            }
        }

        [TestClass]
        public class InOrder
        {
            [TestMethod]
            public void InOrderIterationMustSortCharacter_test1()
            {
                // Arrange
                IBinaryTree<char> bst = new BinaryTree<char>();
                bst.AddNode('a');
                bst.AddNode('b');
                bst.AddNode('c');

                // Act
                var iterationOrder = new List<char>();

                var treeIterator = bst.InOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new char[] { 'a', 'b', 'c'}, iterationOrder);
            }

            [TestMethod]
            public void InOrderIterationMustSortCharacter_test2()
            {
                // Arrange
                IBinaryTree<char> bst = new BinaryTree<char>();
                bst.AddNode('b');
                bst.AddNode('a');
                bst.AddNode('c');

                // Act
                var iterationOrder = new List<char>();

                var treeIterator = bst.InOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new char[] { 'a', 'b', 'c' }, iterationOrder);
            }

            [TestMethod]
            public void InOrderIterationMustSortNumbers_test1()
            {
                // Arrange
                IBinaryTree<int> bst = new BinaryTree<int>();
                bst.AddNode(2);
                bst.AddNode(3);
                bst.AddNode(5);
                bst.AddNode(1);
                bst.AddNode(6);
                bst.AddNode(4);

                // Act
                var iterationOrder = new List<int>();

                var treeIterator = bst.InOrder();
                treeIterator.Reset();
                while (treeIterator.MoveNext())
                {
                    iterationOrder.Add(treeIterator.Current);
                }

                CollectionAssert.AreEqual(new int[] { 1,2,3,4,5,6 }, iterationOrder);
            }
        }
    }
}
