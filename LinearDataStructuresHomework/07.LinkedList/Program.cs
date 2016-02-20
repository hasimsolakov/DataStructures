namespace _07.LinkedList
{
    using System;

    public class Program
    {
        public static void Main()
        {
            LinkedList<int> linkedListOfNumbers = new LinkedList<int>();
            linkedListOfNumbers.Add(9);
            linkedListOfNumbers.Add(8);
            linkedListOfNumbers.Add(5);
            linkedListOfNumbers.Add(9);
            linkedListOfNumbers.Add(4);
            Console.WriteLine(linkedListOfNumbers.Count);
            Console.WriteLine();
            Console.WriteLine(linkedListOfNumbers.FirstIndexOf(9));
            Console.WriteLine();
            Console.WriteLine(linkedListOfNumbers.LastIndexOf(9));
            Console.WriteLine();
            foreach (var number in linkedListOfNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
