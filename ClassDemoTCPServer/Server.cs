﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoTCPServer
{
    public class Server
    {
        private const int PORTNUMMER = 17;


        public void Start()
        {
            TcpListener server = new TcpListener(PORTNUMMER);
            server.Start();
            

            TcpClient socket  = server.AcceptTcpClient();

            StreamReader reader = new StreamReader(socket.GetStream());
            StreamWriter writer = new StreamWriter(socket.GetStream());
            writer.AutoFlush = true;

            string line = reader.ReadLine();

            // gør et eller andet
            line = line.ToUpper();

            writer.WriteLine(line);
            //writer.Flush();

            socket?.Close();



        }
    }
}
