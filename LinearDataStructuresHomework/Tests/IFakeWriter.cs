namespace Tests
{
    using IO.Interfaces;

    public interface IFakeWriter : IWriter
    {
        string ResultToPrint { get; } 
    }
}