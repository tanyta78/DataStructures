using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayLinkedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedStackArr<int> stack = new LinkedStackArr<int>();

            stack.Push(5);
            stack.Push(8);
            stack.Push(-2);
            stack.Push(11);
            stack.Push(6);
            stack.Push(15);

            Console.WriteLine(string.Join(", ",stack.ToArray()));
        }
    }
}
