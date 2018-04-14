using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TLS
{
    class TLSClient
    {
        private const UInt16 TLS_1_2_VERSION = 0x0303;
        private const byte CONTENT_TYPE_HANDSHAKE = 0x16;
        private const byte HANDSHAKE_TYPE_CLIENT_HELLO = 0x01;
        private const UInt16 TLS_RSA_WITH_AES_128_CBC_SHA = 0x002F;
        private const UInt16 RECORD_LENGTH = 0x002f;
        private const int CLIENT_HELLO_MESSAGE_LENGTH = 0x00002b;
        private const byte RANDOM_LENGTH = 32;
        private const UInt16 COUNT_CIPHER_SUITES = 0x0001;
        private const UInt16 EXTENSIONS_LENGTH = 0x0000;



        private string Message;
        private Socket client;
        


        public TLSClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void sendClientHello()
        {
            Message += CONTENT_TYPE_HANDSHAKE;
            Message += TLS_1_2_VERSION;
            Message += RECORD_LENGTH;
            Message += HANDSHAKE_TYPE_CLIENT_HELLO;
            Message += CLIENT_HELLO_MESSAGE_LENGTH;
            Message += TLS_1_2_VERSION;

            Random randomGenerator = new Random();
            for (int i = 0; i < RANDOM_LENGTH; i++)
            {
                Message += (randomGenerator.Next() / 256).ToString("X");
            }
            Message += "0x00";
            Message += COUNT_CIPHER_SUITES;
            Message += TLS_RSA_WITH_AES_128_CBC_SHA;
            Message += EXTENSIONS_LENGTH;
        
        }

        public void ConnectToServer()
        {
            client.Connect("google.com", 443);
        }
    }
}
