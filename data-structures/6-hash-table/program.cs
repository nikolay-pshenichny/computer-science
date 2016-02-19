#define DEBUG
namespace HashTables
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Hash Table. Begin Tests.");

            // Test 1
            var hashTable1 = new HashTable<string, int>();
            System.Diagnostics.Debug.Assert(hashTable1.Count == 0);
            //System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == string.Empty);

            // Test 2
            hashTable1.Add("item1", 11);
            hashTable1.Add("item2", 22);
            System.Diagnostics.Debug.Assert(hashTable1.Count == 2);
            //System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == string.Empty);



            System.Console.WriteLine("Hash Table. End Tests.");
        }
    }

    public class HashTableKeyValuePair<TKeyType, TValueType>
    {
        public TKeyType Key { get; set; }
        public TValueType Value { get; set; }
    }

    public class HashTable<TKeyType, TValueType>
    {
        private const int DefaultCapacity = 32;

        private const double AllowedFillFactor = 0.75;

        private System.Collections.Generic.LinkedList<HashTableKeyValuePair<TKeyType, TValueType>>[] _buckets;

        private int _count;

        public HashTable()
        {
            _buckets = new System.Collections.Generic.LinkedList<HashTableKeyValuePair<TKeyType, TValueType>>[DefaultCapacity];
            _count = 0;
        }

        public int Count { get { return _count; } }

        public void Clear()
        {
            if (_buckets != null)
            {
                foreach (var bucket in _buckets)
                {
                    if (bucket != null)
                    {
                        bucket.Clear();
                    }
                }
            }

            _count = 0;
        }

        public TValueType this[TKeyType key]
        {
            get
            {
                int bucketIndex;

                var item = FindItem(key, out bucketIndex);

                return item.Value.Value;
            }
        }

        public void Add(TKeyType key, TValueType value)
        {
            if (NeedToExpandInternalStorage())
            {
                var oldBuckets = _buckets;

                int newCapacity = _buckets.Length * 2;
                _buckets = new System.Collections.Generic.LinkedList<HashTableKeyValuePair<TKeyType, TValueType>>[newCapacity];
                _count = 0;

                // Copy elements from old buckets to new (larger) ones
                foreach(var bucket in oldBuckets)
                {
                    if (bucket != null)
                    {
                        foreach(var item in bucket)
                        {
                            // Re-add all items to the new "expanded" array
                            this.Add(item.Key, item.Value);
                        }
                    }
                }
            }

            var bucketIndex = this.GetBucketIndex(key);

            if (_buckets[bucketIndex] == null)
            {
                _buckets[bucketIndex] = new System.Collections.Generic.LinkedList<HashTables.HashTableKeyValuePair<TKeyType, TValueType>>();
            }

            _buckets[bucketIndex].AddLast(new HashTables.HashTableKeyValuePair<TKeyType, TValueType>
            {
                Key = key,
                Value = value,
            });

            _count++;
        }

        public void Update(TKeyType key, TValueType value)
        {
            int bucketIndex;

            var item = FindItem(key, out bucketIndex);

            item.Value.Value = value;
        }

        public void Remove(TKeyType key)
        {
            int bucketIndex;

            var item = FindItem(key, out bucketIndex);

            _buckets[bucketIndex].Remove(item);

            _count--;
        }

        private System.Collections.Generic.LinkedListNode<HashTables.HashTableKeyValuePair<TKeyType, TValueType>> FindItem(
            TKeyType key,
            out int bucketIndex)
        {
            bucketIndex = this.GetBucketIndex(key);

            // If the target bucket is not initialized.
            if (_buckets[bucketIndex] == null)
            {
                throw new System.InvalidOperationException("Provided Key cannot be found.");
            }

            // Search for an item with the provided key
            var item = _buckets[bucketIndex].First;
            while ((item != null) && (item.Value.Key.Equals(key)))
            {
                item = item.Next;
            }

            // If item can't be found
            if (item == null)
            {
                throw new System.InvalidOperationException("Provided Key cannot be found.");
            }

            return item;
        }

        private bool NeedToExpandInternalStorage()
        {
            return _count > (_buckets.Length * AllowedFillFactor);
        }

        private int GetHash(TKeyType key)
        {
            return key == null ? 0 : key.GetHashCode();
        }

        private int GetBucketIndex(TKeyType key)
        {
            int hash = GetHash(key) & 0x7FFFFFFF;

            int bucketIndex = hash % _buckets.Length;

            return bucketIndex;
        }
    }

}

