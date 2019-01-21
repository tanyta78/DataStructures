using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);

        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int newElementIndex)
    {
        int parentIndex = (newElementIndex - 1) / 2;

        while (this.IsGreater(newElementIndex, parentIndex))
        {
            this.Swap(newElementIndex, parentIndex);
            newElementIndex = parentIndex;
            parentIndex = (newElementIndex - 1) / 2;
        }

    }

    private void Swap(int a, int b)
    {
        T temp = this.heap[a];
        this.heap[a] = this.heap[b];
        this.heap[b] = temp;
    }

    private bool IsGreater(int index, int parent)
    {
        return this.heap[index].CompareTo(this.heap[parent]) > 0;

    }

    public T Peek()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T result = this.heap[0];
        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown(0);


        return result;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.Count / 2)
        {
            int leftChild = 2 * index + 1;
            int rightChild = leftChild + 1;
            int biggerChild = leftChild;

            if (rightChild < this.Count && this.IsGreater(rightChild, leftChild))
            {
                biggerChild++;
            }

            if (this.IsGreater(index, biggerChild))
            {
                break;
            }

            this.Swap(biggerChild, index);

            index = biggerChild;
        }
    }
}
