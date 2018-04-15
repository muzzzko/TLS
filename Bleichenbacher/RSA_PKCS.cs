using System;
using Bleichenbacher.Key;
using Bleichenbacher.Exceptions;
using System.Numerics;

namespace Bleichenbacher
{
    class RSA_PKCS
    {
        public string Encode(PublicKey key, string message)
        {
            string cipherText;
            int mLen = message.Length;
            if (mLen > key.getK() + 11)
            {
                throw new MessageTooLongException();
            }

            BigInteger psLen = key.getK() - mLen - 3;
            string ps = "";
            char randomByte;
            Random random = new Random(1);
            for(int i = 0; i < psLen; i++)
            {
                while ((randomByte = Convert.ToChar(random.Next() % 256)) == '0');
                ps += randomByte;
            }

            string em = "02" + ps + "0" + message;
            BigInteger m = OS2IP(em);
            m = PowModul(m, key.getE(), key.GetN());
            cipherText = I2OSP(m, key.getK());
            return cipherText;
        }

        public string Decode(PrivateKey key, string cipherText)
        {
            int cipherTextLength = cipherText.Length;
            if (cipherTextLength != key.getK())
            {
                throw new ArgumentException("Decription error");
            }

            BigInteger c = OS2IP(cipherText);
            c = PowModul(c, key.getD(), key.GetN());
            string message = I2OSP(c, key.getK());
            if (message[0] != '0' && message[1] != '2')
            {
                throw new DecryptionErrorExecption("First two bytes is wrong");
            }
            int messageLength = message.Length;
            int count = 2;
            while (count < messageLength)
            {
                if (message[count] == '0')
                {
                    if (count < 10)
                    {
                        throw new DecryptionErrorExecption("PS length is less 8");
                    }
                    return message.Substring(count+1);
                }
                count++;
            }

            throw new DecryptionErrorExecption("No zero byte");
        }



        private BigInteger PowModul(BigInteger basis, BigInteger degree, BigInteger modul)
        {
            BigInteger item = basis;
            BigInteger result = 1;
            while (degree != 0 && item != 1)
            {
                if (degree % 2 == 1)
                {
                    result = result * item % modul;
                }
                item = item * item % modul;
                degree /= 2;
            }
            return result;
        }

        private string I2OSP(BigInteger x, BigInteger xLen)
        {
            string octetItem = "";
            while (xLen > 0)
            {
                octetItem = (char)(x % 256) + octetItem;
                x /= 256;
                xLen--; 
            }
            return octetItem;
        }

        private BigInteger OS2IP(string x)
        {
            BigInteger item = 0;
            int xLen = x.Length - 1;
            BigInteger octetDegree = 1;
            for (int i = xLen; i >= 0; i--)
            {
                item += x[i] * octetDegree;
                octetDegree *= 256;
            }
            return item;
        }
    }
}
