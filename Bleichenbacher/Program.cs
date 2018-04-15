using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bleichenbacher.Key;
using System.Numerics;
using System.Collections;

namespace Bleichenbacher
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA_PKCS rsa = new RSA_PKCS();
            PublicKey publicKey = new PublicKey();
            PrivateKey privateKey = new PrivateKey();
            string message = "hello, Egor";
            string cipherText = rsa.Encode(publicKey, message);
            message = rsa.Decode(privateKey, cipherText);
        }
    }
}
