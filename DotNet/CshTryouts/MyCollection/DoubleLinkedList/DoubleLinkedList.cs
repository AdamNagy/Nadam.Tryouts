using System;
using System.Collections.Generic;

namespace MyCollection.DoubleLinkedList
{
    public class DoubleLinkedList<T>
    {
        public DoubleLinkedListNode<T> Head { get; private set; }
        public DoubleLinkedListNode<T> Tail { get; private set; }

        private int m_updateCode;

        public DoubleLinkedList()
        {
        }
        public DoubleLinkedList(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                AddToEnd(item);
            }
        }

        public bool IsEmpty
        {
            get { return Count <= 0; }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the DoubleLinkedList(T).
        /// </summary>
        public int Count { get; private set; }
        
        /// <summary>
        /// Checks if the specified data is present in the DoubleLinkedList(T).
        /// </summary>
        /// <param name="data">The data to look for.</param>
        /// <returns>True if the data is found, false otherwise.</returns>
        public bool Contains(T data)
        {
            return Find(data) != null;
        }
        
        /// <summary>
        /// Removes all items from the DoubleLinkedList(T).
        /// </summary>
        public void Clear()
        {
            DoubleLinkedListNode<T> tmp;

            // Clean up the items in the list
            for (DoubleLinkedListNode<T> node = Head; node != null;)
            {
                tmp = node.Next;

                // Change the count and head pointer in case we throw an exception.
                // this way the node is removed before we clear the data
                Head = tmp;
                if (tmp != null)
                {
                    tmp.Previous = null;
                }
                --Count;

                // Erase the contents of the node
                node.Next = null;
                node.Previous = null;
                node.Owner = null;

                // Move to the next node
                node = tmp;
            }

            if (Count <= 0)
            {
                Head = null;
                Tail = null;
            }

            ++m_updateCode;
        }
        
        /// <summary>
        /// Adds the specified value to the DoubleLinkedList(T) after the specified node.
        /// </summary>
        /// <param name="node">The node to add the value after.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The newly created node that holds the value.</returns>
        public DoubleLinkedListNode<T> AddAfter(DoubleLinkedListNode<T> node, T value)
        {
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);
            AddAfter(node, newNode);
            return newNode;
        }

        /// <summary>
        /// Adds the specified newNode to the DoubleLinkedList(T) after the specified node.
        /// </summary>
        /// <param name="node">The node to add the newNode after.</param>
        /// <param name="newNode">The node to add.</param>
        public void AddAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            
            if (node.Owner != this)
                throw new InvalidOperationException("node is not owned by this list");
            
            if (newNode.Owner != this)
                throw new InvalidOperationException("newNode is not owned by this list");

            if (node == Tail)
                Tail = newNode;

            if (node.Next != null)
                node.Next.Previous = newNode;

            newNode.Next = node.Next;
            newNode.Previous = node;

            node.Next = newNode;

