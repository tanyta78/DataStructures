﻿using System;

public class ArrayList<T>
{
    private T[] data;

    public ArrayList()
    {
        this.data = new T[2];
    }

    public int Count
    {
        get;
        private set;
    }

 
    public void Add(T item)
    {
        if (this.data.Length <= this.Count)
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
        Array.Copy(this.data,newArr,this.Count);
        this.data = newArr;
    }
}
