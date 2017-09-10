using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedrList<T> : IEnumerable<T>
{
    private const int Initial_Capacity = 2;

    private T[] items;

    public ReversedrList()
    {
        this.items = new T[Initial_Capacity];
    }

    public int Count { get; set; }

    public int Capacity
    {
        get
        {
            return this.items.Length;
        }
        private set
        {

        }
    }
    public T this[int index]
    {


        get
        {
            index = this.Count - index - 1;

            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.items[index];

        }

        set
        {
            index = this.Count - index - 1;

            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.items[index] = value;
        }



    }

    public void Add(T item)
    {
        if (this.Count == this.items.Length)
        {
            this.Resize();
        }

        this.items[this.Count++] = item;
    }

    private void Resize()
    {
        T[] copy = new T[this.items.Length * 2];

        for (int i = 0; i < this.items.Length; i++)
        {
            copy[i] = this.items[i];
        }

        this.items = copy;
    }

    public T RemoveAt(int index)
    {
        index = this.Count - index - 1;

        if (index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        T element = this.items[index];
        this.items[index] = default(T);
        this.Shift(index);
        this.Count--;

        if (this.Count <= this.items.Length / 4)
        {
            this.Shrink();
        }

        return element;

    }

    private void Shrink()
    {
        T[] copy = new T[this.items.Length / 2];

        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this.items[i];
        }

        this.items = copy;
    }

    private void Shift(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.items[i] = this.items[i + 1];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {

        int br = this.Count - 1;

        while (br >= 0)
        {
            yield return this.items[br];
            br--;
        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        int br = this.Count - 1;

        while (br >= 0)
        {
            yield return this.items[br - 1];
            br--;
        }

    }



}