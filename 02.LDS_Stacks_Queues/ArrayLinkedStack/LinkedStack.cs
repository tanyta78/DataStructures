using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedStackArr<T> : IEnumerable<T>
{
    public int Count { get; set; }
    private StackNode top;

    public void Push(T element)
    {

        this.top = new StackNode(element, this.top);

        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T result = this.top.Value;
        this.top = this.top.Next;
        this.Count--;

        return result;
    }


    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.top.Value;
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];

        int index = 0;
        StackNode current = this.top;

        while (current != null)
        {
            array[index++] = current.Value;
            current = current.Next;
        }

        return array;
    }


    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class StackNode
    {
        public T Value { get; set; }
        public StackNode Next { get; set; }


        public StackNode(T value, StackNode next)
        {
            this.Value = value;
            this.Next = next;
        }
    }
}

