#define DEBUG
namespace Set
{
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Set. Begin Tests.");

            // Test 1
            var set1 = new Set<int>();
            System.Diagnostics.Debug.Assert(set1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == string.Empty);

            // Test 2
            set1 = new Set<int>(new[] { 1, 2, 3 });
            System.Diagnostics.Debug.Assert(set1.Count == 3);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "1; 2; 3");

            // Test 3
            set1.Add(4);
            System.Diagnostics.Debug.Assert(set1.Count == 4);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "1; 2; 3; 4");

            // Test 4
            set1.Remove(1);
            System.Diagnostics.Debug.Assert(set1.Count == 3);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "2; 3; 4");

            // Test 5
            set1.UnionWith(new[] { 1, 2, 4, 5 });
            System.Diagnostics.Debug.Assert(set1.Count == 5);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "2; 3; 4; 1; 5");

            // Test 6
            set1.Except(new[] { 2, 3, 6 });
            System.Diagnostics.Debug.Assert(set1.Count == 3);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "4; 1; 5");

            // Test 7
            set1.Intersect(new[] { 2, 1, 4, 6 });
            System.Diagnostics.Debug.Assert(set1.Count == 2);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "1; 4");

            // Test 8
            set1.SymmetricExceptWith(new[] { 4, 5 });
            System.Diagnostics.Debug.Assert(set1.Count == 2);
            System.Diagnostics.Debug.Assert(string.Join("; ", set1.GetEnumerable()) == "1; 5");

            System.Console.WriteLine("Set. End Tests.");
        }
    }

    public class Set<TValueType>
    {
        private List<TValueType> _items;

        public Set()
            : this(new TValueType[0])
        {
            //
        }

        public Set(IEnumerable<TValueType> source)
        {
            _items = new List<TValueType>();

            foreach (var item in source)
            {
                this.Add(item);
            }
        }

        public int Count { get { return _items.Count; } }

        public void Clear()
        {
            _items.Clear();
        }

        public void Add(TValueType value)
        {
            // Add the item, only if it is not in the _items yet.
            if (!_items.Contains(value))
            {
                _items.Add(value);
            }
        }

        public void Remove(TValueType value)
        {
            _items.Remove(value);
        }

        public bool Contains(TValueType value)
        {
            return _items.Contains(value);
        }

        /// <summary>
        /// Modify the current set to contain all values from the Current and Other sets
        /// </summary>
        /// <param name="other"></param>
        public void UnionWith(IEnumerable<TValueType> other)
        {
            _items = this.Union(_items, other);
        }

        /// <summary>
        /// Modify the current set to contain only values that are present in both sets
        /// </summary>
        /// <param name="other"></param>
        public void Intersect(IEnumerable<TValueType> other)
        {
            _items = this.Intersect(_items, other);
        }

        /// <summary>
        /// Modify the current set to contain only values that are not present in the Other set
        /// </summary>
        /// <param name="other"></param>
        public void Except(IEnumerable<TValueType> other)
        {
            _items = this.Except(_items, other);
        }

        /// <summary>
        /// Modify the current set to contain values that are present only in one of the sets (Current or Other)
        /// </summary>
        /// <param name="other"></param>
        public void SymmetricExceptWith(IEnumerable<TValueType> other)
        {
            var union = this.Union(this._items, other);
            var intersection = this.Intersect(this._items, other);
            _items = this.Except(union, intersection);
        }


        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        private List<TValueType> Union(List<TValueType> source, IEnumerable<TValueType> other)
        {
            var result = new List<TValueType>(source);

            foreach (var otherItem in other)
            {
                if (!result.Contains(otherItem))
                {
                    result.Add(otherItem);
                }
            }

            return result;
        }

        private List<TValueType> Intersect(List<TValueType> source, IEnumerable<TValueType> other)
        {
            var result = new List<TValueType>();

            foreach (var otherItem in other)
            {
                if (source.Contains(otherItem))
                {
                    result.Add(otherItem);
                }
            }

            return result;
        }

        private List<TValueType> Except(List<TValueType> source, IEnumerable<TValueType> other)
        {
            var result = new List<TValueType>(source);

            foreach (var otherItem in other)
            {
                result.Remove(otherItem);
            }

            return result;
        }
    }
}