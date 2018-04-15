using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bleichenbacher.Key
{
    class PrivateKey
    {
        public PrivateKey()
        {
            n = Extentions.p * Extentions.q;
            BigInteger y;
            BigInteger fi = (Extentions.p - 1) * (Extentions.q - 1);
            Extentions.GCD(Extentions.e, fi, out d, out y);
            d = (d + fi) % fi;
            k = 0;
            BigInteger res = Extentions.e * d % ((Extentions.p - 1) * (Extentions.q - 1));
            BigInteger tmp = n;
            while (tmp != 0)
            {
                k++;
                tmp /= 256;
            }
        }



        private BigInteger n;
        private BigInteger d;
        private BigInteger k;



        public BigInteger GetN()
        {
            return n;
        }

        public BigInteger getD()
        {
            return d;
        }

        public BigInteger getK()
        {
            return k;
        }
    }
}
