#define DEBUG
namespace LinkedLists
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Doubly Linked List. Begin Tests.");

            // Test 1
            var list1 = new DoublyLinkedList<string>();
            System.Diagnostics.Debug.Assert(list1.Count == 0);
            System.Diagnostics.Debug.Assert(list1.First == null);
            System.Diagnostics.Debug.Assert(list1.Last == null);
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == string.Empty);

            // Test 2
            list1.AddFirst("item-1");
            System.Diagnostics.Debug.Assert(list1.Count == 1);
            System.Diagnostics.Debug.Assert(list1.First.Value == "item-1");
            System.Diagnostics.Debug.Assert(list1.Last.Value == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == "item-1");

            // Test 3
            list1.AddFirst("item-2");
            System.Diagnostics.Debug.Assert(list1.Count == 2);
            System.Diagnostics.Debug.Assert(list1.First.Value == "item-2");
            System.Diagnostics.Debug.Assert(list1.Last.Value == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == "item-2; item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == "item-1; item-2");

            // Test 4
            list1.AddLast("item-3");
            System.Diagnostics.Debug.Assert(list1.Count == 3);
            System.Diagnostics.Debug.Assert(list1.First.Value == "item-2");
            System.Diagnostics.Debug.Assert(list1.Last.Value == "item-3");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == "item-2; item-1; item-3");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == "item-3; item-1; item-2");

            // Test 5
            list1.RemoveLast();
            System.Diagnostics.Debug.Assert(list1.Count == 2);
            System.Diagnostics.Debug.Assert(list1.First.Value == "item-2");
            System.Diagnostics.Debug.Assert(list1.Last.Value == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == "item-2; item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == "item-1; item-2");

            // Test 6
            list1.RemoveFirst();
            System.Diagnostics.Debug.Assert(list1.Count == 1);
            System.Diagnostics.Debug.Assert(list1.First.Value == "item-1");
            System.Diagnostics.Debug.Assert(list1.Last.Value == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == "item-1");

            // Test 7
            list1.Clear();
            System.Diagnostics.Debug.Assert(list1.Count == 0);
            System.Diagnostics.Debug.Assert(list1.First == null);
            System.Diagnostics.Debug.Assert(list1.Last == null);
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetEnumerable()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", list1.GetReversedEnumerable()) == string.Empty);

            System.Console.WriteLine("Doubly Linked List. End Tests.");
        }
    }

    public class Node<TValueType>
    {
        public TValueType Value { get; set; }

        public Node<TValueType> NextNode { get; set; }

        public Node<TValueType> PreviousNode { get; set; } 
    }

    public class DoublyLinkedList<TValueType>
    {
        private Node<TValueType> _first;
        private Node<TValueType> _last;
        private int _count;

        public DoublyLinkedList()
        {
            _first = null;
            _last = null;
            _count = 0;
        }

        public Node<TValueType> First { get { return _first; } }

        public Node<TValueType> Last { get { return _last; } }

        public int Count { get { return _count; } }

        public void Clear()
        {
            _first = null;
            _last = null;
            _count = 0;
        }

        public Node<TValueType> AddFirst(TValueType value)
        {
            var node = new Node<TValueType> { Value = value };
            this.AddFirst(node);
            return node;
        }

        public void AddFirst(Node<TValueType> node)
        {
            if (node == null)
            {
                throw new System.ArgumentNullException("node");
            }

            if ((node.NextNode != null) || (node.PreviousNode != null))
            {
                throw new System.ArgumentException("Looks like the provided Node is already linked to something.");
            }

            if (_count == 0)
            {
                _first = node;
                _last = node;
            }
            else
            {
                _first.PreviousNode = node;
                node.NextNode = _first;
                _first = node;
            }

            _count++;
        }

        public Node<TValueType> AddLast(TValueType value)
        {
            var node = new Node<TValueType> { Value = value };
            this.AddLast(node);
            return node;
        }

        public void AddLast(Node<TValueType> node)
        {
            if (node == null)
            {
                throw new System.ArgumentNullException("node");
            }

            if ((node.NextNode != null) || (node.PreviousNode != null))
            {
                throw new System.ArgumentException("Looks like the provided Node is already linked to something.");
            }

            if (_count == 0)
            {
                _last = node;
                _first = node;
            }
            else
            {
                _last.NextNode = node;
                node.PreviousNode = _last;
                _last = node;
            }

            _count++;
        }

        public void RemoveFirst()
        {
            if (_count == 0)
            {
                throw new System.InvalidOperationException("Uh oh. The list is empty.");
            }

            if (_count == 1)
            {
                _first = null;
                _last = null;
            }
            else
            {
                var nodeToBeRemoved = _first;
                _first = _first.NextNode;
                _first.PreviousNode = null; // Unlink the node
                nodeToBeRemoved.NextNode = null; // Unlink the node
            }

            _count--;
        }

        public void RemoveLast()
        {
            if (_count == 0)
            {
                throw new System.InvalidOperationException("Uh oh. The list is empty.");
            }

            if (_count == 1)
            {
                _first = null;
                _last = null;
            }
            else
            {
                var lastButOne = _last.PreviousNode;
                lastButOne.NextNode = null; // Unlink the node
                _last.PreviousNode = null; // Unlink the node
                _last = lastButOne;
            }

            _count--;
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            var current = _first;
            while (current != null)
            {
                yield return current.Value;
                current = current.NextNode;
            }
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetReversedEnumerable()
        {
            var current = _last;
            while (current != null)
            {
                yield return current.Value;
                current = current.PreviousNode;
            }
        }
    }
}

