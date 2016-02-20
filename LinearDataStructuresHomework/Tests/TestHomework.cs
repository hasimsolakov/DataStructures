namespace Tests
{
    using IO.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _3.LongestSubsequence;
    [TestClass]
    public class TestHomework
    {
        [TestMethod]
        public void Test_LongestSubsequence_ShouldReturnLeftMost()
        {
            IReader reader = new FakeReader();
            IFakeWriter writer = new FakeWriter();
            LongestSequenceFinder.FindLongestSubseqeunce(reader, writer);
            string expectedResult = "6 6 6";
            string result = writer.ResultToPrint.Trim();
            Assert.AreEqual(expectedResult, result);
        }
    }
}