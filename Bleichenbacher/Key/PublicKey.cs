using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bleichenbacher.Key
{
    class PublicKey
    {
        public PublicKey()
        {
            n = Extentions.p * Extentions.q;
            e = Extentions.e;
            k = 0;
            BigInteger tmp = n;
            while (tmp != 0)
            {
                k++;
                tmp /= 256;
            }
        }



        private BigInteger n;
        private BigInteger e;
        private BigInteger k;



        public BigInteger GetN()
        {
            return n;
        }

        public BigInteger getE()
        {
            return e;
        }

        public BigInteger getK()
        {
            return k;
        }
    }
}
