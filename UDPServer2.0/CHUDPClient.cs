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
		Socket client;
		int bufferSize = 1024;
		public CHUDPClient()
		{
            
            remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

			client = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
        }

		public void StartReceiving()
		{
			try
			{
				var aSyncArgs = new SocketAsyncEventArgs();
				aSyncArgs.SetBuffer(new byte[bufferSize], 0, bufferSize);
				aSyncArgs.RemoteEndPoint = remoteEP;
				aSyncArgs.Completed += ASyncArgs_Completed;

				if(!client.ReceiveFromAsync(aSyncArgs))
				{
					Console.WriteLine("Faild");
				}
			}
			catch(Exception e)
			{

				Console.WriteLine("Faild" + e);
			}

			
			
		}

		private void ASyncArgs_Completed(object sender, SocketAsyncEventArgs e)
		{
			Console.WriteLine(e.Buffer);
		}

		public void Send(string s)
		{
			try
			{
				var aSyncArgs = new SocketAsyncEventArgs();
				aSyncArgs.RemoteEndPoint = remoteEP;

				var stream = new MemoryStream();
				var pack = new Packet();
				pack.Test = s;

				MsgPack.Serialize(pack, stream, SerializationOptions.SuppressTypeInformation);

				var bytePack = stream.ToArray();
				aSyncArgs.SetBuffer(bytePack, 0, bytePack.Length);

				aSyncArgs.Completed += ASyncArgs_Completed1;
				client.SendToAsync(aSyncArgs);
			}
			catch(Exception e)
			{
				Console.WriteLine("Send failed " + e);
			}
		}

		private void ASyncArgs_Completed1(object sender, SocketAsyncEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
