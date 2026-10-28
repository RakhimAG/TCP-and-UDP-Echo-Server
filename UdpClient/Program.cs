// UDP CLIENT

using System.Net;
using System.Net.Sockets;
using System.Text;

var client = new Socket
(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
);

var serverPort = 5001;
EndPoint serverAdress = new IPEndPoint(IPAddress.Loopback, serverPort);

byte[] buffer = new byte[1024];

while (true)
{
    Console.Clear();

    Console.Write("Enter your message =>");
    var message = Console.ReadLine()!;
    var messageInBytes = Encoding.UTF8.GetBytes(message);

    client.SendTo(messageInBytes, serverAdress);
    Console.WriteLine("Message was sent to server, waiting for answer..");

    client.Receive(buffer);
    var messageFromServeer = Encoding.UTF8.GetString(buffer);
    Console.WriteLine($"Received answer from server -> {messageFromServeer}");

    Console.WriteLine("Press [E] to continue...");
    while (Console.ReadKey().Key != ConsoleKey.E)
        continue;
}