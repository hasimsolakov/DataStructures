namespace _07.DoubleLinkedQueue
{
    using System;

    public class LinkedQueue<T>
    {
        private QueueNode<T> head;
        private QueueNode<T> tail;
        private int count;

        public LinkedQueue()
        {
            this.head = null;
            this.tail = null;
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
                    throw new ArgumentException("Count amount cannot be negative number");
                }

                this.count = value;
            }
        }

        public void Enqueue(T element)
        {
            if (this.Count == 0)
            {
                QueueNode<T> firstNode = new QueueNode<T>(element, null, null);
                this.head = firstNode;
                this.tail = firstNode;
                this.Count++;
                return;
            }

            QueueNode<T> newNode = new QueueNode<T>(element, this.tail, null);
            this.tail.NextNode = newNode;
            this.tail = newNode;
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot dequeue empty queue");
            }

            var elementToReturn = this.head.Value;
            this.head = this.head.NextNode;
            if (this.head != null)
            {
                this.head.PreviousNode = null;
            }

            this.Count--;
            return elementToReturn;
        }

        public T[] ToArray()
        {
            T[] queueAsArray = new T[this.Count];
            QueueNode<T> currentNode = this.head;
            for (int index = 0; index < this.Count; index++)
            {
                queueAsArray[index] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return queueAsArray;
        }

        private class QueueNode<TK>
        {
            private TK value;
            private QueueNode<TK> previousNode;
            private QueueNode<TK> nextNode;

            public QueueNode(TK value, QueueNode<TK> previousNode, QueueNode<TK> nextNode)
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

            public QueueNode<TK> PreviousNode
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

            public QueueNode<TK> NextNode
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
    }
}