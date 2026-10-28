using System.Net;
using System.Net.Sockets;
using System.Text;


var client = new Socket
(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
);

var serverPort = 5000;
IPEndPoint serverAdress = new IPEndPoint(IPAddress.Loopback, serverPort);

client.Connect(serverAdress);

Console.WriteLine("[i] Leave your message with blank spaces, if you don't want to send any");
while (true)
{
    byte[] buffer = new byte[1024];

    Console.Write("Your message => ");
    string message = Console.ReadLine()!;

    if (string.IsNullOrEmpty(message))
    {
        client.Shutdown(SocketShutdown.Send);

        var receivedLength = client.Receive(buffer);
        var overallMessages = Encoding.UTF8.GetString(buffer, 0, receivedLength);
        Console.WriteLine("OVERALL MESSAGES : ");
        Console.WriteLine(overallMessages);
        break;
    }

    var messageData = Encoding.UTF8.GetBytes(message);
    client.Send(messageData);
}
client.Close();