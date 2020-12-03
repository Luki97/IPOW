using System;
using System.Text;

namespace AppInterface.Generators
{
    readonly struct IntToBitExpression
    {
        private const int BIN = 2;
        private const int REPLACE_LOWER_BOUND = 1;
        private const int REPLACE_UPPER_BOUND = 4;
        private const char AND = '&';
        private const char OR  = '|';
        private const char XOR = '^';
        private readonly static (int, int)[] AND_ZERO_OPTIONS = new (int, int)[] { (1, 0), (0, 0), (0, 1) };
        private readonly static (int, int)[] AND_ONE_OPTIONS  = new (int, int)[] { (1, 1) };
        private readonly static (int, int)[] OR_ZERO_OPTIONS  = new (int, int)[] { (0, 0) };
        private readonly static (int, int)[] OR_ONE_OPTIONS   = new (int, int)[] { (1, 0), (1, 1), (0, 1) };
        private readonly static (int, int)[] XOR_ZERO_OPTIONS = new (int, int)[] { (0, 0), (1, 1) };
        private readonly static (int, int)[] XOR_ONE_OPTIONS  = new (int, int)[] { (1, 0), (0, 1) };
        private readonly static Random RAND = new Random();

        public static string Replace(int token)
        {
            var binary = Convert.ToString(token, BIN);
            return RAND.Next(REPLACE_LOWER_BOUND, REPLACE_UPPER_BOUND) switch
            {
                int rnd when rnd == 1 => ToAndOperation(binary),
                int rnd when rnd == 2 => ToOrOperation(binary),
                int rnd when rnd == 3 => ToXorOperation(binary),
                _ => ToXorOperation(binary)
            };
        }

        public static string ToAndOperation(string binary)
        {
            return ConvertToExpression(binary, AND, AND_ZERO_OPTIONS, AND_ONE_OPTIONS);
        }

        public static string ToOrOperation(string binary)
        {
            return ConvertToExpression(binary, OR, OR_ZERO_OPTIONS, OR_ONE_OPTIONS);
        }

        public static string ToXorOperation(string binary)
        {
            return ConvertToExpression(binary, XOR, XOR_ZERO_OPTIONS, XOR_ONE_OPTIONS);
        }

        private static string ConvertToExpression(string binary, char binaryOperator, (int, int)[] zerioOptions, (int, int)[] oneOptions)
        {
            StringBuilder left = new StringBuilder();
            StringBuilder right = new StringBuilder();

            foreach (var c in binary.ToCharArray())
            {
                (int forLeft, int forRight) = c switch
                {
                    '0' => GetRandomOption(zerioOptions),
                    '1' => GetRandomOption(oneOptions),
                    _ => throw new ArgumentException("It's not binary!")
                };

                left.Append(forLeft);
                right.Append(forRight);
            }

            return PullTogether(binaryOperator, Convert.ToInt32(left.ToString(), BIN), Convert.ToInt32(right.ToString(), BIN));
        }

        private static (int, int) GetRandomOption((int, int)[] options)
        {
            return options[RAND.Next(0, options.Length)];
        }

        private static string PullTogether(char binaryOperator, int left, int right)
        {
            StringBuilder result = new StringBuilder();

            result.Append('(');
            result.Append(left);
            result.Append(' ');
            result.Append(binaryOperator);
            result.Append(' ');
            result.Append(right);
            result.Append(')');

            return result.ToString();
        }
    }
}
