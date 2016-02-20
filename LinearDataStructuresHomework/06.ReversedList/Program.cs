namespace _06.ReversedList
{
    using System;

   public class Program
    {
        public static void Main()
        {
            ReversedList<int> collection = new ReversedList<int>(6);
            collection.Add(6);
            collection.Add(5);
            collection.Add(4);
            Console.WriteLine(collection[0]);
            Console.WriteLine(string.Join(" ", collection));

            ReversedList<int> collection2 = new ReversedList<int>(collection);
        }
    }
}
