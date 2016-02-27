(Max) Heap
http://visualgo.net/heap.html#

https://en.wikipedia.org/wiki/Heap_(data_structure)
https://en.wikipedia.org/wiki/Binary_heap

Binary Heap is a binary-tree-like structure with additional constraints
Binary Heap is a nearly complete binary tree (only last level may not be fully filled. The last level is filled from left to right)
All nodes in Max Binary Heap are greater than or equal to each of its children 

Binary Heap uses an array to store its elements:
 - First element in the array represents the Root node.
 - second and third elements represent left and right children of the Root node and so on.
 - all elements after mid-point are leafs

For each nodeIdx in a zero-based array:
  - parentIdx = floor(nodeIdx / 2)
  - leftChildIdx = (nodeIdx * 2) + 1
  - rightChildIdx = leftChildIdx + 1

