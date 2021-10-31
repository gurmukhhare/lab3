using System;
using System.Collections.Generic;
namespace Lab3Q1
{
    public class WordCountTester
    {
        public static void Main()
        {
            //2D array to hold all test cases along with expected results
            object[,] testCases = new object[8, 3] { {"",0,0}, {"",5,0}, {"a b c d",-1,0},
                {"a b c d",1,3}, {"a b c d",6,1}, {"abcd", 0, 1}, {" ab cd ",0,2}, {"ab  cd",0,2} };
            int numCases = 8;
            //loop through each test case and run tester method to compare expected and computed results
            for(int i = 0; i < numCases; i++)
            {
                string line = testCases[i, 0].ToString();
                int startIdx = (int)testCases[i, 1];
                int expectedResults = (int)testCases[i, 2];
                try
                {

                    //=================================================
                    // Implement your tests here. Check all the edge case scenarios.
                    // Create a large list which iterates over WCTester
                    //=================================================

                    WCTester(line, startIdx, expectedResults);
                    Console.WriteLine("unit test for testcase {0} passed", i);

                }
                catch (UnitTestException e)
                {
                    Console.WriteLine(e);
                }

            }

        }


        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */
          static void WCTester(string line, int start_idx, int expected) {

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================
            int result = HelperFunctions.WordCount(line, start_idx);
            if (result != expected) {
              throw new Lab3Q1.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }

           }
    }
}
