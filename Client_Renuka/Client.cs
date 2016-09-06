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
    public class Client
    {

        public byte[] clientBuffer = new Byte[1024];
        public int hostPort;
        public IPAddress hostIPAddress;
        public TcpClient client;
        public NetworkStream stream;


        public Client()
        {
        }


        public void Start()
        {
            Console.WriteLine("You will be connected!");
        }

        public void StartMessage()
        {
            Console.WriteLine("Your message:");
        }
        public string GetIPConnectionInfo()
        {
            Console.WriteLine("Please enter the IP address of the host you wish to connect to, in the format 'w.x.y.z'.");
            string hostIPAddress = Console.ReadLine();
            return hostIPAddress;
        }

        public int GetPortConnectionInfo()
        {
            Console.WriteLine("Please enter the port number of the host you wish to connect to.");
            int hostPort = Convert.ToInt32(Console.ReadLine());
            return hostPort;
        }


        public void ConnectToHost()
        {
            string ipAddress = GetIPConnectionInfo();
            int hostport = GetPortConnectionInfo();
            client.Connect(IPAddress.Parse(ipAddress), hostport);
        }



        public byte[] ToBytes(string responseStr)
        {
            byte[] responseByt = Encoding.ASCII.GetBytes(responseStr);
            return responseByt;
        }

        public string ToString(byte[] messageByt)
        {
            string messageStr = System.Text.Encoding.ASCII.GetString(messageByt, 0, messageByt.Length);
            return messageStr;
        }














    }
}