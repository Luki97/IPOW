using System;
using System.Collections.Generic;
using System.Text;

namespace AppInterface.Algorithms
{
    class NumericTypesUtils
    {
        private const String BIN_PREFIX = "0b";
        private const String HEX_PREFIX = "0x";

        private const int DEC = 10;
        private const int BIN = 2;
        private const int HEX = 16;


        public static String DecToHex(String dec)
        {
            return HEX_PREFIX + Convert.ToString(Convert.ToInt32(dec, DEC), HEX);
        }

        public static String DecToBin(String dec)
        {
            return BIN_PREFIX + Convert.ToString(Convert.ToInt32(dec, DEC), BIN);
        }

    }
}
