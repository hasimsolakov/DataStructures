namespace _01.ReverseNumberWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using IO.Interfaces;

   public class NumberReverser
    {
        private static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            writer.WriteLine(
                string.Join(
                    " ", 
                    new Stack<int>(reader.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray())
                .ToList()));          
        }
    }
}
