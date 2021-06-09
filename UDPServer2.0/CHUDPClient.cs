using GameDevWare.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer2._0
{
	public class CHUDPClient
	{
        private IPEndPoint remoteEP;
		Socket socket;
		int bufferSize = 1024;

		public CHUDPClient()
		{
			socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
        }

		public void Server(string address, int port)
        {
			remoteEP = new IPEndPoint(IPAddress.Any, 0);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            StartReceiving();            
        }

        public void Client(string address, int port)
        {
			remoteEP = new IPEndPoint(IPAddress.Parse(address), port);
			//socket.Connect(IPAddress.Parse(address), port);
			StartReceiving();            
        }

		public void StartReceiving()
		{
			try
			{
				var aSyncArgs = new SocketAsyncEventArgs();
				aSyncArgs.SetBuffer(new byte[bufferSize], 0, bufferSize);
				aSyncArgs.RemoteEndPoint = remoteEP;
				aSyncArgs.Completed += ReceiveCompleted;

				if(!socket.ReceiveFromAsync(aSyncArgs))
				{
					Console.WriteLine("Faild");
				}
			}
			catch(Exception e)
			{

				Console.WriteLine("Faild" + e);
			}
		}

		private void ReceiveCompleted(object sender, SocketAsyncEventArgs e)
		{
			var stream = new MemoryStream(e.Buffer);
			var packet = MsgPack.Deserialize<Packet>(stream);

			Console.WriteLine(packet.Test);
		}

		public void Send(string s)
		{
			try
			{
				var aSyncArgs = new SocketAsyncEventArgs();
				aSyncArgs.RemoteEndPoint = remoteEP;

				var stream = new MemoryStream();
				var pack = new Packet()
				{
					Test = s
				};

				MsgPack.Serialize(pack, stream);
				
				var bytePack = stream.ToArray();
				aSyncArgs.SetBuffer(bytePack, 0, bytePack.Length);

				aSyncArgs.Completed += SendComplete;
				socket.SendToAsync(aSyncArgs);
			}
			catch(Exception e)
			{
				Console.WriteLine("Send failed " + e);
			}
		}

		private void SendComplete(object sender, SocketAsyncEventArgs e)
		{
			//throw new NotImplementedException();
		}
	}
}
