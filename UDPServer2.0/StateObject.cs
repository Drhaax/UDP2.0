using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CircleHard
{
	public class StateObject
	{
		public Socket workSocket = null;
		public string tesb;

		public StateObject(Socket Socket, string s)
		{
			workSocket = Socket;
			tesb = s;
		}
	}
}
