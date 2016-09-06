using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace Client_Renuka
{
    class Program
    {
        static void Main(string[] args)
        {

            Client chat = new Client();
            Console.WriteLine("Welcome to your Chat Room connection!");
            TcpClient client = new TcpClient();
            Thread connectThread = new Thread(chat.ConnectToHost);
            connectThread.Start();
            string ipAddress = chat.GetIPConnectionInfo();
            int hostport = chat.GetPortConnectionInfo();
            client.Connect(IPAddress.Parse(ipAddress), hostport);


            while (client.Connected == true)
            {
                try
                {
                    NetworkStream stream = client.GetStream();

                    int numberOfBytesReceived = 0;
                    if (numberOfBytesReceived != 0)
                    {

                        string responseStr = "";
                        numberOfBytesReceived = stream.Read(chat.clientBuffer, 0, chat.clientBuffer.Length);
                        responseStr = Encoding.ASCII.GetString(chat.clientBuffer);
                        Console.WriteLine(responseStr);

                    }
                    else
                    {
                        Thread startMessageThread = new Thread(chat.StartMessage);
                        startMessageThread.Start();

                        string messageStr = Console.ReadLine();
                        byte[] messageByt = Encoding.ASCII.GetBytes(messageStr);

                        stream.Write(messageByt, 0, messageByt.Length);
                        chat.clientBuffer = new Byte[256];

                    }
                }

                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
            }

            try
            {

                client.Connect(IPAddress.Parse("10.2.20.21"), 9218);

                Console.WriteLine("What message would you like to send in order to connect?");
                string messageString = Console.ReadLine();
                byte[] messageByte = chat.ToBytes(messageString);

                NetworkStream stream = client.GetStream();
                stream.Write(messageByte, 0, messageByte.Length);
                Console.WriteLine("Message sent!");

                chat.clientBuffer = new Byte[100];

                int numberOfBytesReceived = 1;
                while (numberOfBytesReceived != 0)
                {
                    string responseStr = "";
                    numberOfBytesReceived = stream.Read(chat.clientBuffer, 0, chat.clientBuffer.Length);
                    responseStr = System.Text.Encoding.ASCII.GetString(chat.clientBuffer, 0, numberOfBytesReceived);
                    Console.WriteLine("Received message:");
                    Console.WriteLine(responseStr);

                    Console.WriteLine("Your message:");
                    string messageStr = Console.ReadLine();
                    byte[] messageByt = chat.ToBytes(messageStr);
                    stream.Write(messageByt, 0, messageByt.Length);
                    Console.WriteLine("Message sent!");
                }

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }


        }
    }
}