namespace Hash_Table
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DefaultInitialCapacity = 16;
        private const float LoadFactor = 0.75f;
        private LinkedList<KeyValue<TKey, TValue>>[] slots; 

        public HashTable() : this(DefaultInitialCapacity)
        {
        }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.slots.Length;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.Select(keyValue => keyValue.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(keyValue => keyValue.Value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeed();
            int slotIndexOfTheKey = this.FindSlotNumber(key);
            if (this.slots[slotIndexOfTheKey] == null)
            {
                this.slots[slotIndexOfTheKey] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var keyValue in this.slots[slotIndexOfTheKey])
            {
                if (keyValue.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists :" + key);
                }
            }

            var newKeyValue = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotIndexOfTheKey].AddLast(newKeyValue);
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            bool addedElement = true;
            this.GrowIfNeed();
            int slotIndexOfTheKey = this.FindSlotNumber(key);
            if (this.slots[slotIndexOfTheKey] == null)
            {
                this.slots[slotIndexOfTheKey] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var keyValue in this.slots[slotIndexOfTheKey])
            {
                if (keyValue.Key.Equals(key))
                {
                    keyValue.Value = value;
                    addedElement = false;
                }
            }

            if (addedElement)
            {
                var newKeyValue = new KeyValue<TKey, TValue>(key, value);
                this.slots[slotIndexOfTheKey].AddLast(newKeyValue);
                this.Count++;
            }

            return addedElement;
        }

        public TValue Get(TKey key)
        {
            int keySlotIndex = this.FindSlotNumber(key);
            var elementsOnThisPosistion = this.slots[keySlotIndex];
            if (elementsOnThisPosistion != null)
            {
                foreach (var keyValue in elementsOnThisPosistion)
                {
                    if (keyValue.Key.Equals(key))
                    {
                        return keyValue.Value;
                    }
                }
            }

            throw new KeyNotFoundException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this.Get(key);
                return true;
            }
            catch (KeyNotFoundException)
            {
                value = default(TValue);
                return false;
            }
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int keySlotIndex = this.FindSlotNumber(key);
            var keyValuesCollection = this.slots[keySlotIndex];
            if (keyValuesCollection == null)
            {
                return null;
            }
        
            foreach (var keyValue in keyValuesCollection)
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue;
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            int keySlotIndex = this.FindSlotNumber(key);
            var keyValuesCollection = this.slots[keySlotIndex];
            if (keyValuesCollection == null)
            {
                return false;
            }

            foreach (var keyValue in keyValuesCollection)
            {
                if (keyValue.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            int keySlotIndex = this.FindSlotNumber(key);
            var keyValuesCollection = this.slots[keySlotIndex];
            if (keyValuesCollection == null)
            {
                return false;
            }

            foreach (var keyValue in keyValuesCollection)
            {
                if (keyValue.Key.Equals(key))
                {
                    keyValuesCollection.Remove(keyValue);
                    this.Count--;
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultInitialCapacity];
            this.Count = 0;
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (var keyValue in linkedList)
                    {
                        yield return keyValue;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int FindSlotNumber(TKey key)
        {
            int keySlotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
            return keySlotNumber;
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
            var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var keyValue in this)
            {
                newHashTable.Add(keyValue.Key, keyValue.Value);
            }

            this.slots = newHashTable.slots;
            this.Count = newHashTable.Count;
        }
    }
}
