using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        var list = new ReversedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(40);
        list.Add(50);



        list.RemoveAt(0);
        list.RemoveAt(0);
        list.RemoveAt(0);

        var mylist = new List<int>();

        System.Console.WriteLine();
    }
}
