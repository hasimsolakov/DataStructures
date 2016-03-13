namespace Problem4.OrderedSet
{
    using System;

    public class OrderedSetExamples
    {
        private static void Main()
        {
            OrderedSet<int> set = new OrderedSet<int>();
            set.Add(5);
            set.Add(2);
            set.Add(8);
            set.Add(1);
            set.Add(3);
            set.Add(7);
            set.Add(10);
            set.Add(4);
            set.Add(6);
            set.Add(9);
            set.Add(11);
            set.Add(12);
            set.Remove(7);
            Console.WriteLine(set.Contains(1));
            foreach (var num in set)
            {
                Console.WriteLine(num);
            }
        }
    }
}
