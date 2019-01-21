using System;

public static class Heap<T> where T : IComparable<T>
{
    //inplace sort algo
    public static void Sort(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            HeapifyDown(arr, 1, arr.Length);
        }
        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
        
    }

    private static void HeapifyDown(T[] arr, int index, int length)
    {
        while (index < length / 2)
        {
            int leftChild = 2 * index + 1;
            int rightChild = leftChild + 1;
            int biggerChild = leftChild;

            if (rightChild < length && IsGreater(arr, rightChild, leftChild))
            {
                biggerChild = rightChild;
            }

            if (IsGreater(arr, index, biggerChild))
            {
                break;
            }

            Swap(arr, biggerChild, index);

            index = biggerChild;
        }
    }

    private static void Swap(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }

    private static bool IsGreater(T[] arr, int a, int b)
    {
        return arr[a].CompareTo(arr[b]) > 0;
    }
}

