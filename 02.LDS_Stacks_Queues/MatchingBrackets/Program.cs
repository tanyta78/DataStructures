namespace MatchingBrackets
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
       public static void Main()
        {
            Stack<int> stack = new Stack<int>();

            string expresion = "1+(2-(2+3)*4/(3+1))*5";

            for (int i = 0; i < expresion.Length; i++)
            {
                if (expresion[i] == '(')
                {
                    stack.Push(i);
                }
                else if (expresion[i] ==')')
                {
                    //to do if stack is empty
                    int startIndex = stack.Pop();
                    Console.WriteLine(expresion.Substring(startIndex,i-startIndex+1));
                }
            }
        }
    }
}
