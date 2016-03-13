namespace Problem3.Phonebook
{
    using System;
    using System.Collections.Generic;
    using Problem1.Dictionary;

    public class Phonebook
    {
        private static void Main()
        {
            string inputLine = Console.ReadLine();
            string[] inputs = inputLine.Split('-');
            CostumDictionary<string, string> phonebook = new CostumDictionary<string, string>();
            while (inputs[0] != "search")
            {
                phonebook.Add(inputs[0], inputs[1]);
                inputLine = Console.ReadLine();
                inputs = inputLine.Split('-');
            }

            string toSearch = Console.ReadLine();
            while (toSearch != string.Empty)
            {
                try
                {
                    var result = phonebook.Find(toSearch);
                    Console.WriteLine("{0} -> {1}", result.Key, result.Value);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Contact {0} does not exist.", toSearch);
                }

                toSearch = Console.ReadLine();
            }
        }
    }
}
