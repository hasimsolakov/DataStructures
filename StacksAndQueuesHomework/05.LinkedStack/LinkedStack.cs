namespace _05.LinkedStack
{
    using System;

    public class LinkedStack<T>
    {
        private Node<T> firstNode;
        private int count;

        public LinkedStack()
        {
            this.firstNode = null;
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

        public void Push(T item)
        {
            Node<T> newNode = new Node<T>(item, this.firstNode);
            this.firstNode = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot pop from empty stack");
            }

            Node<T> secondNode = this.firstNode.NextNode;
            var elementToReturn = this.firstNode.Value;
            this.firstNode = secondNode;
            this.Count--;
            return elementToReturn;
        }

        public T[] ToArray()
        {
            T[] stackAsArray = new T[this.Count];
            Node<T> curremtNode = this.firstNode;
            for (int index = 0; index < this.Count; index++)
            {
                stackAsArray[index] = curremtNode.Value;
                curremtNode = curremtNode.NextNode;
            }

            return stackAsArray;
        }

        private class Node<TK>
        {
            private TK value;
            private Node<TK> nextNode;

            public Node(TK value, Node<TK> nextNode)
            {
                this.Value = value;
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

            public Node<TK> NextNode
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