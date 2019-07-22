using DirectedGraph;

namespace BinaryTree
{
    public class BinaryNode<T> : DirectedNode<T>
    {
        public BinaryNode(T value, int id) : base(value, id, 2)
        {
            References.Add(-1);
            References.Add(-1);
        }

        public bool AddLeftChild(int left)
        {
            if( References[0] == -1 )
            {
                AddReference(left, 0);
                return true;
            }
            return false;            
        }

        public bool AddRightChild(int right)
        {
            if (References[1] == -1)
            {
                AddReference(right, 1);
                return true;
            }
            return false;
        }

        private bool AddReference(int id, int idx)
        {
            if (!HasReferenceFor(id))
            {
                References[idx] = id;
                return true;
            }
            return false;
        }

        public int LeftChild => References[0];        

        public int RightChild => References[1];
    }
}
