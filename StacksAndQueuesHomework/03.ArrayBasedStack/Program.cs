using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ArrayBasedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayStack<int> stack = new ArrayStack<int>(2);
            Stack<int> defaultStack = new Stack<int>(2);
            defaultStack.Push(6);
            defaultStack.Push(3);
            defaultStack.Push(2);
            stack.Push(6);
            stack.Push(3);
            stack.Push(2);
            Console.WriteLine(string.Join(" ",stack.ToArray()));
            Console.WriteLine();
            Console.WriteLine(string.Join(" ", defaultStack.ToArray()));

        }
    }
}
