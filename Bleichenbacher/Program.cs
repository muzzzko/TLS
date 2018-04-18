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
            PublicKey publicKey = new PublicKey();
            PrivateKey privateKey = new PrivateKey();
            RSA_PKCS rsa = new RSA_PKCS(privateKey);    
            string message = "hello, Egor";
            string cipherText = rsa.Encode(publicKey, message);

            Attack attack = new Attack(rsa, publicKey, privateKey);
            attack.Start(cipherText);
        }
    }
}
