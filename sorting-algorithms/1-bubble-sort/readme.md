Bubble sort
For each item perform a "swap" with the next item if the "next" item is smaller

https://en.wikipedia.org/wiki/Bubble_sort

Uses multiple passes over the data set in order to sort it.
Sorts it "in place" no additional allocations are needed
Bad for large data sets
Uses multiple passes to sort the data set



Performance:
  Worst case performance - O(n^2)
  Average performance - O(n^2)
  Best case performance - O(n); Happens when a data set is already sorted or nearly sorted

Space required:
  All operations performed in the original data set. No additionally allocated space is required.
  Therefore, space requirement is O(n) + O(1) for temp space 


*where: n - number of items to be sorted 


http://visualgo.net/sorting.html (select  BUBBLE from tabs)