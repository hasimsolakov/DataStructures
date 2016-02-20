namespace _3.LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using IO;
    using IO.Interfaces;

    public class LongestSequenceFinder
    {
        public static void FindLongestSubseqeunce(IReader reader, IWriter writer)
        {
            var inputSequenceOfNumbers = GetSequenceOfNumbers(reader);
            var longestSubsequence = GetLongestSubsequence(inputSequenceOfNumbers);

            writer.WriteLine(string.Join(" ", longestSubsequence));
        }

        private static List<int> GetLongestSubsequence(List<int> inputSequenceOfNumbers)
        {
            int counter = 1;
            int currentMaxCounts = counter;
            int longestSubsequenceStartIndex = 0;
            for (int i = 1; i < inputSequenceOfNumbers.Count; i++)
            {
                if (inputSequenceOfNumbers[i - 1] == inputSequenceOfNumbers[i])
                {
                    counter++;
                }
                else
                {
                    if (currentMaxCounts < counter)
                    {
                        currentMaxCounts = counter;
                        longestSubsequenceStartIndex = i - counter;
                    }

                    counter = 1;
                }
            }

            if (currentMaxCounts < counter)
            {
                currentMaxCounts = counter;
                longestSubsequenceStartIndex = inputSequenceOfNumbers.Count - counter;
            }

            List<int> longestSubsequence = new List<int>(currentMaxCounts);
            longestSubsequence
                .AddRange(inputSequenceOfNumbers
                    .GetRange(longestSubsequenceStartIndex, currentMaxCounts));
            return longestSubsequence;
        }

        private static List<int> GetSequenceOfNumbers(IReader reader)
        {
            List<int> inputSequenceOfNumbers = reader
                .ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (inputSequenceOfNumbers.Count == 0)
            {
                throw new ArgumentException("Sequence must not be empty");
            }

            return inputSequenceOfNumbers;
        }
    }
}
