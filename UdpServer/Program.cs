using System.Net;
using System.Net.Sockets;

var serverSocket = new Socket
(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
);

var port = 5001;
IPEndPoint serverAdress = new IPEndPoint(IPAddress.Loopback, port);

serverSocket.Bind(serverAdress);
