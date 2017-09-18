using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public class Node<T>
    {
        public T Value { get; private set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }

   public Node<T> head;
   public Node<T> tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.head == null)
        {
            this.head = this.tail = new Node<T>(element);
        }
        else
        {
            var newhead = new Node<T>(element);
            newhead.Previous = null;
            newhead.Next = this.head;
            this.head.Previous = newhead;
            this.head = newhead;

        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.tail = this.head = new Node<T>(element);
        }
        else
        {
            var newtail = new Node<T>(element);
            newtail.Previous = this.tail;
            newtail.Next = null;

            this.tail.Next = newtail;
            this.tail = newtail;

        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count==0)
        {
            throw new InvalidOperationException("List empty");
        }

        var firstEl = this.head.Value;
       

        if (this.Count==1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
            this.head.Previous = null;
        }

        this.Count--;
        return firstEl;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var lastElement = this.tail.Value;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.tail = this.tail.Previous;
            this.tail.Next = null;
        }

        this.Count--;
        return lastElement;
    }

    public void ForEach(Action<T> action)
    {
        var current = this.head;
        while (current!=null)
        {
            action(current.Value);
            current = current.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;
        while (current!=null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        if (this.Count==0)
        {
            return  new T[0];
        }

        var currentNode = this.head;
        var resultArr = new T[this.Count];
        int index = 0;
        while (currentNode != null)
        {
            resultArr[index] = currentNode.Value;
            currentNode = currentNode.Next;
            index++;
        }

        return resultArr;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
