namespace _05.CountOfOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using IO.Interfaces;

    public class Program
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            List<int> inputSequence = GetInputSequence(reader);
            var uniqueNumbersCollection = inputSequence.Distinct().ToList();
            uniqueNumbersCollection.Sort();

            foreach (var uniqueNumber in uniqueNumbersCollection)
            {
                int occurencesCount = inputSequence
                    .FindAll(number => number == uniqueNumber)
                    .Count;
                writer.WriteLine(string.Format("{0} -> {1} times", uniqueNumber, occurencesCount));
            }
        }

        private static List<int> GetInputSequence(IReader reader)
        {
            List<int> inputSequence = reader
                .ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            if (inputSequence.Count == 0)
            {
                throw new ArgumentException("Input sequence cannot be empty");
            }

            return inputSequence;
        } 
    }
}
