using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AppInterface.Algorithms
{
    class NumbersExtension
    {

        public ExpressionSyntax ExtendNumber(LiteralExpressionSyntax node)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.InvariantCulture;

            long integer;
            decimal dec;

            String value = node.Token.ValueText;


            if (long.TryParse(value, style, culture, out long intRes))
            {
                List<String> newNumbers = new List<String>();
                foreach(String num in PrimeFactors(intRes))
                {
                    newNumbers.Add(BreakIntoNumbers(num));
                }
                return SyntaxFactory.ParseExpression(String.Join(" * ", newNumbers));
            }
            else if (decimal.TryParse(value, style, culture, out decimal decRes))
            {
                long floor = (long)Math.Floor(decRes);
                if (floor == 0)
                {
                    integer = 1;
                    dec = decRes - integer;
                    return SyntaxFactory.ParseExpression(1 + " + " + ConvertToCommonFraction(dec));
                }
                else
                {
                    integer = floor;
                    dec = decRes - floor;
                    List<String> newNumbers = new List<String>();
                    foreach (String num in PrimeFactors(integer))
                    {
                        newNumbers.Add(BreakIntoNumbers(num));
                    }
                    return SyntaxFactory.ParseExpression(String.Join(" * ", newNumbers) + " + " + ConvertToCommonFraction(dec));
                }
            }
            return SyntaxFactory.ParseExpression(value);
        }

        private List<String> PrimeFactors(long number)
        {
            List<String> primeFactors = new List<String>();
            long x, i, e;
            x = number;

            i = 2;
            e = (long)Math.Sqrt(x);
            while (i <= e)
            {
                while ((x % i) == 0)
                {
                    x /= i;
                    e = (long)Math.Sqrt(x);
                    primeFactors.Add(i.ToString());
                }
                i++;
            }
            if (x > 1) primeFactors.Add(x.ToString());

            return primeFactors;
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
                        if(random.Next(0,2) == 0)
                        {
                            return "( " + (numAsInt - randomNumber) + " + " + (randomNumber) + " )";
                        } 
                        else
                        {
                            return "( " + BreakIntoNumbers((numAsInt - randomNumber).ToString()) + " + " + BreakIntoNumbers((randomNumber).ToString()) + " )";
                        }
                        
                    }
                case 1:
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            return "( " + (numAsInt + randomNumber) + " - " + (randomNumber) + " )";
                        }
                        else
                        {
                            return "( " + BreakIntoNumbers((numAsInt + randomNumber).ToString()) + " - " + BreakIntoNumbers((randomNumber).ToString()) + " )";
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
                        if(dividers.Count == 0)
                        {
                            divider = Math.Abs(numAsInt);
                        }
                        else
                        {
                            divider = dividers[random.Next(0, dividers.Count)];
                        }

                        if (random.Next(0, 2) == 0)
                        {
                            return "( " + (numAsInt / divider) + " * " + (divider) + " )";
                        }
                        else
                        {
                            return "( " + BreakIntoNumbers((numAsInt / divider).ToString()) + " * " + BreakIntoNumbers((divider).ToString()) + " )";
                        }
                    }
                case 3:
                    {
                        if (random.Next(0, 2) == 0)
                        {
                            return "( " + (numAsInt * randomNumber) + " / " + (randomNumber) + " )";
                        }
                        else
                        {
                            return "( " + BreakIntoNumbers((numAsInt * randomNumber).ToString()) + " * " + BreakIntoNumbers((randomNumber).ToString()) + " )";
                        }
                    }
                default:
                    return number;
            }
        }

    }
}
