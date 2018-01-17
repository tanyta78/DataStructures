using System;

public class Program
{
   public static void Main(string[] args)
    {
        LinkedStack<int> stack = new LinkedStack<int>();

        stack.Push(5);
        stack.Push(8);
        stack.Push(-2);
        stack.Push(11);
        stack.Push(6);
        stack.Push(15);

        Console.WriteLine(string.Join(", ", stack.ToArray()));
    }
}

