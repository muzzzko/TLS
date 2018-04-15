using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TLS
{
    class TLSClient
    {
        private const string TLS_1_2_VERSION = "0303";
        private const string CONTENT_TYPE_HANDSHAKE = "16";
        private const string HANDSHAKE_TYPE_CLIENT_HELLO = "01";
        private const string TLS_RSA_WITH_AES_128_CBC_SHA = "002F";
        private const string RECORD_LENGTH = "002f";
        private const string CLIENT_HELLO_MESSAGE_LENGTH = "00002b";
        private const byte RANDOM_LENGTH = 32;
        private const string COUNT_CIPHER_SUITES = "0001";
        private const string EXTENSIONS_LENGTH = "0000";



        private string Message;
        private NetworkStream client;
        private TcpClient TcpClient;




        public TLSClient()
        {
            TcpClient = new TcpClient();
        }

        public void sendClientHello()
        {
            Message += CONTENT_TYPE_HANDSHAKE;
            Message += TLS_1_2_VERSION;
            Message += RECORD_LENGTH;
            Message += HANDSHAKE_TYPE_CLIENT_HELLO;
            Message += CLIENT_HELLO_MESSAGE_LENGTH;
            Message += TLS_1_2_VERSION;

            int len = Message.Length;
            Random randomGenerator = new Random();
            string byteValue;
            for (int i = 0; i < RANDOM_LENGTH; i++)
            {
                byteValue = (randomGenerator.Next() % 256).ToString("X");
                byteValue = byteValue.Length == 1 ? '0' + byteValue : byteValue;
                Message += byteValue;
            }
            len = Message.Length;
            Message += "00";
            Message += COUNT_CIPHER_SUITES;
            Message += TLS_RSA_WITH_AES_128_CBC_SHA;
            Message += EXTENSIONS_LENGTH;

            byte[] pool = StringToByteArray(Message);

            client.Write(pool, 0, 50);
        }

        public void ConnectToServer()
        {
            TcpClient.Connect("vk.com", 443);
            client = TcpClient.GetStream();
        }

        public void ReceiveMessage()
        {
            byte[] buff = new byte[256];
            client.Read(buff, 0, 256);
            string str = Encoding.Unicode.GetString(buff);
        }



        private byte[] StringToByteArray(string message)
        {
            int messageLength = message.Length / 2;
            byte[] pool = new byte[messageLength];
            string byteValue;
            for(int i = 0; i < messageLength; i++)
            {
                byteValue = "";
                byteValue += message[i * 2];
                byteValue += message[i * 2 + 1];
                pool[i] = Convert.ToByte(byteValue, 16);
            }
            return pool;
        }
    }
}
