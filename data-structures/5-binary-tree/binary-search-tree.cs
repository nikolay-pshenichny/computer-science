#define DEBUG
namespace Trees
{
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Binary Search Tree. Begin Tests.");

            // Test 1
            var tree1 = new BinarySearchTree<int>();
            System.Diagnostics.Debug.Assert(tree1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraverseInOrder()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePostOrder()) == string.Empty);

            // Test 2
            new int[] { 4, 2, 6, 1, 3, 5, 7 }.ToList().ForEach(i => tree1.Add(i));
            System.Diagnostics.Debug.Assert(tree1.Count == 7);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 6; 5; 7");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraverseInOrder()) == "1; 2; 3; 4; 5; 6; 7");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePostOrder()) == "1; 3; 2; 5; 7; 6; 4");

            // Test 3
            tree1.Clear();
            System.Diagnostics.Debug.Assert(tree1.Count == 0);

            // Test 4
            new int[] { 4, 2, 8, 1, 3, 6, 7, 5 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Delete(8);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 6; 5; 7");

            // Test 5
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 7, 8 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Delete(6);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 7; 5; 8");

            // Test 6
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 8, 7 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Delete(6);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 7; 5; 8");

            // Test 6
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 7 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Delete(1);
            tree1.Delete(7);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 3; 6; 5");


            System.Console.WriteLine("Binary Search Tree. End Tests.");
        }
    }

    public class BinarySearchTree<TValueType>
        where TValueType : System.IComparable<TValueType>
    {
        private TreeNode<TValueType> _head;
        private int _count;

        public BinarySearchTree()
        {
            _head = null;
            _count = 0;
        }

        public int Count { get { return _count; } }

        public TreeNode<TValueType> Head { get { return _head; } }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        public void Add(TValueType value)
        {
            var newNode = new TreeNode<TValueType> { Value = value };

            if (_head == null)
            {
                // If the tree was empty, just set the new node as the _head;
                _head = newNode;
                _count++;
                return;
            }

            // Find a place to the left or right from the _head and insert the newNode;
            recursiveAdd(_head, newNode);

            _count++;
        }

        public void Delete(TValueType value)
        {
            var findeNodeByValueResult = this.FindNodeByValue(value);
            var nodeToBeDeleted = findeNodeByValueResult.Node;
            var parentNode = findeNodeByValueResult.Parent;

            // Node to be deleted doesn't exist
            if (nodeToBeDeleted == null)
            {
                return;
            }

            // Node to be deleted is a regular node with child nodes
            // Three (3) different cases of deletetion logic should be considered here
            // Case 1: NodeToBeDeleted has no right child.
            if ((nodeToBeDeleted.Right == null))
            {
                // In this case the Left child replaces the node that we are deleting
                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, nodeToBeDeleted.Left);
                }
                else
                {
                    _head = nodeToBeDeleted.Left;
                }

                _count--;

                return;
            }
            
            // Case 2: NodeToBeDeleted has a Righ child and the Right child has no left child
            if ((nodeToBeDeleted.Right != null) && (nodeToBeDeleted.Right.Left == null))
            {
                // In this case the Right child replaces the node that we are deleting
                // Note: don't forget that nodeToBeDeleted can also have Left child, which should be preserved 
                //       (ie. moved to the node that will replace the nodeToBeDeleted)
                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, nodeToBeDeleted.Right);
                }
                else
                {
                    _head = nodeToBeDeleted.Right;
                }

                // Preserve the left sub-tree of the NodeToBeDeleted
                nodeToBeDeleted.Right.Left = nodeToBeDeleted.Left;

                _count--;

                return;
            }

            // Case 3: NodeToBeDeleted has a Right child and the Right child has a left child
            if ((nodeToBeDeleted.Right != null) && (nodeToBeDeleted.Right.Left != null))
            {
                // In this case the left-most child of the Right child of the NodeToBeDeleted should replace the NodeToBeDeleted

                // Find the left-most child
                var leftMostChildOfTheRightNode = nodeToBeDeleted.Right.Left;
                var parentOfLeftMostChildOfTheRightNode = nodeToBeDeleted.Right;
                while (leftMostChildOfTheRightNode.Left != null)
                {
                    leftMostChildOfTheRightNode = leftMostChildOfTheRightNode.Left;
                    parentOfLeftMostChildOfTheRightNode = leftMostChildOfTheRightNode;
                }

                // Right subtree of the left-most child should be disconnected from it and connected to the left to the parent of the leftmost child
                parentOfLeftMostChildOfTheRightNode.Left = leftMostChildOfTheRightNode.Right;

                // Since the left-most child is going to replace NodeToBeDeleted, we need to move Left and Right subtrees of NodeToBeDeleted to
                // the left-most child
                leftMostChildOfTheRightNode.Left = nodeToBeDeleted.Left;
                leftMostChildOfTheRightNode.Right = nodeToBeDeleted.Right;


                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, leftMostChildOfTheRightNode);
                }
                else
                {
                    _head = leftMostChildOfTheRightNode;
                }
            }
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePreOrder()
        {
            return this.TraversePreOrder(_head);
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePreOrder(TreeNode<TValueType> node)
        {
            // Note: Recursive algorithm can cause "Stack Overflow" errors. Can be replaced with a "loop and a Stack"
            if (node != null)
            {
                yield return node.Value;
                foreach (var item in TraversePreOrder(node.Left)) { yield return item; }
                foreach (var item in TraversePreOrder(node.Right)) { yield return item; }
            }
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraverseInOrder()
        {
            return this.TraverseInOrder(_head);
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraverseInOrder(TreeNode<TValueType> node)
        {
            if (node != null)
            {
                foreach (var item in TraverseInOrder(node.Left)) { yield return item; }
                yield return node.Value;
                foreach (var item in TraverseInOrder(node.Right)) { yield return item; }
            }
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePostOrder()
        {
            return this.TraversePostOrder(_head);
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePostOrder(TreeNode<TValueType> node)
        {
            if (node != null)
            {
                foreach (var item in TraversePostOrder(node.Left)) { yield return item; }
                foreach (var item in TraversePostOrder(node.Right)) { yield return item; }
                yield return node.Value;
            }
        }

        private FindNodeByValueResult FindNodeByValue(TValueType value)
        {
            var current = _head;
            TreeNode<TValueType> parent = null;

            while (current != null)
            {
                var comparingResult = current.CompareTo(value);
                bool requestedValueIsLessThanValueOfCurrentNode = comparingResult > 0;
                bool requestedValueIsGreaterThanValueOfCurrentNode = comparingResult < 0;
                bool requestedValueIsEqualToValueOfCurrentNode = comparingResult == 0;

                if (requestedValueIsEqualToValueOfCurrentNode)
                {
                    break;
                }
                else if (requestedValueIsLessThanValueOfCurrentNode)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (requestedValueIsGreaterThanValueOfCurrentNode)
                {
                    parent = current;
                    current = current.Right;
                }
            }

            return new FindNodeByValueResult { Node = current, Parent = parent };
        }

        private void recursiveAdd(TreeNode<TValueType> parent, TreeNode<TValueType> nodeToAdd)
        {
            bool newNodeValueIsLessThanValueOfParentNode = nodeToAdd.Value.CompareTo(parent.Value) < 0;

            if (newNodeValueIsLessThanValueOfParentNode)
            {
                if (parent.Left == null)
                {
                    parent.Left = nodeToAdd;
                }
                else
                {
                    recursiveAdd(parent.Left, nodeToAdd);
                }
            }
            else
            {
                if (parent.Right == null)
                {
                    parent.Right = nodeToAdd;
                }
                else
                {
                    recursiveAdd(parent.Right, nodeToAdd);
                }
            }
        }

        public class FindNodeByValueResult
        {
            public TreeNode<TValueType> Node { get; set; }
            public TreeNode<TValueType> Parent { get; set; }
        }
    }

    public class TreeNode<TValueType> : System.IComparable<TValueType>
        where TValueType : System.IComparable<TValueType>
    {
        public TreeNode<TValueType> Left { get; set; }
        public TreeNode<TValueType> Right { get; set; }
        public TValueType Value { get; set; }

        public bool IsLeafNode { get { return (Left == null) && (Right == null); } }

        public int CompareTo(TValueType other)
        {
            return this.Value.CompareTo(other);
        }

        public bool ReplaceChild(TreeNode<TValueType> childToBeReplaced, TreeNode<TValueType> newChild)
        {
            if (this.Left == childToBeReplaced)
            {
                this.Left = newChild;
                return true;
            }
            else if (this.Right == childToBeReplaced)
            {
                this.Right = newChild;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            var l = (this.Left != null) ? string.Format("L:'{0}'", this.Left.Value) : "L: missing";
            var r = (this.Right != null) ? string.Format("R:'{0}'", this.Right.Value) : "R: missing";
            return string.Format("V:'{0}' {1} {2}", this.Value, l, r);
        }
    }
}

 