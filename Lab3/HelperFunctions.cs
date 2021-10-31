using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace Lab3Q1
{
    public class HelperFunctions
    {
        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */
        public static int WordCount(string line, int start_idx)
        {
            // YOUR IMPLEMENTATION HERE
            if(line.Length == 0) //error checking for invalid indices or empty string
            {
                Console.WriteLine("Empty string entered");
                return 0;
            }
            int count = 0;
            if (start_idx < 0 || start_idx >= line.Length)
            {
                Console.WriteLine("Invalid index entered");
                return 0;
            }
            int i = start_idx;
            while (i < line.Length) //iterate through the string
            {
                if(line[i]!=' ') //if the current character is not an empty space, we have encountered a word
                {
                    count += 1; //increment counter
                    while (i < line.Length) //increment i until we finish current word, once we encounter whitespace we break inner while loop
                    {
                        if (line[i] != ' ')
                        {
                            i += 1;
                        }
                        else break; //break out of loop once whitespace encountered since current word has ended
                    }
                }
                else //just increment i if it was whitespace
                {
                    i += 1;
                }
            }
            return count;

        }


        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename,
                                 Mutex mutex,
                                 Dictionary<string, int> wcounts)
        {

            //===============================================
            //  IMPLEMENT THIS METHOD INCLUDING THREAD SAFETY
            //===============================================

             string line;  // for storing each line read from the file
             string character = "";  // empty character to start
             System.IO.StreamReader file = new System.IO.StreamReader(filename);

             while ((line = file.ReadLine()) != null)
             {
                //=================================================
                // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
                //=================================================

                // Is the line a dialogueLine?
                //    If yes, get the index and the character name.
                //      if index > 0 and character not empty
                //        get the word counts
                //          if the key exists, update the word counts
                //          else add a new key-value to the dictionary
                //    reset the character
                int index = IsDialogueLine(line, ref character);
                if (index>0 && character!="") //check if line is a dialogue
                {
                    int numWords = WordCount(line, index);
                    mutex.WaitOne();
                    if (wcounts.ContainsKey(character)) //check if dictionary already contains this character
                    {
                        wcounts[character] += numWords;
                    }
                    else
                    {
                        wcounts.Add(character, numWords); //add new key, character, if dictionary didn't contain
                    }
                    mutex.ReleaseMutex();
             
                }

            }
            // Close the file
            file.Close();
        }



        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int IsDialogueLine(string line, ref string character)
        {

            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return 0;
        }

        /**
         * Sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */
        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {

            // Implement sorting by word count here
            List<Tuple<int, string>> sortedByValueList = new List<Tuple<int, string>>();
            foreach(KeyValuePair<string,int> item in wordcount) //iterate over key-value pairs
            {
                sortedByValueList.Add(new Tuple<int, string>(item.Value, item.Key)); //add each pair to sorted list
            }
            sortedByValueList = sortedByValueList.OrderByDescending(t => t.Item1).ToList(); //sort list by count descending
            return sortedByValueList;

        }


        /**
         * Prints the List of Tuple<int, string>
         *
         * @param sortedList
         * @return Nothing
         */
        public static void PrintListofTuples(List<Tuple<int, string>> sortedList)
        {

          // Implement printing here
          foreach(var tup in sortedList)
            {
                Console.WriteLine("{0}:{1}", tup.Item2, tup.Item1);
            }
        }
    }
}
