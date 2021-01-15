using System;
using System.Collections.Generic;
using System.Text;

namespace TestProgram5
{
    class TestProgram5
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