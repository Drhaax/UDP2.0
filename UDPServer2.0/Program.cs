using System;

namespace UDPServer2._0
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new CHUDPClient();

			client.StartReceiving();

			while(true)
			{
				var cmd = Console.ReadLine();

				if(cmd == "exit")
				{
					break;
				}
			}
			
		}
	}
}
