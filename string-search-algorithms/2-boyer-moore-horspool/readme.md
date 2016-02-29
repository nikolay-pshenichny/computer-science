Boyer–Moore–Horspool


Simplification of Boyer–Moore algorithm
Has a PreProcessing stage
Preprocessing stage builds a table that contains lengths that are safe to skip if a bad match occurs
Doesn't create "Good Suffix" table as in classic Boyer–Moore
Appropriate for the general use

Performance:
 - Best case = O(N/M)
 - Worst case = O(N*M)


https://en.wikipedia.org/wiki/Boyer–Moore–Horspool_algorithm
