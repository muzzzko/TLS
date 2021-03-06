﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bleichenbacher
{
    class Extentions
    {
        public static readonly BigInteger p = 1000000000000037;
        public static readonly BigInteger q = StrToInt();
        public static readonly BigInteger e = 65535;
        public static BigInteger GCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            BigInteger x1, y1;
            BigInteger d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
        private static BigInteger StrToInt()
        {
            BigInteger res = 0;
            string q = "6122421090493547576937037317561418841225758554253106999";
            int qLen = q.Length;
            for(int i = 0; i < qLen; i++)
            {
                res = res * 10 + (q[i] - '0');
            }
            return res;
        }
    }
}
