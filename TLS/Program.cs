using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLS
{
    class Program
    {
        static void Main(string[] args)
        {
            TLSClient tlsClient = new TLSClient();
            tlsClient.ConnectToServer();
            tlsClient.sendClientHello();
            tlsClient.ReceiveMessage();
        }
    }
}
