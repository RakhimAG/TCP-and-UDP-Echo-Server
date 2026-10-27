

using System.Net;
using System.Net.Sockets;
using System.Text;



var serverSocket = new Socket
(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
);

var port = 5000;
IPEndPoint serverAdress = new IPEndPoint(IPAddress.Loopback, port);

serverSocket.Bind(serverAdress);
serverSocket.Listen();

Console.WriteLine("WAITING FOR CLIENT TO CONNECT...");
var client = serverSocket.Accept();
Console.WriteLine("CLIENT CONNECTED SUCCESSFULY");



var messages = new List<string>();
using var overallMessagesBytes = new MemoryStream();
byte[] buffer = new byte[1024];
while (true)
{
    int received = client.Receive(buffer);
    if (received == 0)
    {
        var result = overallMessagesBytes.ToArray();
        client.Send(result);
        client.Dispose();
        
        break;
    }
    overallMessagesBytes.Write(buffer, 0, received);
    string message = Encoding.UTF8.GetString(buffer, 0, received);
    messages.Add(message);
}



Console.WriteLine("OVERALL MESSAGES STORED IN SERVER: \n\n");
if (messages.Any())
{
    int i = 1;
    foreach (var message in messages)
    {
        Console.WriteLine($"{i}) {message}");
        i++;
    }
}
else
    Console.WriteLine("THERE WERE NO MESSAGES SENT FROM CLIENT :(");
