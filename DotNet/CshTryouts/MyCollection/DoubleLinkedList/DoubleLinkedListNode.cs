namespace MyCollection.DoubleLinkedList
{
    public class DoubleLinkedListNode<T>
    {
        public T Data { get; internal set; }

        public DoubleLinkedListNode<T> Next { get; internal set; }
        public DoubleLinkedListNode<T> Previous { get; internal set; }

        internal DoubleLinkedList<T> Owner { get; set; }

        /// <summary>
        /// Initializes a new instance of the DoubleLinkedListNode(T) class with the specified data.
        /// </summary>
        /// <param name="data">The data that this node will contain.</param>
        public DoubleLinkedListNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the DoubleLinkedListNode(T) class with the specified data and owner.
        /// </summary>
        /// <param name="data">The data that this node will contain.</param>
        internal DoubleLinkedListNode(DoubleLinkedList<T> owner, T data)
        {
            Data = data;
            Owner = owner;
        }
    }
}
