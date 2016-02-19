Insertion sort
For each item, find a spot to the left from it and "Insert" it there

https://en.wikipedia.org/wiki/Insertion_sort

More efficient than Bubble Sort and Selection Sort
Sorts data "in place". no additional allocations needed
Data on "the left" considered to be sorted, data on "the right" is unsorted
Bad for large data sets
"single pass" algorithm


Performance:
  Worst case performance - O(n^2)
  Average performance - O(n^2)
  Best case performance - O(n); Happens when a data set is already sorted or nearly sorted

Space required:
  All operations performed in the original data set. No additionally allocated space is required.
  Therefore, space requirement is O(n) + O(1) for temp space 


*where: n - number of items to be sorted 