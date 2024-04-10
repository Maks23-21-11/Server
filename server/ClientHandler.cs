using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class ClientHandler
    {
        public TcpClient clientSocket;
        public void RunClient()
        {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writeStream = clientSocket.GetStream();

            string returnData = readerStream.ReadLine();
            string name = returnData;

            Console.WriteLine($"Welcome {name} to the server");

            while (true)
            {
                returnData = readerStream.ReadLine();
                if (returnData.IndexOf("QUIT") > -1)
                {
                    Console.WriteLine($"Goodbay {name}");
                    break;
                }

                Console.WriteLine($"{name} : {returnData}");
                returnData += "\r\n";
                byte[] dataWrite = Encoding.ASCII.GetBytes(returnData);

                writeStream.Write(dataWrite, 0, dataWrite.Length);
            }
            clientSocket.Close();
        }
    }
}
