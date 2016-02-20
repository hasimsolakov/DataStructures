namespace SortWords
{
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

            List<string> sequenceOfWords = reader.ReadLine().Split(' ').ToList();
            sequenceOfWords.Sort();
            writer.WriteLine(string.Join(" ", sequenceOfWords));
        }
    }
}