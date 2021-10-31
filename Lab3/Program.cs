using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Lab3Q1
{
    class Program
    {
        static int Main(string[] args)
        {
          // map and mutex for thread safety
          Mutex mutex = new Mutex();
          Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();
          Dictionary<string, int> wcountsMultiThread = new Dictionary<string, int>(); //new dictionary for multithreaded case


          var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
           };
           //=============================================================
           // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
           //=============================================================
           Stopwatch singleThreadWatch = new Stopwatch();
           singleThreadWatch.Start();
           foreach(string myFile in filenames)
            {
                HelperFunctions.CountCharacterWords(myFile, mutex, wcountsSingleThread);
            }
           singleThreadWatch.Stop();
           TimeSpan ts1 = singleThreadWatch.Elapsed;
           HelperFunctions.PrintListofTuples(HelperFunctions.SortCharactersByWordcount(wcountsSingleThread));
           Console.WriteLine( "SingleThread is Done!");
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================

            int numFiles = 10;
            Thread[] threads = new Thread[numFiles];
            int i = 0;

            Stopwatch multiThreadWatch = new Stopwatch();
            multiThreadWatch.Start();
            foreach (string myFile in filenames)//traverse over files and create a new thread to count character counts of each file
            {
                threads[i] = new Thread(() => HelperFunctions.CountCharacterWords(myFile, mutex, wcountsMultiThread));
                threads[i].Start();
                i += 1;
            }

            for(int j = 0; j < numFiles; j++) //joining all new threads spun
            {
                threads[j].Join();
            }
            multiThreadWatch.Stop();
            TimeSpan ts2 = multiThreadWatch.Elapsed;
            HelperFunctions.PrintListofTuples(HelperFunctions.SortCharactersByWordcount(wcountsMultiThread));

            Console.WriteLine( "MultiThread is Done!");
            Console.WriteLine("single thread time: {0}", ts1);
            Console.WriteLine("multi thread time: {0}", ts2);
            return 0;
        }
    }
}
