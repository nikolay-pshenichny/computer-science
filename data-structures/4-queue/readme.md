A Queue is a First In, First Out (FIFO) collection



If a LinkedList is used as the internal storage, there will be some overhead, because we need to store Nodes and Pointers to elements
If an Array is used as the internal storage, less memory is required. And all data is "close" to each other


Array based implementation uses "wrapping" ie.:
  If there is an empty space at the beginning of the array (due to dequeued elements), 
  and the Tail reached the end of the array (ie. can't write any more elements to the end),
  then the Tail will be set to the beginning of the array in order to fill in the array addresses that are available



http://visualgo.net/list.html (select QUEUE from tabs)