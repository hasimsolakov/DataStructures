namespace _07.LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> firstElement;
        private int count;

        public LinkedList()
        {
            this.Count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;    
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Count cannot be negative number");
                }

                this.count = value;              
            }
        }

        public void Add(T item)
        {
            if (this.firstElement == null)
            {
                this.firstElement = new ListNode<T>(item, null);
            }
            else
            {
                ListNode<T> lastNonEmptyNode = this.firstElement;
                while (lastNonEmptyNode.NextElement != null)
                {
                    lastNonEmptyNode = lastNonEmptyNode.NextElement;
                }

                lastNonEmptyNode.NextElement = new ListNode<T>(item, null);
            }

            this.Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                this.firstElement = this.firstElement.NextElement;
                return;
            }

            ListNode<T> previousNodeOfNodeToRemove = this.firstElement;
            for (int i = 0; i < index - 1; i++)
            {
                previousNodeOfNodeToRemove = previousNodeOfNodeToRemove.NextElement;
            }

            ListNode<T> nodeToRemove = previousNodeOfNodeToRemove.NextElement;
            var nodeAfterNodeToRemove = nodeToRemove.NextElement;
            if (nodeAfterNodeToRemove == null)
            {
                previousNodeOfNodeToRemove.NextElement = null;
            }
            else
            {
                previousNodeOfNodeToRemove.NextElement = nodeAfterNodeToRemove;
            }

            this.Count--;
        }

        /// <summary>
        /// Gets index of the first occurence of the specified item
        /// </summary>
        /// <param name="item">Item to search for</param>
        /// <returns>Returns the index of the first occurence of the specified item if it is not found returns -1</returns>
        public int FirstIndexOf(T item)
        {
            ListNode<T> node = this.firstElement;
            for (int i = 0; i < this.Count; i++)
            {
                if (node.Value.Equals(item))
                {
                    return i;
                }

                node = node.NextElement;
            }

            return -1;
        }

        /// <summary>
        /// Gets index of the last occurence of the specified item
        /// </summary>
        /// <param name="item">Item to search for</param>
        /// <returns>Returns the index of the last occurence of the specified item, if it is not found returns -1</returns>
        public int LastIndexOf(T item)
        {
            ListNode<T> currentNode = this.firstElement;

            int currentFoundIndex = -1;
            for (int index = 0; index < this.Count; index++)
            {
                if (currentNode.Value.Equals(item))
                {
                    currentFoundIndex = index;
                }

                currentNode = currentNode.NextElement;
            }

            return currentFoundIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T> node = this.firstElement;
            do
            {
                yield return node.Value;
                node = node.NextElement;
            }
            while (node.NextElement != null);

            yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }

        private class ListNode<TK>
        {
            private TK value;
            private ListNode<TK> nextElement;

            public ListNode(TK value, ListNode<TK> nextElement)
            {
                this.Value = value;
                this.NextElement = nextElement;
            }

            public ListNode<TK> NextElement
            {
                get
                {
                    return this.nextElement;                   
                }

                set
                {
                    this.nextElement = value;                   
                }
            } 

            public TK Value
            {
                get
                {
                    return this.value;                    
                }

                set
                {
                    this.value = value;                  
                }
            }
        }
    }
}