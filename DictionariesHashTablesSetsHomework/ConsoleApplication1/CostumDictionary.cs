namespace Problem1.Dictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CostumDictionary<TKey, TValue> : IEnumerable<CostumKeyValuePair<TKey, TValue>>
    {
        private const int DefaultInitialCapacity = 16;
        private const float LoadFactor = 0.75f;

        public CostumDictionary(int capacity = DefaultInitialCapacity)
        {
            this.LinkedListsCollection = new LinkedList<CostumKeyValuePair<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.LinkedListsCollection.Length; }
        }

        public IEnumerable KeysCollection
        {
            get { return this.Select(keyValuePair => keyValuePair.Key); }
        }

        public IEnumerable ValuesCollection
        {
            get { return this.Select(keyValuePair => keyValuePair.Value); }
        }

        private LinkedList<CostumKeyValuePair<TKey, TValue>>[] LinkedListsCollection { get; set; }

        public TValue this[TKey key]
        {
            get
            {
                return this.GetValueByKey(key);
            }

            set
            {
                this.Find(key).Value = value;
            }
        }

        /// <exception cref="ArgumentException">Cannot add if there is an element with the same key</exception>
        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeed();
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] == null)
            {
                this.LinkedListsCollection[keySlotIndex] = new LinkedList<CostumKeyValuePair<TKey, TValue>>();
            }

            foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
            {
                if (costumKeyValuePair.Key.Equals(key))
                {
                    throw new ArgumentException("There is already an element with that key");
                }
            }

            this.LinkedListsCollection[keySlotIndex].AddLast(new CostumKeyValuePair<TKey, TValue>(key, value));
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] == null)
            {
                this.LinkedListsCollection[keySlotIndex] = new LinkedList<CostumKeyValuePair<TKey, TValue>>();
            }

            foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
            {
                if (costumKeyValuePair.Key.Equals(key))
                {
                    costumKeyValuePair.Value = value;
                    return false;
                }
            }

            this.GrowIfNeed();
            this.LinkedListsCollection[keySlotIndex].AddLast(new CostumKeyValuePair<TKey, TValue>(key, value));
            this.Count++;
            return true;
        }

        public TValue GetValueByKey(TKey key)
        {
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] != null)
            {
                foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
                {
                    if (costumKeyValuePair.Key.Equals(key))
                    {
                        return costumKeyValuePair.Value;
                    }
                }
            }

            throw new KeyNotFoundException();
        }

        public CostumKeyValuePair<TKey, TValue> Find(TKey key)
        {
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] != null)
            {
                foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
                {
                    if (costumKeyValuePair.Key.Equals(key))
                    {
                        return costumKeyValuePair;
                    }
                }
            }

            throw new KeyNotFoundException();
        }

        public bool ContainsKey(TKey key)
        {
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] != null)
            {
                foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
                {
                    if (costumKeyValuePair.Key.Equals(key))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            int keySlotIndex = this.GetKeySlotIndex(key);
            if (this.LinkedListsCollection[keySlotIndex] != null)
            {
                foreach (var costumKeyValuePair in this.LinkedListsCollection[keySlotIndex])
                {
                    if (costumKeyValuePair.Key.Equals(key))
                    {
                        this.LinkedListsCollection[keySlotIndex].Remove(costumKeyValuePair);
                        return true;
                    }
                }
            }

            return false;
        }

        public void Clear()
        {
            this.LinkedListsCollection = new LinkedList<CostumKeyValuePair<TKey, TValue>>[DefaultInitialCapacity];
            this.Count = 0;
        }

        public IEnumerator<CostumKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var costumKeyValuePairCollection in this.LinkedListsCollection)
            {
                if (costumKeyValuePairCollection != null)
                {
                    foreach (var keyValuePair in costumKeyValuePairCollection)
                    {
                        yield return keyValuePair;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetKeySlotIndex(TKey key)
        {
            int keySlotIndex = Math.Abs(key.GetHashCode()) % this.Capacity;
            return keySlotIndex;
        }

        private void GrowIfNeed()
        {
            if (((float)(this.Count + 1) / this.Capacity) > LoadFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newDictionary = new CostumDictionary<TKey, TValue>(this.Capacity * 2);

            foreach (var keyValuePair in this)
            {
                newDictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            this.LinkedListsCollection = newDictionary.LinkedListsCollection;
            this.Count = newDictionary.Count;
        }
    }
}