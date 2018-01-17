//
//Implement a data structure ArrayList<T> that holds a sequence of elements of generic type T.It should hold a sequence of items in an array.The structure should have capacity that grows twice when it is filled, always starting at 2. The list should support the following operations:
//•	int Count  returns the number of elements in the structure 
//•	T this[int index]  the indexer should access the elements by index (in range 0 … Count-1) in the reverse order of adding
//•	void Add(T item)  adds an element to the sequence(grow twice the underlying array to extend its capacity in case the capacity is full)
//•	T RemoveAt(int index)  removes an element by index(in range 0 … Count-1) and returns the element
//Be sure to test implemented operations whenever possible before moving to the next

using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayList<T>:IEnumerable<T>
{
    private const int InitialCapacity = 2;
    private T[] data;

    public ArrayList()
    {
        this.data = new T[InitialCapacity];
    }

    public int Count
    {
        get;
        private set;
    }

    public T this[int index]
    {
        get
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.data[index];
        }

        set
        {
            if (index >= this.Count || index < 0)
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
        T[] newArr = new T[this.Count * 2];
        Array.Copy(this.data, newArr, this.Count);
        this.data = newArr;
    }

    public T RemoveAt(int index)
    {
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
        int index =0;

        while (index <= this.Count - 1)
        {
            yield return this.data[index];
            index++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
