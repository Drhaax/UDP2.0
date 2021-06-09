using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UDPServer2._0
{
	[DataContract]
	public class Packet
	{
		[DataMember(Order = 0)]
		public string Test { get; set; }

		public Packet() { 
		}
	}
}
