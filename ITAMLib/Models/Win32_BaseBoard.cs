using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_BaseBoard
	{
		public string Manufacturer { get; set; }
		public string Product { get; set; }
		public string SerialNumber { get; set; }
		public string Version { get; set; }

		public Win32_BaseBoard(WMIRecord data)
		{
			Manufacturer = data.Properties["Manufacturer"];
			Product = data.Properties["Product"];
			SerialNumber = data.Properties["SerialNumber"];
			Version = data.Properties["Version"];
		}
	}
}
