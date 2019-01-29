using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const float LoadFactor = 0.75f;

    private int maxElements;

    private LinkedList<KeyValue<TKey, TValue>>[] hashTable;

    public int Count { get; private set; }

    public int Capacity => this.hashTable.Length;

    public HashTable(int capacity = DefaultCapacity)
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.maxElements = (int)(capacity * LoadFactor);
    }

    public void Add(TKey key, TValue value)
    {
        this.CheckGrowth();
        var hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var keyValue in this.hashTable[hash])
        {
            if (keyValue.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }

        var kvp = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[hash].AddLast(kvp);
        this.Count++;
    }

    private void CheckGrowth()
    {
        if (this.Count >= this.maxElements)
        {
            this.Resize();
            this.maxElements = (int)(this.Capacity * LoadFactor);
        }
    }

    private void Resize()
    {
        var newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

        foreach (LinkedList<KeyValue<TKey, TValue>> linkedList in this.hashTable)
        {
            if (linkedList != null)
            {
                foreach (KeyValue<TKey, TValue> keyValue in linkedList)
                {
                    newTable.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        this.hashTable = newTable.hashTable;
        this.Count = newTable.Count;

    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.CheckGrowth();
        var hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.hashTable[hash] == null)
        {
            this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
        }


        foreach (var keyValue in this.hashTable[hash])
        {
            if (keyValue.Key.Equals(key))
            {
                keyValue.Value = value;
                return true;
            }
        }

        var kvp = new KeyValue<TKey, TValue>(key, value);
        this.hashTable[hash].AddLast(kvp);
        this.Count++;
        return false;
    }

    public TValue Get(TKey key)
    {
        var element = this.Find(key);

        if (element == null)
        {
            throw new KeyNotFoundException();
        }

        return element.Value;
    }

    public TValue this[TKey key]
    {
        get => this.Get(key);

        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var result = this.Find(key);
        if (result != null)
        {
            value = result.Value;
            return true;
        }

        value = default(TValue);
        return false;

    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.hashTable[hash] != null)
        {
            foreach (var keyValue in this.hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue;
                }
            }
        }
        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var result = this.Find(key);
        return result != null;
    }

    public bool Remove(TKey key)
    {
        var hash = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.hashTable[hash] != null)
        {
            var kvp = this.Find(key);
            if (kvp != null)
            {
                this.hashTable[hash].Remove(kvp);
                this.Count--;
                return true;

            }
        }
        return false;

    }

    public void Clear()
    {
        this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.maxElements = (int)(this.Capacity * LoadFactor);
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

    public IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        return this.hashTable.Where(list => list != null).SelectMany(list => list).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
