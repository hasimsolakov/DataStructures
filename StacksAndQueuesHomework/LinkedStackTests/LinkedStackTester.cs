namespace LinkedStackTests
{
    using System;
    using _05.LinkedStack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedStackTester
    {
        [TestMethod]
        public void Test_Push_Pop_ShouldIncrementAndDecrementCount_ReturnCorrectElement()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            Assert.AreEqual(stack.Count, 0);

            int elementToPush = 8;
            stack.Push(elementToPush);
            Assert.AreEqual(stack.Count, 1);

            int popedElement = stack.Pop();
            Assert.AreEqual(elementToPush, popedElement);

            Assert.AreEqual(stack.Count, 0);
        }

        [TestMethod]
        public void Test_Push_Pop_With1000Elements()
        {
            LinkedStack<string> stack = new LinkedStack<string>();
            Assert.AreEqual(stack.Count, 0);

            for (int iteration = 0; iteration < 1000; iteration++)
            {
                stack.Push(iteration.ToString());
                Assert.AreEqual(stack.Count, iteration + 1);
            }

            for (int iteration = 999; iteration >= 0; iteration--)
            {
                string poppedElement = stack.Pop();
                Assert.AreEqual(poppedElement, iteration.ToString());
                Assert.AreEqual(stack.Count, iteration);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_PopFromEmptyStack_ShouldThrow()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void Test_Push_Pop_WithInitialCapacity()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            Assert.AreEqual(stack.Count, 0);
            stack.Push(5);
            Assert.AreEqual(stack.Count, 1);
            stack.Push(7);
            Assert.AreEqual(stack.Count, 2);
            int poppedElement = stack.Pop();
            Assert.AreEqual(stack.Count, 1);
            Assert.AreEqual(poppedElement, 7);
            int poppedElement2 = stack.Pop();
            Assert.AreEqual(stack.Count, 0);
            Assert.AreEqual(poppedElement2, 5);
        }

        [TestMethod]
        public void Test_ToArray_ShouldReturnArrayInReverseOrder()
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            stack.Push(3);
            stack.Push(5);
            stack.Push(-2);
            stack.Push(7);
            int[] expectedArray = { 7, -2, 5, 3 };
            CollectionAssert.AreEqual(expectedArray, stack.ToArray());
        }

        [TestMethod]
        public void Test_ToArray_WithEmptyStack()
        {
            LinkedStack<DateTime> stack = new LinkedStack<DateTime>();
            CollectionAssert.AreEqual(stack.ToArray(), new int[] { });
        }
    }
}
