namespace Problem1.Dictionary
{
    using System;

    public class CostumDictionaryExamples
    {
        public static void Main(string[] args)
        {        
            CostumDictionary<string, int> costumDictionary = new CostumDictionary<string, int>();
            costumDictionary.Add("Pesho", 16); 
            costumDictionary.Add("Ivan", 19);
            costumDictionary.Add("Gosho", 20);
            foreach (var person in costumDictionary)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine();
            costumDictionary.Remove("Ivan");
            foreach (var person in costumDictionary)
            {
                Console.WriteLine(person);
            }
        } 
    }
}