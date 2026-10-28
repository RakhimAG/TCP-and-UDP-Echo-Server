using System.Net;
using System.Net.Sockets;
using System.Text;

// UDP SERVER

var serverSocket = new Socket
(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
);

var port = 5001;
IPEndPoint serverAdress = new IPEndPoint(IPAddress.Loopback, port);

serverSocket.Bind(serverAdress);

byte[] buffer = new byte[1024];

while (true)
{
    EndPoint receivedFrom = new IPEndPoint(IPAddress.Any, 0);

    Console.WriteLine("Waiting to receive message from UDP client..");
    int receivedLength = serverSocket.ReceiveFrom(buffer, ref receivedFrom);
    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedLength);

    Console.WriteLine($"Received from UDP client => {receivedMessage}");

    Console.WriteLine("Sending message back to client..");
    serverSocket.SendTo(buffer, receivedFrom);
}