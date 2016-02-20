namespace _04.RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using IO.Interfaces;

    internal class Program
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            var inputNumbers = GetInputNumbers(reader);

            var toRemoveNumbersCollection = ToRemoveNumbersCollection(inputNumbers);

            foreach (int numberToRemove in toRemoveNumbersCollection)
            {
                inputNumbers.Remove(numberToRemove);
            }

            writer.WriteLine(string.Join(" ", inputNumbers));
        }

        private static List<int> ToRemoveNumbersCollection(List<int> inputNumbers)
        {
            List<int> toRemoveNumbersCollection = new List<int>();
            for (int index = 0; index < inputNumbers.Count; index++)
            {
                int countOfTheOccurences = inputNumbers
                    .FindAll(number => number == inputNumbers[index])
                    .Count;

                if (countOfTheOccurences % 2 == 1)
                {
                    toRemoveNumbersCollection.Add(inputNumbers[index]);
                }
            }

            return toRemoveNumbersCollection;
        }

        private static List<int> GetInputNumbers(IReader reader)
        {
            List<int> inputNumbers = reader
                .ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (inputNumbers.Count == 0)
            {
                throw new ArgumentException("Input sequence cannot be empty");
            }

            return inputNumbers;
        }
    }
}