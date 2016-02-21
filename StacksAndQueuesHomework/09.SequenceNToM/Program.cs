namespace _09.SequenceNToM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static void Main()
        {
            Queue<Item> itemsCollection = new Queue<Item>();
            string[] input = Console.ReadLine().Split(' ');
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);
            if (n > m)
            {
                Console.WriteLine("No solution");
                return;
            }

            itemsCollection.Enqueue(new Item(n, null));
            while (itemsCollection.Count() != 0)
            {
                Item item = itemsCollection.Dequeue();
                if (item.Value < m)
                {
                    itemsCollection.Enqueue(new Item(item.Value + 1, item));
                    itemsCollection.Enqueue(new Item(item.Value + 2, item));
                    itemsCollection.Enqueue(new Item(item.Value * 2, item));
                }

                if (item.Value == m)
                {
                   PrintSolution(item);
                    return;
                }
            }
        }

        private static void PrintSolution(Item item)
        {
            Stack<int> numbersToPrint = new Stack<int>();
            while (item != null)
            {
               numbersToPrint.Push(item.Value);
                item = item.PreviousItem;
            }

            Console.WriteLine(string.Join(" -> ", numbersToPrint));
        }
    }
}
