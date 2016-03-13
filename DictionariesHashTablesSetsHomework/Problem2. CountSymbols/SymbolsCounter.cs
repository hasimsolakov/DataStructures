namespace Problem2.CountSymbols
{
    using System;
    using System.Linq;
    using Problem1.Dictionary;

    public class SymbolsCounter
    {
        private static void Main()
        {
            string input = Console.ReadLine();
            CostumDictionary<char, int> dicitionary = new CostumDictionary<char, int>();
            foreach (var character in input)
            {
                if (dicitionary.ContainsKey(character))
                {
                    dicitionary[character]++;
                }
                else
                {
                    dicitionary.Add(character, 1);
                }
            }

            dicitionary
                .OrderBy(keyValuePair => keyValuePair.Key)
                .ToList()
                .ForEach(keyValue => Console.WriteLine("{0}: {1} times/s", keyValue.Key, keyValue.Value));
        }
    }
}
