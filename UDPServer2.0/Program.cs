using System;

namespace CircleHard
{
	class Program
	{
		static void Main(string[] args)
		{
			var s = new CHUDPClient();
			s.Server("127.0.0.1", 27000);

			var c = new CHUDPClient();
			c.Client("127.0.0.1", 27000);

			c.Send("oispaKaljaa");
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
