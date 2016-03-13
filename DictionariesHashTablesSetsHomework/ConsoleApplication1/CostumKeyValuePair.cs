namespace Problem1.Dictionary
{
    public class CostumKeyValuePair<TKey, TValue>
    {
        public CostumKeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; }

        public TValue Value { get; set; }

        public override string ToString()
        {
            return "[" + this.Key.ToString() + ", " + this.Value.ToString() + "]";
        }
    }
}