A binary tree is a tree that can have at most 2 childred (left and right)

In binary search tree:
  - Smaller values on the left
  - Larger values on the right
  - Remove has 3 cases and for each of the removal logic is different

Binary tree can be traversed in the following ways:
  - Pre Order -  action(), visit(left), visit(right)
  - In Order - visit(left), action(), visit(right)
  - Post Order - visit(left), visit(right), action()

For binary search tree, In Order traversal will yield elements in the sorted order
Post order traversal of a tree will return childred and then a parent, this can be used to return dependencies first and and parent -last



http://visualgo.net/bst.html