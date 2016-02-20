namespace SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using IO.Interfaces;

    internal class Program
    {
        private static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            List<int> sequenceOfNumbers = reader
                    .ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(number => int.Parse(number))
                    .ToList();

            if (sequenceOfNumbers.Count == 0)
            {
                writer.WriteLine("Sum=0; Average=0");
                return;
            }

            int sumOfSequence = sequenceOfNumbers.Sum();
            double averageOfSequence = sequenceOfNumbers.Average();

            writer.WriteLine("Sum=" + sumOfSequence + "; Average=" + averageOfSequence);
        }
    }
}