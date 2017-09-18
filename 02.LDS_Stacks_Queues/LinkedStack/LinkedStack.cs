using System;

public class LinkedStack<T>
    {
        private Node<T> firstNode;
        public int Count { get; private set; }

        public void Push(T element)
        {
            var node = new LinkedStack<T>.Node<T>(element);

            if (this.Count == 0)
            {
                this.firstNode = node;
            }
            else
            {
                node.NextNode = this.firstNode;
                this.firstNode = node;
            }

            this.Count++;
        }

        public T Pop()
        {
            if (this.Count<=0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            var first = this.firstNode;
            this.firstNode = this.firstNode.NextNode;

            this.Count--;

            return first.Value;
        }
    
        public T[] ToArray()
        {
            var result = new T[this.Count];
            var nextNode = this.firstNode;

            for (int i = 0; i < this.Count; i++)
            {
                result[i] = nextNode.Value;
                nextNode = nextNode.NextNode;
            }

            return result;
        }


        private class Node<T>
        {
            public T Value { get; }
            public Node<T> NextNode { get; set; }

            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }
        }

}

