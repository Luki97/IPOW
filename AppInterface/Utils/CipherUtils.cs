using System;
using System.Collections.Generic;
using System.Text;

namespace AppInterface.Utils
{
    class CipherUtils
    {
        public char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }


        public string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += Cipher(ch, key);

            return output + key;
        }

        public string Decipher(string input, int key)
        {
            String line = Encipher(input, 26 - key);
            return line.Substring(0, line.Length - 2);
        }
    }
}
