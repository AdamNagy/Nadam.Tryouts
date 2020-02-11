using System.Collections.Generic;

namespace Graphs.Trees.LinkedTree
{
    public static class LinkedTreeNodeIterationExtensions
    {
        // (Root, Left, Right)
        public static IEnumerable<LinkedTreeNode<TNode>> PreOrder<TNode>(this LinkedTreeNode<TNode> currentRoot)
        {
            yield return currentRoot;
            foreach (var child in currentRoot.Children)
            {
                foreach (var grandChild in PreOrder(child))
                {
                    yield return grandChild;
                }
            }
        }

        // (Left, Right, Root)
        public static IEnumerable<LinkedTreeNode<TNode>> PostOrder<TNode>(this LinkedTreeNode<TNode> currentRoot)
        {
            var nodeQueue = BuildPostOrderStack(currentRoot);

            while (nodeQueue.Count > 0)
                yield return nodeQueue.Dequeue();
        }

        // iteratin by the levels of the tree
        public static IEnumerable<LinkedTreeNode<TNode>> BreadthFirst<TNode>(this LinkedTreeNode<TNode> currentRoot)
        {
            var dictOfLevels = new Dictionary<int, List<LinkedTreeNode<TNode>>>();
            BuildLevelDictionary(currentRoot, ref dictOfLevels, 0);

            foreach (var node in BuildQueueFromDict(dictOfLevels))
            {
                yield return node;
            }
        }

        private static Queue<LinkedTreeNode<TNode>> BuildPostOrderStack<TNode>(LinkedTreeNode<TNode> currentRoot)
        {
            var queue = new Queue<LinkedTreeNode<TNode>>();
            foreach (var node in currentRoot)
                queue.EnqueueAll(BuildPostOrderStack(node));

            queue.Enqueue(currentRoot);
            return queue;
        }

        private static Queue<LinkedTreeNode<TNode>> BuildQueueFromDict<TNode>(Dictionary<int, List<LinkedTreeNode<TNode>>> dict)
        {
            var queue = new Queue<LinkedTreeNode<TNode>>();
            foreach (var dictKey in dict.Keys)
            {
                foreach (var node in dict[dictKey])
                {
                    queue.Enqueue(node);
                }
            }

            return queue;
        }

        private static void BuildLevelDictionary<TNode>(
            LinkedTreeNode<TNode> currentRoot,
            ref Dictionary<int, List<LinkedTreeNode<TNode>>> dict,
            int currentLevel = 0)
        {
            if (!dict.ContainsKey(currentLevel))
                dict.Add(currentLevel, new List<LinkedTreeNode<TNode>>());

            dict[currentLevel].Add(currentRoot);
            foreach (var child in currentRoot.Children)
            {
                BuildLevelDictionary(child, ref dict, currentLevel + 1);
            }
        }
    }
}
