using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    internal class Program
    {
        const int ECHO_PORT = 8080;
        public static int nClient = 0;
        static void Main(string[] args)
        {
            Console.WriteLine(GetLocalIPAddress());
            try 
            {
                TcpListener clientListener = new TcpListener(ECHO_PORT);
                clientListener.Start();

                Console.WriteLine("Waiting for connections...");

                while(nClient<5)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.clientSocket = client;

                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClient++;
                }
                clientListener.Stop();
            }
            catch(Exception e) 
            { 
                Console.WriteLine(e.ToString()); 
            }
            
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("no Network adapters with an IPv4 address in the system");
        }
    }
}
