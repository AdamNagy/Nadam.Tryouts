using Graph;
using System.Collections.Generic;

namespace DirectedGraph
{
    public class DirectedNode<T> : Node<T>
    {
        protected IList<int> References;

        public DirectedNode(T value, int id) : base(value, id)
        {
            References = new List<int>();
        }

        public DirectedNode(T value, int id, int size) : base(value, id)
        {
            References = new List<int>(size);
        }

        public IList<int> GetReferences() => References;        

        public bool AddReference(int id)
        {
            if(!HasReferenceFor(id))
            {
                References.Add(id);
                return true;
            }
            return false;
        }

        public bool RemoveReference(int id)
        {
            if (HasReferenceFor(id))
            {
                References.Remove(id);
                return true;
            }
            return false;            
        }

        public void RemoveReferences()
        {
            References = new List<int>();
        }

        public bool HasReferenceFor(int id) => References.Contains(id);        
    }
}
