using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CircleHard
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
