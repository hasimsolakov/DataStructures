namespace _02.CalculateSequenceWithQueue
{
    using System.Collections.Generic;
    using IO;
    using IO.Interfaces;

    public class SequenceCalculator
    {
        private static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            int n = int.Parse(reader.ReadLine());

            Queue<int> numbersCollection = new Queue<int>(102);
            numbersCollection.Enqueue(n);
            for (int index = 0; index < 50; index++)
            {
                int number = numbersCollection.Dequeue();
                numbersCollection.Enqueue(number + 1);
                numbersCollection.Enqueue((2 * number) + 1);
                numbersCollection.Enqueue(number + 2);
                if (index == 0)
                {
                    writer.Write(number.ToString());
                }
                else
                {
                    writer.Write(", " + number);
                }
            }

            writer.WriteLine(string.Empty);
        }
    }
}
