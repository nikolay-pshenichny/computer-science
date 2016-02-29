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
            var tree1 = new AVLTree<int>();
            System.Diagnostics.Debug.Assert(tree1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraverseInOrder()) == string.Empty);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePostOrder()) == string.Empty);

            // Test 2
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.ToList().ForEach(i => tree1.Add(i));
            System.Diagnostics.Debug.Assert(tree1.Count == 10);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 8; 6; 5; 7; 9; 10");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraverseInOrder()) == "1; 2; 3; 4; 5; 6; 7; 8; 9; 10");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePostOrder()) == "1; 3; 2; 5; 7; 6; 10; 9; 8; 4");

            // Test 3
            tree1.Clear();
            new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }.ToList().ForEach(i => tree1.Add(i));
            System.Diagnostics.Debug.Assert(tree1.Count == 10);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "7; 3; 2; 1; 5; 4; 6; 9; 8; 10");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraverseInOrder()) == "1; 2; 3; 4; 5; 6; 7; 8; 9; 10");
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePostOrder()) == "1; 2; 4; 6; 5; 3; 8; 10; 9; 7");

            // Test 4
            tree1.Clear();
            System.Diagnostics.Debug.Assert(tree1.Count == 0);

            // Test 5
            new int[] { 4, 2, 8, 1, 3, 6, 7, 5 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Remove(8);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 6; 5; 7");

            // Test 6
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 7, 8 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Remove(6);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 7; 5; 8");

            // Test 7
            tree1.Remove(7);
            tree1.Remove(8);
            tree1.Remove(5);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "2; 1; 4; 3");

            // Test 8
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 8, 7 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Remove(6);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 1; 3; 7; 5; 8");

            // Test 9
            tree1.Clear();
            new int[] { 4, 2, 6, 1, 3, 5, 7 }.ToList().ForEach(i => tree1.Add(i));
            tree1.Remove(1);
            tree1.Remove(7);
            System.Diagnostics.Debug.Assert(string.Join("; ", tree1.TraversePreOrder()) == "4; 2; 3; 6; 5");

            System.Console.WriteLine("Binary Search Tree. End Tests.");
        }
    }

    public class AVLTree<TValueType>
        where TValueType : System.IComparable<TValueType>
    {
        private AVLTreeNode<TValueType> _head;
        private int _count;

        public AVLTree()
        {
            _head = null;
            _count = 0;
        }

        public int Count { get { return _count; } }

        public AVLTreeNode<TValueType> Head { get { return _head; } }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        public void Add(TValueType value)
        {
            var newNode = new AVLTreeNode<TValueType> { Value = value };

            // if the tree was empty, this is going to be our head.
            if (_head == null)
            {
                _head = newNode;
                _count++;
                return;
            }

            // If tree was not empty, we need to find a proper place for the new item
            _head = RecursiveAdd(_head, newNode);

            _count++;
        }

        public void Remove(TValueType value)
        {
            var findNodeByValueResult = this.FindNodeByValue(value);
            var nodeToBeDeleted = findNodeByValueResult.Node;
            var parentNode = findNodeByValueResult.Parent;

            // Node to be deleted doesn't exist
            if (nodeToBeDeleted == null)
            {
                return;
            }

            // Three (3) different cases of deletion logic are below this comment

            // Case 1: NodeToBeDeleted has no Right child
            if (nodeToBeDeleted.Right == null)
            {
                // Left child replaces the node that we are deleting
                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, nodeToBeDeleted.Left);
                }
                else
                {
                    _head = nodeToBeDeleted.Left;
                }
            }
            // Case 2: NodeToBeDeleted has Right child and the Right child has no left child
            else if ((nodeToBeDeleted.Right != null) && (nodeToBeDeleted.Right.Left == null))
            {
                // Right child replaces the node that we are deleting
                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, nodeToBeDeleted.Right);
                }
                else
                {
                    _head = nodeToBeDeleted.Right;
                }

                // Preserve the left subtree of the NodeToBeDeleted
                nodeToBeDeleted.Right.Left = nodeToBeDeleted.Left;
            }
            // Case 3: NodeToBeDeleted has Right child and the Right child has left child
            else if ((nodeToBeDeleted.Right != null) && (nodeToBeDeleted.Right.Left != null))
            {
                // The left-most child of the right child replaces the NodeToBeDeleted
                var findLeftMostNodeResult = FindLeftMostNode(nodeToBeDeleted.Right);

                // Right sub-tree of the left-most child goes to the left of the left-most child parent
                findLeftMostNodeResult.Parent.Left = findLeftMostNodeResult.Node.Right;

                // Move left and right sub-trees of the NodeToBeDeleted to the Left-most child
                findLeftMostNodeResult.Node.Left = nodeToBeDeleted.Left;
                findLeftMostNodeResult.Node.Right = nodeToBeDeleted.Right;

                if (parentNode != null)
                {
                    parentNode.ReplaceChild(nodeToBeDeleted, findLeftMostNodeResult.Node);
                }
                else
                {
                    _head = findLeftMostNodeResult.Node;
                }
            }

            _count--;

            // Rebalance the tree;
            if (parentNode == null)
            {
                _head = this.BalanceTree(_head);
            }
            else
            {
                // Go Up the sub-tree from which we deleted a node and rebalance it.
                AVLTreeNode<TValueType> currentRoot = parentNode;
                AVLTreeNode<TValueType> currentRootsParent = GetParent(currentRoot);

                while (currentRootsParent != null)
                {
                    if (currentRoot.TreeBalance != TreeBalance.Balanced)
                    {
                        var newRoot = this.BalanceTree(currentRoot);
                        currentRootsParent.ReplaceChild(currentRoot, newRoot);
                    }

                    currentRoot = currentRootsParent;
                    currentRootsParent = GetParent(currentRoot);
                }

                _head = this.BalanceTree(_head);
            }
        }

        public bool Contains(TValueType value)
        {
            return FindNodeByValue(value).Node != null;
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePreOrder()
        {
            return this.TraversePreOrder(_head);
        }

        public System.Collections.Generic.IEnumerable<TValueType> TraversePreOrder(AVLTreeNode<TValueType> node)
        {
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

        public System.Collections.Generic.IEnumerable<TValueType> TraverseInOrder(AVLTreeNode<TValueType> node)
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

        public System.Collections.Generic.IEnumerable<TValueType> TraversePostOrder(AVLTreeNode<TValueType> node)
        {
            if (node != null)
            {
                foreach (var item in TraversePostOrder(node.Left)) { yield return item; }
                foreach (var item in TraversePostOrder(node.Right)) { yield return item; }
                yield return node.Value;
            }
        }

        public void PrintTree()
        {
            var lines = new System.Collections.Generic.List<string>();
            this.PrintTree(lines, _head, 1);

            foreach(var line in lines)
            {
                System.Console.WriteLine(line);
            }
        }

        private string PrintTree(System.Collections.Generic.List<string> lines, AVLTreeNode<TValueType> node, int level)
        {
            //TODO: Improve!
            if (lines.Count() < level)
            {
                lines.Add(string.Empty);
            }

            string result = string.Empty;

            if (node != null)
            {
                var l = this.PrintTree(lines, node.Left, level + 1);
                var r = this.PrintTree(lines, node.Right, level + 1);

                lines[level - 1] = lines[level - 1] + string.Format("{0}{1}{2}", l, node.Value, r);

                result = l + r + " ";
            }
            else
            {
                result = "   ";
            }

            return result;
        }

        private FindNodeByValueResult FindNodeByValue(TValueType value)
        {
            AVLTreeNode<TValueType> current = _head;
            AVLTreeNode<TValueType> parent = null;

            while (current != null)
            {
                var comparisonResults = value.CompareTo(current.Value);

                // If values are equal, then we found our node.
                if (comparisonResults == 0)
                {
                    break;
                }
                // if Value we are searching for is less than the value of the current node
                else if (comparisonResults < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                // if Value we are searching for is greater than the value of the current node
                else if (comparisonResults > 0)
                {
                    parent = current;
                    current = current.Right;
                }
            }

            return new FindNodeByValueResult { Node = current, Parent = parent };
        }


        private FindNodeByValueResult FindLeftMostNode(AVLTreeNode<TValueType> node)
        {
            var current = node.Left;
            var parent = node;

            while (current.Left != null)
            {
                current = current.Left;
                parent = current;
            }

            return new FindNodeByValueResult { Node = current, Parent = parent };
        }

        /// <summary>
        /// Note: this method can be removed if we add the "Parent" property to each AVLTreeNode 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AVLTreeNode<TValueType> GetParent(AVLTreeNode<TValueType> node)
        {
            if (node == _head)
            {
                return null;
            }

            return this.FindNodeByValue(node.Value).Parent;
        }

        private AVLTreeNode<TValueType> RecursiveAdd(AVLTreeNode<TValueType> parent, AVLTreeNode<TValueType> nodeToAdd)
        {
            AVLTreeNode<TValueType> newRoot = parent;

            bool newNodeValueIsLessThanValueOfParentNode = nodeToAdd.Value.CompareTo(parent.Value) < 0;

            // Values less than parent go to the left
            if (newNodeValueIsLessThanValueOfParentNode)
            {
                if (parent.Left == null)
                {
                    parent.Left = nodeToAdd;
                }
                else
                {
                    parent.Left = RecursiveAdd(parent.Left, nodeToAdd);
                }
            }
            else
            // Values greater than or equal to parent go to the right
            {
                if (parent.Right == null)
                {
                    parent.Right = nodeToAdd;
                }
                else
                {
                    parent.Right = RecursiveAdd(parent.Right, nodeToAdd);
                }
            }

            // Balance the tree
            newRoot = BalanceTree(parent);

            return newRoot;
        }

        private AVLTreeNode<TValueType> BalanceTree(AVLTreeNode<TValueType> node)
        {
            var newRoot = node;

            var balance = node.TreeBalance;

            switch (balance)
            {
                case TreeBalance.RightHeavy:
                    {
                        if ((node.Right != null) && (node.Right.TreeBalance == TreeBalance.LeftHeavy))
                        {
                            newRoot = LeftRightRotation(node);
                        }
                        else
                        {
                            newRoot = LeftRotation(node);
                        }

                        break;
                    }
                case TreeBalance.LeftHeavy:
                    {
                        if ((node.Left != null) && (node.Left.TreeBalance == TreeBalance.RightHeavy))
                        {
                            newRoot = RightLeftRotation(node);
                        }
                        else
                        {
                            newRoot = RightRotation(node);
                        }

                        break;
                    }
            }

            return newRoot;
        }

        private AVLTreeNode<TValueType> RightRotation(AVLTreeNode<TValueType> oldRoot)
        {
            // Left child becomes new root
            var newRoot = oldRoot.Left;

            // Right child of the new root is assigned to the left child of the old root
            oldRoot.Left = newRoot.Right;

            // Previous root becomes the new root's right child
            newRoot.Right = oldRoot;

            return newRoot;
        }

        private AVLTreeNode<TValueType> LeftRotation(AVLTreeNode<TValueType> oldRoot)
        {
            // Right child becomes new root
            var newRoot = oldRoot.Right;

            // Left child of the new root is assigned to the right child of the old root
            oldRoot.Right = newRoot.Left;

            // Previous root becomes the new root's left child
            newRoot.Left = oldRoot;

            return newRoot;
        }

        private AVLTreeNode<TValueType> RightLeftRotation(AVLTreeNode<TValueType> node)
        {
            // Left Rotate the left child
            var newRoot = this.LeftRotation(node.Left);

            // Right Rotate the "rotated" tree
            newRoot = this.RightRotation(newRoot);

            return newRoot;
        }

        private AVLTreeNode<TValueType> LeftRightRotation(AVLTreeNode<TValueType> node)
        {
            // Right Rotate the right child
            var newRoot = this.RightRotation(node.Right);

            // Left Rotate the "rotated" tree
            newRoot = this.LeftRotation(newRoot);

            return newRoot;
        }

        public class FindNodeByValueResult
        {
            public AVLTreeNode<TValueType> Node { get; set; }
            public AVLTreeNode<TValueType> Parent { get; set; }
        }
    }

    public class AVLTreeNode<TValueType>
        where TValueType : System.IComparable<TValueType>
    {
        public TValueType Value { get; set; }

        public AVLTreeNode<TValueType> Left { get; set; }

        public AVLTreeNode<TValueType> Right { get; set; }

        public TreeBalance TreeBalance
        {
            get
            {
                TreeBalance result = TreeBalance.Balanced;

                var leftHeight = this.CalculateTreeHeight(this.Left);
                var rightHeight = this.CalculateTreeHeight(this.Right);

                if ((leftHeight - rightHeight) > 1)
                {
                    result = TreeBalance.LeftHeavy;
                }
                else if ((rightHeight - leftHeight) > 1)
                {
                    result = TreeBalance.RightHeavy;
                }

                return result;
            }
        }

        public bool ReplaceChild(AVLTreeNode<TValueType> childToBeReplaced, AVLTreeNode<TValueType> newChild)
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

        private int CalculateTreeHeight(AVLTreeNode<TValueType> node)
        {
            return (node == null)
                ? 0
                : 1 + System.Math.Max(CalculateTreeHeight(node.Left), CalculateTreeHeight(node.Right));
        }
    }

    public enum TreeBalance
    {
        Balanced,
        LeftHeavy,
        RightHeavy
    }

}