            ++Count;
            ++m_updateCode;
        }

        /// <summary>
        /// Adds the specified value to the DoubleLinkedList(T) before the specified node.
        /// </summary>
        /// <param name="node">The node to add the value before.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The newly created node that holds the value.</returns>
        public DoubleLinkedListNode<T> AddBefore(DoubleLinkedListNode<T> node, T value)
        {
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);
            AddBefore(node, newNode);
            return newNode;
        }

        /// <summary>
        /// Adds the specified newNode to the DoubleLinkedList(T) before the specified node.
        /// </summary>
        /// <param name="node">The node to add the newNode before.</param>
        /// <param name="newNode">The node to add.</param>
        public void AddBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            
            if (node.Owner != this)
                throw new InvalidOperationException("node is not owned by this list");
            
            if (newNode.Owner != this)
                throw new InvalidOperationException("newNode is not owned by this list");

            // We have to find the node before this one
            if (Head == node)
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }
            else
            {
                // Set the node before the node we are inserting in front of Next to the new node
                if (node.Previous != null)
                {
                    node.Previous.Next = newNode;
                }

                newNode.Previous = node.Previous;
                newNode.Next = node;

                node.Previous = newNode;
            }

            ++Count;
            ++m_updateCode;
        }

        /// <summary>
        /// Adds the value to the beginning of the DoubleLinkedList(T).
        /// </summary>
        /// <param name="value">The value to add to the beginning of the DoubleLinkedList(T).</param>
        /// <returns>The newly created node that is holding the value.</returns>
        public DoubleLinkedListNode<T> AddToBeginning(T value)
        {
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

            if (IsEmpty)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Previous = newNode;
                Head = newNode;
            }

            ++Count;
            ++m_updateCode;

            return newNode;
        }

        /// <summary>
        /// Adds the value to the end of the DoubleLinkedList(T).
        /// </summary>
        /// <param name="value">The value to add to the end of the DoubleLinkedList(T).</param>
        /// <returns>The newly created node that is holding the value.</returns>
        public DoubleLinkedListNode<T> AddToEnd(T value)
        {
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

            if (IsEmpty)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }

            ++Count;
            ++m_updateCode;

            return newNode;
        }

        /// <summary>
        /// Locates the first node that contains the specified data.
        /// </summary>
        /// <param name="data">The data to find.</param>
        /// <returns>The node that contains the specified data, null otherwise.</returns>
        public DoubleLinkedListNode<T> Find(T data)
        {
            if (IsEmpty)
                return null;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Traverse the list from Head to tail
            for (var curr = Head; curr != null; curr = curr.Next)
            {
                // Return the node we are currently on if it contains the data we are looking for.
                if (comparer.Equals(curr.Data, data))
                    return curr;
            }

            return null;
        }

        /// <summary>
        /// Removes the first occurrence of the specified item from the DoubleLinkedList(T).
        /// </summary>
        /// <param name="item">The item to remove from the DoubleLinkedList(T).</param>
        /// <returns>True if an item was removed, false otherwise.</returns>
        public bool Remove(T item)
        {
            return Remove(item, false);
        }

        /// <summary>
        /// Removes the first or all occurrences of the specified item from the DoubleLinkedList(T).
        /// </summary>
        /// <param name="item">The item to remove from the DoubleLinkedList(T).</param>
        /// <param name="alloccurrences">True if all nodes should be removed that contain the specified item, False otherwise</param>
        /// <returns>True if an item was removed, false otherwise.</returns>
        public bool Remove(T item, bool alloccurrences)
        {
            if (IsEmpty)
                return false;

            var comparer = EqualityComparer<T>.Default;
            var removed = false;
            var curr = Head;

            while (curr != null)
            {
                // Check to see if the current node contains the data we are trying to delete
                if (!comparer.Equals(curr.Data, item))
                {
                    // Assign the current node to the previous node and the previous node to the current node
                    curr = curr.Next;
                    continue;
                }

                // Create a pointer to the next node in the previous node
                if (curr.Previous != null)
                    curr.Previous.Next = curr.Next;

                // Create a pointer to the previous node in the next node (link the next node with prvious one)
                if (curr.Next != null)
                    curr.Next.Previous = curr.Previous;

                // If the current node is the head we will have to assign the next node as the head
                if (curr == Head)
                    Head = curr.Next;

                // If the current node is the tail we will have to assign the previous node as the tail
                if (curr == Tail)
                    Tail = curr.Previous;

                // Save the pointer for clean up later
                var tmp = curr;

                // Advance the current to the next node
                curr = curr.Next;

                // Since the node will no longer be used clean up the pointers in it
                tmp.Next = null;
                tmp.Previous = null;
                tmp.Owner = null;

                // Decrement the counter since we have removed a node
                --Count;
                removed = true;

                if (!alloccurrences)
                    break;
            }

            if (removed)
            {
                ++m_updateCode;
            }

            return removed;
        }

        /// <summary>
        /// Removes the specified node from the DoubleLinkedList(T).
        /// </summary>
        /// <param name="node">The node to remove from the DoubleLinkedList(T).</param>
        public void Remove(DoubleLinkedListNode<T> node)
        {
            if (IsEmpty)
                return;

            if (node == null)
                throw new ArgumentNullException("node");

            if (node.Owner != this)
                throw new InvalidOperationException("The node doesn't belong to this list.");

            var prev = node.Previous;
            var next = node.Next;

            // Assign the head to the next node if the specified node is the head
            if (Head == node)
            {
                Head = next;
            }

            // Assign the tail to the previous node if the specified node is the tail
            if (Tail == node)
            {
                Tail = prev;
            }

            // Set the previous node next reference to the removed nodes next reference.
            if (prev != null)
            {
                prev.Next = next;
            }

            // Set the next node prev reference to the removed nodes prev reference.
            if (next != null)
            {
                next.Previous = prev;
            }

            // Null out the removed nodes next and prev pointer to be safe.
            node.Previous = null;
            node.Next = null;
            node.Owner = null;

            --Count;
            ++m_updateCode;
        }

        /// <summary>
        /// Copies the elements of the DoubleLinkedList(T) to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the DoubleLinkedList(T).</returns>
        public T[] ToArray()
        {
            var retval = new T[Count];

            var index = 0;
            for (var i = Head; i != null; i = i.Next)
            {
                retval[index] = i.Data;
                ++index;
            }

            return retval;
        }

        /// <summary>
        /// Copies the elements of the DoubleLinkedList(T) from back to front to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the DoubleLinkedList<T>.</returns>
        public T[] ToArrayReversed()
        {
            var retval = new T[Count];

            var index = 0;
            for (var i = Tail; i != null; i = i.Previous)
            {
                retval[index] = i.Data;
                ++index;
            }

            return retval;
        }
    }
}
