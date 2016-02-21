namespace DoubleLinkedQueueTests
{
    using System;
    using _07.DoubleLinkedQueue;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedQueueTester
    {
        [TestMethod]
        public void Test_Push_Pop_ShouldIncrementAndDecrementCount_ReturnCorrectElement()
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();
            Assert.AreEqual(linkedQueue.Count, 0);

            int elementToEnqueue = 8;
            linkedQueue.Enqueue(elementToEnqueue);
            Assert.AreEqual(linkedQueue.Count, 1);

            int dequeuedElement = linkedQueue.Dequeue();
            Assert.AreEqual(elementToEnqueue, dequeuedElement);

            Assert.AreEqual(linkedQueue.Count, 0);
        }

        [TestMethod]
        public void Test_Push_Pop_With1000Elements()
        {
            LinkedQueue<string> linkedQueue = new LinkedQueue<string>();
            Assert.AreEqual(linkedQueue.Count, 0);

            for (int iteration = 0; iteration < 1000; iteration++)
            {
                linkedQueue.Enqueue(iteration.ToString());
                Assert.AreEqual(linkedQueue.Count, iteration + 1);
            }

            for (int iteration = 0; iteration < 1000; iteration++)
            {
                string dequeuedElement = linkedQueue.Dequeue();
                Assert.AreEqual(dequeuedElement, iteration.ToString());
                Assert.AreEqual(linkedQueue.Count, 999 - iteration);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_PopFromEmptyStack_ShouldThrow()
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();
            linkedQueue.Dequeue();
        }

        [TestMethod]
        public void Test_Push_Pop_WithInitialCapacity()
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();
            Assert.AreEqual(linkedQueue.Count, 0);

            linkedQueue.Enqueue(5);
            Assert.AreEqual(linkedQueue.Count, 1);

            linkedQueue.Enqueue(7);
            Assert.AreEqual(linkedQueue.Count, 2);

            int dequeuedElement = linkedQueue.Dequeue();
            Assert.AreEqual(linkedQueue.Count, 1);
            Assert.AreEqual(dequeuedElement, 5);

            int dequeuedElement2 = linkedQueue.Dequeue();
            Assert.AreEqual(linkedQueue.Count, 0);
            Assert.AreEqual(dequeuedElement2, 7);
        }

        [TestMethod]
        public void Test_ToArray_ShouldReturnArrayInNormalOrder()
        {
            LinkedQueue<int> linkedQueue = new LinkedQueue<int>();
            linkedQueue.Enqueue(3);
            linkedQueue.Enqueue(5);
            linkedQueue.Enqueue(-2);
            linkedQueue.Enqueue(7);
            int[] expectedArray = { 3, 5, -2, 7 };
            CollectionAssert.AreEqual(expectedArray, linkedQueue.ToArray());
        }

        [TestMethod]
        public void Test_ToArray_WithEmptyStack()
        {
            LinkedQueue<DateTime> linkedQueue = new LinkedQueue<DateTime>();
            CollectionAssert.AreEqual(linkedQueue.ToArray(), new int[] { });
        }
    }
}
