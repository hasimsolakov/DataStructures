namespace _07.DoubleLinkedQueue
{
    using System;

   public class Program
    {
        private static void Main()
        {
            LinkedQueue<int> queue = new LinkedQueue<int>();
            queue.Enqueue(9);
            queue.Enqueue(8);
            queue.Enqueue(7);
            Console.WriteLine(string.Join(", ", queue.ToArray()));
        }
    }
}
