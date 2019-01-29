namespace Hash_Table
{
    using System.Collections;
    using System.Collections.Generic;

    public class HashSet<T> : IEnumerable<T>
    {
        private HashTable<T, T> hashTable;

        public HashSet()
        {
            this.hashTable = new HashTable<T, T>();
        }

        public void Add(T item)
        {
            this.hashTable.AddOrReplace(item, item);
        }

        public HashSet<T> UnionWith(IEnumerable<T> other)
        {
            var resultSet = new HashSet<T>();

            foreach (T item in this)
            {
                resultSet.Add(item);
            }

            foreach (var item in other)
            {
                resultSet.Add(item);
            }

            return resultSet;
        }

        public HashSet<T> IntersectsWith(IEnumerable<T> other)
        {
            var resultSet = new HashSet<T>();

            foreach (var item in other)
            {
                if (this.hashTable.ContainsKey(item))
                {
                    resultSet.Add(item);
                }
            }


            return resultSet;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var key in this.hashTable.Keys)
            {
                yield return key;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
