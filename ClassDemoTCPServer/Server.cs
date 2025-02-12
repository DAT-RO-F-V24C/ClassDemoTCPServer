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
        private readonly int PORTNUMMER = 7; // ekko server
        private readonly string NAME = "Dummy";


        public Server(int port = 7, string name = "Dummy")
        {
            PORTNUMMER = port;
            NAME = name;
        }

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

            // EchoProtocol(reader, writer);

            MathProtocol(reader, writer);

            /*
             * Slut
             */

            socket?.Close();
        }

        private static void EchoProtocol(StreamReader reader, StreamWriter writer)
        {
            string? line = reader.ReadLine();
            Console.WriteLine("line from client :: " + line);
            
            writer.WriteLine(line); // echo line
        }

        private static void MathProtocol(StreamReader reader, StreamWriter writer)
        {
            string? str = reader.ReadLine();
            // todo - fejl ved null string
            string[] strSplittet = str.Split();

            try
            {
                string method = strSplittet[0];
                int tal1 = int.Parse(strSplittet[1]);
                int tal2 = int.Parse(strSplittet[2]);
            

            string returnLine = "";

            switch (method.ToLower())
            {
                case "add":
                    {
                            returnLine = "Resultat " + (tal1 + tal2);
                            break;
                    }
                case "mul":
                    {
                        returnLine = "Resultat " + (tal1 * tal2);
                        break;
                    }
                case "sub":
                    {
                        returnLine = "Resultat " + (tal1 - tal2);
                        break;
                    }
                case "div":
                    {
                        returnLine = "Resultat " + (tal1 / tal2);
                        break;
                    }
                default:
                        throw new ArgumentException("No such method");
                }

                writer.WriteLine(returnLine);
            }
            catch (Exception ex)
            {
                writer.WriteLine("Error \r\n" + ex.Message);
            }
        }
    }
}
