using System;

public class ArrayStack<T>
{
    private T[] elements;

    public int Count
    {
        get;
        private set;
       
    }

    private const int InitialCapacity = 16;

    public ArrayStack()
    {
        this.elements=new T[InitialCapacity];
    }

    public ArrayStack(int capacity)
    {
        this.elements = new T[capacity];
    }


    public void Push(T element)
    {
        if (this.Count==this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count<=0)
        {
            throw  new InvalidOperationException("Stack is empty");
        }

        var element = this.elements[this.Count - 1];
        this.Count--;

        return element;
    }

    public T[] ToArray()
    {
        var result = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            result[i] = this.elements[this.Count - 1 - i];
        }
        return result;

    }

    private void Grow()
    {
        var currentLen = this.elements.Length;
        var newElements = new T[currentLen * 2];

        this.elements.CopyTo(newElements,0);

        this.elements = newElements;
    }
}

