namespace _03.ArrayBasedStack
{
    using System;

    public class ArrayStack<T>
    {
        private const int DefaultInitialCapacity = 16;
        private int count;
        private int capacity;
        private T[] elements;

        public ArrayStack(int capacity = DefaultInitialCapacity)
        {
            this.elements = new T[capacity];
            this.capacity = capacity;
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
            if (this.Count == this.capacity)
            {
                this.Grow();
            }

            this.elements[this.Count] = item;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot pop from empty stack");
            }

            this.Count--;
            return this.elements[this.Count];
        }

        public T[] ToArray()
        {
            T[] stackAsArray = new T[this.Count];
            Array.Copy(this.elements, stackAsArray, this.Count);
            Array.Reverse(stackAsArray);
            return stackAsArray;
        }

        private void Grow()
        {
            this.capacity *= 2;
            T [] newArray = new T[this.capacity];
            Array.Copy(this.elements, newArray,this.Count);
            this.elements = newArray;
        }
    }
}