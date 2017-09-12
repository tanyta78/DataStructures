using System.Runtime.InteropServices;
//to do
public class LinkedQueue<T>
    {
        public int Count { get; private set; }
    
        public void Enqueue(T element)
        {
            var node =new QueueNode<T>(element);

        }
        public T Dequeue() { … }
        public T[] ToArray() { … }

        private class QueueNode<T>
        {
            public QueueNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }
            public QueueNode<T> NextNode { get; set; }
            public QueueNode<T> PrevNode { get; set; }
        }

}
