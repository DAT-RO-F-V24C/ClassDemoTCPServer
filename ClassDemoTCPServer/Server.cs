using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTCPServer
{
    public class Server
    {
        private const int PORTNUMMER = 7; // ekko server


        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, PORTNUMMER);
            server.Start();
            Console.WriteLine("Server started at port " + PORTNUMMER);

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoOneClient(tempSocket);
                });
            }




            //server.Stop();
        }

        private static void DoOneClient(TcpClient socket)
        {
            StreamReader reader = new StreamReader(socket.GetStream());
            StreamWriter writer = new StreamWriter(socket.GetStream());
            writer.AutoFlush = true;

            /*
             * Her begynder protokollen
             */

            // her ekko, hvor svaret bliver i store bogstaver
            string? line = reader.ReadLine();
            Console.WriteLine("line from client :: " + line);

            // gør et eller andet
            line = line?.ToUpper();

            writer.WriteLine(line);
            //writer.Flush();

            /*
             * Slut
             */

            socket?.Close();
        }
    }
}
