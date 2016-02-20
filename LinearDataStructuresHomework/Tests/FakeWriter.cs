namespace Tests
{
    using System.Text;
    using IO.Interfaces;

    public class FakeWriter : IFakeWriter
    {
        private StringBuilder resultToPrint;

        public FakeWriter()
        {
            this.resultToPrint = new StringBuilder();
        }

        public string ResultToPrint
        {
            get
            {
                return this.resultToPrint.ToString();
                
            }
        }

        public void Write(string message)
        {
            this.resultToPrint.Append(message);
        }

        public void WriteLine(string message)
        {
            this.resultToPrint.AppendLine(message);
        }


    }


}