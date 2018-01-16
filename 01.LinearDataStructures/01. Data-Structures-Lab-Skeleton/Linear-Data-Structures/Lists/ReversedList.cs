using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T>:IEnumerable<T>
{
    private T[] data;

    private const int InitCapacity = 2;

    public int Count { get; set; }
    
    public ReversedList()
    {
        this.data= new T[InitCapacity];
    }

    public int Capacity
    {
        get
        {
            return this.data.Length;
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

            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.data[index];
        }

        set
        {
            index = this.Count - index - 1;
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.data[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.data.Length == this.Count)
        {
            this.Resize();
        }
        this.data[this.Count++] = item;
    }

    private void Resize()
    {
        T[] newReversedList = new T[this.data.Length * 2];

        Array.Copy(this.data, newReversedList, this.Count);
       
        this.data = newReversedList;
    }

    public T RemoveAt(int index)
    {
        index = this.Count - index - 1;
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        T item = this.data[index];

        for (int i = index; i < this.Count; i++)
        {
            this.data[i] = this.data[i + 1];
        }

        this.Count--;
        if (this.Count <= this.data.Length / 4)
        {
            this.Shrink();
        }

        return item;
    }

    private void Shrink()
    {
        T[] newArr = new T[this.data.Length / 2];
        Array.Copy(this.data, newArr, this.Count);
        this.data = newArr;
    }

    public IEnumerator<T> GetEnumerator()
    {
        int index = this.Count - 1;

        while (index >= 0)
        {
            yield return this.data[index];
            index--;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

