using Nadam.Global.Lib.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Global.Lib.Tree
{
    public class TreeNode<T> : Node<T>
    {
        private List<TreeNode<T>> children { get; set; }

        public TreeNode(T value, int id) : base(value, id)
        {            
        }

        public void AddChild(T newChild, int id)
        {
            var newChildTreeNode = new TreeNode<T>(newChild, id);
            children.Add(newChildTreeNode);
        }

        public void RemoveChild(T child)
        {
            var childTreeNode = children.FirstOrDefault(p => p.Value.Equals(child));
            if (childTreeNode != null)
                childTreeNode.RemoveChildren();
        }

        public void RemoveChildren()
        {
            children.RemoveAll(p => true);
        }
    }
}
