Merge sort
Original array is recursively splitted and then - "Merged" back 

https://en.wikipedia.org/wiki/Merge_sort

Divide and conquer algorithm
Recursively splits array in half till array length is 1 and then reconstructs the array in sorted order
Appropriate for large data sets
Very predictable algorithm. Always O(n log n), because it needs to split data

Performance:
  Worst case performance - O(n log n)
  Average performance - O(n log n)
  Best case performance - O(n log n)

Space required:
  Merge can be performed "in place" - O(n)
  But usually additional space is allocated for each splitted array, therefore - multiple additional allocations are required - 
  O(n) + O(n) for temp space 


*where: n - number of items to be sorted 