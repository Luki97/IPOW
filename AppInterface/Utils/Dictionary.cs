using System;

namespace AppInterface.Utils
{
    class Dictionary
    {
        private static String[] words = 
        { "person", "bird", "dog", "cat", "animal"};

        private static int index = 0;

        public static String GetNext()
        {
            String word = "";
            int tempIndex = index;
            while(tempIndex >= words.Length)
            {
                word += words[tempIndex % words.Length];
                tempIndex -= words.Length;
            }
            word += words[tempIndex];
            index++;
            return word;
        }
    }
}
