Hash Table - a structure that can map Keys to values.

Hash Table uses a hash function to calculate a hash of a key and quickly map it to a Bucket(or Slot) in which the associated Value can be found.


One of the Collision resolution approaches is to use LinkedLists as a data storage in each Bucket.
This way if more than one Key maps to the same Bucket, than key and its value will be stored in the underlying Linked List.

To avoid collisions and frequien lookups in linked lists, number of buckets should be increased if FillFactor reached some threshold.




https://en.wikipedia.org/wiki/Hash_table

http://visualgo.net/bst.html