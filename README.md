# LAB 3 QUESTION ANSWERS

1). To test my program without manually going through the actual plays, I would create smaller dialogue files that have similar formatting 
to the files containing the play dialogue. Just like we had to set up test cases to test the WordCount method, I could follow a similar procedure on 
smaller dialogue files for which the expected result is known.

2). From my observations I noticed that the time taken to parse all the dialogue files and count words for the multithreaded case actually took longer than 
the single threaded case. I used the Stopwatch class to time the duration for both the multithreaded and singlethreaded which produced the following results:

      single thread time: 00:00:00.148977
      multi thread time: 00:00:00.303716
      
The multithreaded duration was essentially twice as long. I believe that multithreading has more benefits and provides better performance for very large data sets
and the files for this lab may simply just not have been large enough to realize the performance improvements of concurrency.

3). To treat the same name belonging to multiple different people scenarios, we need a way to generate unique key values for our dictionary. One method may be to first 
generate unique hash codes using a helper method to assign unique keys to each individual character. This hashing method may use some techniue to gaurantee uniqueness of keys 
by for example incorporating things such as date of the play into account when creating unique keys for the dictionary.
