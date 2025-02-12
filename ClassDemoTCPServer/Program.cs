// See https://aka.ms/new-console-template for more information
using ClassDemoTCPServer;

Console.WriteLine("Hello, World!");


Server server = new Server(21);
server.Start();

Console.ReadKey();