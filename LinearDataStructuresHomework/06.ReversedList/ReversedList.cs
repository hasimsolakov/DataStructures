namespace _06.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int DefaultStartCapacityAmount = 4;
        private List<T> elementsCollection;

        public ReversedList(int capacity = DefaultStartCapacityAmount)
            : this(new List<T>(capacity))
        {
        }

        public ReversedList(IEnumerable<T> collection)
        {
            this.ElementsCollection = new List<T>(collection);
        }

        public int Capacity
        {
            get
            {
                return this.ElementsCollection.Capacity;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity cannot be zero or negative number");
                }

                this.ElementsCollection.Capacity = value;
            }
        }

        public int Count
        {
            get
            {
                return this.ElementsCollection.Count;             
            }
        }

        private List<T> ElementsCollection
        {
            get
            {
                return this.elementsCollection;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.elementsCollection = value;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return this.ElementsCollection[this.Count - index - 1];
            }

            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                this.ElementsCollection[this.Count - index - 1] = value;
            }
        }

        public void Add(T item)
        {
            this.ElementsCollection.Add(item);
        }

        public void Remove(T item)
        {
            this.ElementsCollection.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = this.ElementsCollection.Count - 1; index >= 0; index--)
            {
                yield return this.ElementsCollection[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}