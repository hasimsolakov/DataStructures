namespace Double_Linked_List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> firstNode;
        private ListNode<T> lastNode;

        public DoublyLinkedList()
        {
            this.firstNode = null;
            this.lastNode = null;
            this.Count = 0;
        }

        private class ListNode<TK>
        {
            private TK value;
            private ListNode<TK> previousNode;
            private ListNode<TK> nextNode;

            public ListNode(TK value, ListNode<TK> previousNode, ListNode<TK> nextNode)
            {
                this.Value = value;
                this.PreviousNode = previousNode;
                this.NextNode = nextNode;
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

            public ListNode<TK> PreviousNode
            {
                get
                {
                    return this.previousNode;               
                }

                set
                {
                    this.previousNode = value;             
                }
            }

            public ListNode<TK> NextNode
            {
                get
                {
                    return this.nextNode;               
                }

                set
                {
                    this.nextNode = value;               
                }
            }
        }

        public int Count { get; private set; }

        public void AddFirst(T element)
        {
            if (this.firstNode == null)
            {
                this.firstNode = new ListNode<T>(element, null, null);
                if (this.lastNode != null)
                {
                    this.firstNode.NextNode = this.lastNode;
                    this.lastNode.PreviousNode = this.firstNode;
                }

                this.Count++;
                return;
            }

            if (this.lastNode == null)
            {
                ListNode<T> firstNode = this.firstNode;
                this.lastNode = firstNode;
                this.firstNode = new ListNode<T>(element, null, this.lastNode);
                this.firstNode.NextNode = this.lastNode;
                this.lastNode.PreviousNode = this.firstNode;
                this.Count++;
                return;
            }

            ListNode<T> currentFirstNode = this.firstNode;
            this.firstNode = new ListNode<T>(element, null, currentFirstNode);
            currentFirstNode.PreviousNode = this.firstNode;
            this.Count++;
        }

        public void AddLast(T element)
        {
            if (this.firstNode == null)
            {
                this.firstNode = new ListNode<T>(element, null, null);
                this.Count++;
                return;
            }

            ListNode<T> currentLastNode = this.lastNode;
            if (currentLastNode == null)
            {
                this.lastNode = new ListNode<T>(element, this.firstNode, null);
                this.firstNode.NextNode = this.lastNode;
                this.Count++;
                return;
            }

            this.lastNode = new ListNode<T>(element, this.lastNode, null);
            currentLastNode.NextNode = this.lastNode;
            this.Count++;
        }

        public T RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot remove element from empty collection");
            }

            ListNode<T> currentFirstNode = this.firstNode;
            var valueToReturn = currentFirstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            if (this.firstNode != null)
            {
                this.firstNode.PreviousNode = null;
            }

            currentFirstNode = null;
            this.Count --;
            return valueToReturn;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot remove element from empty collection");
            }

            if (this.Count == 1)
            {
                T elementToReturn = default(T);
                if (this.firstNode == null && this.lastNode != null)
                {
                    elementToReturn = this.lastNode.Value;
                }
                else if (this.firstNode != null && this.lastNode == null)
                {
                    elementToReturn = this.firstNode.Value;
                }
                else
                {
                    elementToReturn = this.firstNode.Value;
                }

                this.firstNode = null;
                this.lastNode = null;
                this.Count--;
                return elementToReturn;
            }

            ListNode<T> currentLastNode = this.lastNode;
            T valueOfTheLastNode;
            if (currentLastNode == null)
            {
                currentLastNode = this.firstNode;
                valueOfTheLastNode = currentLastNode.Value;
                this.firstNode = null;
                this.Count--;
                return valueOfTheLastNode;
            }

            valueOfTheLastNode = currentLastNode.Value;
            ListNode<T> nodeBeforeLastNode = this.lastNode.PreviousNode;
            if (nodeBeforeLastNode != null)
            {
                nodeBeforeLastNode.NextNode = null;
            }

            this.lastNode = nodeBeforeLastNode;
            this.Count--;
            return valueOfTheLastNode;
        }

        public void ForEach(Action<T> action)
        {
            ListNode<T> currentNode = this.firstNode;
            if (currentNode == null)
            {
                currentNode = this.lastNode;
                if (currentNode == null)
                {
                    return;
                }
            }

            while (currentNode.NextNode != null)
            {
                action.Invoke(currentNode.Value);
                currentNode = currentNode.NextNode;
            }

            action.Invoke(currentNode.Value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T> currentNode = this.firstNode;
            while (currentNode.NextNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            yield return currentNode.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            T [] listAsArray = new T[this.Count];
            ListNode<T> currentNode = this.firstNode;
            for (int index = 0; index < this.Count; index++)
            {
                listAsArray[index] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return listAsArray;
        }
    }


    class Example
    {
        static void Main()
        {
            var list = new DoublyLinkedList<int>();

            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------");

            list.AddLast(5);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddLast(10);
            Console.WriteLine("Count = {0}", list.Count);

            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------");

            list.RemoveFirst();
            list.RemoveLast();
            list.RemoveFirst();

            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------");

            list.RemoveLast();

            list.ForEach(Console.WriteLine);
            Console.WriteLine("--------------------");
        }
    }
}