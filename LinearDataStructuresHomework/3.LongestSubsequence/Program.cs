namespace _3.LongestSubsequence
{
    using IO;
    using IO.Interfaces;

    public class Program
    {
        private static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            LongestSequenceFinder.FindLongestSubseqeunce(reader, writer);
        }
    }
}