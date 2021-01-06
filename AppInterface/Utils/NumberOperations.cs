using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AppInterface.Algorithms
{
    public class NumberOperations
    {
        private const String BIN_PREFIX = "0b";
        private const String HEX_PREFIX = "0x";

        private const int DEC = 10;
        private const int BIN = 2;
        private const int HEX = 16;

        private NumberStyles style;
        private CultureInfo culture;

        public NumberOperations()
        {
            style = NumberStyles.AllowDecimalPoint;
            culture = CultureInfo.InvariantCulture;
        }

        public static String DecToHex(String dec)
        {
            return HEX_PREFIX + Convert.ToString(Convert.ToInt32(dec, DEC), HEX);
        }

        public static String DecToBin(String dec)
        {
            return BIN_PREFIX + Convert.ToString(Convert.ToInt32(dec, DEC), BIN);
        }
        public String UnwrapNumber(String value)
        {
            long integer;
            decimal dec;

            if (long.TryParse(value, style, culture, out long intRes))
            {
                return BreakIntoNumbers(intRes.ToString());
            }
            else if (decimal.TryParse(value, style, culture, out decimal decRes))
            {
                long floor = (long)Math.Floor(decRes);
                if (floor == 0)
                {
                    integer = 1;
                    dec = decRes - integer;
                    return 1 + " + " + ConvertToCommonFraction(dec);
                }
                else
                {
                    integer = floor;
                    dec = decRes - floor;
                    return BreakIntoNumbers(integer.ToString()) + " + " + ConvertToCommonFraction(dec);
                }
            }
            return value;
        }

        public decimal ExpressionParser(String expression)
        {
            expression = expression.Replace(" ", "");
            while (expression.Contains("(") || expression.Contains(")"))
            {
                String value = Regex.Match(expression, @"(\()([^)]+)(\))").Value;
                while (value.LastIndexOf('(') != 0) value = value.Substring(1);
                while (value.IndexOf(')') != (value.Length - 1)) value = value.Substring(0, value.Length - 2);
                expression = expression.Replace(value, Parse(value).ToString());
            }
            return Parse(expression);
        }

        private decimal Parse(String exp)
        {
            exp = exp.Replace("(", "").Replace(")", "");
            char op;
            if (exp.Contains("*")) op = '*';
            else if (exp.Contains("/")) op = '/';
            else if (exp.Contains("+")) op = '+';
            else if (exp.Contains("-")) op = '-';
            else return decimal.Parse(exp);

            if (exp.Substring(0, 1).Equals("-"))
            {
                exp = exp.Substring(1);
                return ApplyOp(op, -decimal.Parse(exp.Split(op)[0], culture), decimal.Parse(exp.Split(op)[1], culture));
            }
            else
            {
                return ApplyOp(op, decimal.Parse(exp.Split(op)[0], culture), decimal.Parse(exp.Split(op)[1], culture));
            }
        }

        private decimal ApplyOp(char op, decimal a, decimal b)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                    {
                        throw new System.NotSupportedException("Cannot divide by zero");
                    }
                    return a / b;
            }
            return 0;
        }

        private String ConvertToCommonFraction(decimal number)
        {
            long nominator;
            long denominator = 10;
            while((number * denominator) % 1 != 0)
            {
                denominator *= 10;
            }
            nominator = (long)(number * denominator);
            long gcd = GreatestCommonDivisor(Math.Abs(nominator), denominator);
            nominator /= gcd;
            denominator /= gcd;

            return ("(" + nominator + " / " + denominator + ")");
        }

        private long GreatestCommonDivisor(long a, long b)
        {
            while (a != b)
            {
                if (a > b)
                    a -= b;
                else
                    b -= a;
            }

            return a;
        }

        private String BreakIntoNumbers(String number)
        {
            long numAsInt = long.Parse(number);
            int randomNumber = RandomUtils.GenerateLittleInt();
            Random random = new Random();
            switch (random.Next(0, 4))
            {
                case 0:
                    {
                        if(random.Next(0,4) == 0)
                        {
                            return "( " + BreakIntoNumbers((numAsInt - randomNumber).ToString()) + " + " + BreakIntoNumbers((randomNumber).ToString()) + " )";
                        } 
                        else
                        {
                            return "( " + (numAsInt - randomNumber) + " + " + (randomNumber) + " )";
                        }
                    }
                case 1:
                    {
                        if (random.Next(0, 4) == 0)
                        {
                            return "( " + BreakIntoNumbers((numAsInt + randomNumber).ToString()) + " - " + BreakIntoNumbers((randomNumber).ToString()) + " )";
                        }
                        else
                        {
                            return "( " + (numAsInt + randomNumber) + " - " + (randomNumber) + " )";
                        }
                    }
                case 2:
                    {
                        List<long> dividers = new List<long>();
                        long divider = Math.Abs(numAsInt) - 1;
                        while(divider > 0)
                        {
                            if (numAsInt % divider == 0) dividers.Add(divider);
                            divider--;
                        }

                        divider = (dividers.Count == 0) ? Math.Abs(numAsInt) : dividers[random.Next(0, dividers.Count)];
                        
                        if(divider == 0)
                        {
                            return BreakIntoNumbers(number);
                        }

                        if (random.Next(0, 4) == 0)
                        {
                            return "( " + BreakIntoNumbers((numAsInt / divider).ToString()) + " * " + BreakIntoNumbers((divider).ToString()) + " )";
                        }
                        else
                        {
                            return "( " + (numAsInt / divider) + " * " + (divider) + " )";
                        }
                    }
                case 3:
                    {
                        if (random.Next(0, 4) == 0)
                        {
                            return "( " + BreakIntoNumbers((numAsInt * randomNumber).ToString()) + " / " + BreakIntoNumbers((randomNumber).ToString()) + " )";
                        }
                        else
                        {
                            return "( " + (numAsInt * randomNumber) + " / " + (randomNumber) + " )";
                        }
                    }
                default:
                    return number;
            }
        }

    }
}
