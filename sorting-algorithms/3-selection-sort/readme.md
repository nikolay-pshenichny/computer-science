Selection sort
"Select" a minimal item and insert it to the beginning of the collection


https://en.wikipedia.org/wiki/Selection_sort

Finds a smallest item and moves it to the beginning of the collection
Sorts items "in place"
Bad for large data sets
Does a lot of comparisons even if the set is already sorted
Performs worse than Insertion sort


Performance:
  Worst case performance - O(n^2)
  Average performance - O(n^2)
  Best case performance - O(n^2)

Space required:
  All operations performed in the original data set. No additionally allocated space is required.
  Therefore, space requirement is O(n) + O(1) for temp space 


*where: n - number of items to be sorted 