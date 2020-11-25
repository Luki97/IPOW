using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppInterface.Algorithms
{
    class RandomUtils
    {
        static String SEMICOLN = ";";
        static String QUOTE = "\"";
        static String OPENING_CURLY_BRACKET = "{";
        static String CLOSING_CURLY_BRACKET = "}";

        public static String generateRandomAlphanumericString(int repetitionNumber)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrtsuvwxyzZ0123456789";
            return new string(Enumerable.Repeat(chars, repetitionNumber)
                .Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
        }
        public static String generateRandomString(int repetitionNumber)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrtsuvwxyz";
            return new string(Enumerable.Repeat(chars, repetitionNumber)
                .Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
        }
        public static int generateRandomInt()
        {
            var ints = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(ints).Next(0, 99999999);
        }
        public static int GenerateLittleInt()
        {
            var ints = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(ints).Next(0, 99);
        }
        public static String generateRandomUnusedSeparators()
        {
            return new string(Enumerable.Repeat(SEMICOLN, GenerateLittleInt())
               .Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
        }
        public static String generateUnusedStringDeclaration()
        {
            return "String " + generateRandomString(GenerateLittleInt()) + " = \"" + generateRandomString(GenerateLittleInt()) + QUOTE + SEMICOLN;
        }
        public static String generateUnusedintegerDeclaration()
        {
            return "int " + generateRandomString(GenerateLittleInt()) + "= " + generateRandomInt() + SEMICOLN;
        }
        public static String generateBlockWithSemicolns()
        {
            int bracketRepetition = generateRandomInt();
            return new string(Enumerable.Repeat(OPENING_CURLY_BRACKET, bracketRepetition).Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray()) +
                new string(Enumerable.Repeat(SEMICOLN, GenerateLittleInt()).Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray()) +
                new string(Enumerable.Repeat(CLOSING_CURLY_BRACKET, bracketRepetition).Select(s => s[new Random(Guid.NewGuid().GetHashCode()).Next(s.Length)]).ToArray());
        }
        public static String generateSingleLineComment()
        {
            return "//" + generateRandomAlphanumericString(GenerateLittleInt());
        }
    }
}
