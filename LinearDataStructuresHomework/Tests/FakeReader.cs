namespace Tests
{
    using IO.Interfaces;

    public class FakeReader : IReader
    {
        private readonly string[] Output =
        {
            "6 6 6 4 4 4"
        };

        public string ReadLine()
        {
            return this.Output[0];
            
        }
    }
}