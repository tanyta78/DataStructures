using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    private Node<T> head;
    private Node<T> tail;

    public void AddFirst(T item)
    {
        Node<T> oldHead = this.head;

        this.head = new Node<T>(item);
        this.head.Next = oldHead;

        if (this.Count == 0)
        {
            this.tail = this.head;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        Node<T> oldTail = this.tail;

        this.tail = new Node<T>(item);

        if (this.Count == 0)
        {
            this.head = this.tail;
        }
        else
        {
            oldTail.Next = this.tail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {

        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        Node<T> oldHead = this.head;

        this.head = this.head.Next;

        this.Count--;

        if (this.Count == 0)
        {
            this.tail = null;
        }

        return oldHead.Value;
    }

    public T RemoveLast()
    {

        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        Node<T> oldTail = this.tail;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            Node<T> newTail = this.GetBeforeLastNode();
            newTail.Next = null;
            this.tail = newTail;

        }

        this.Count--;

        return oldTail.Value;
    }

    private Node<T> GetBeforeLastNode()
    {
        Node<T> current = this.head;
        while (current.Next != this.tail)
        {
            current = current.Next;
        }

        return current;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> current = this.head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node<T>
    {
        private T value;
        private Node<T> next;

        public Node(T value)
        {
            this.value = value;
          
        }

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Node<T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
    }

}
