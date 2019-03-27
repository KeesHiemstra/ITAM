using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_BIOS
	{
		public string Manufacturer { get; set; }
		public string Name { get; set; }
		public string ReleaseDate { get; set; }
		public string SerialNumber { get; set; }
		public string Version { get; set; }

		public Win32_BIOS(WMIRecord data)
		{
			Manufacturer = data.Properties["Manufacturer"];
			Name = data.Properties["Name"];
			ReleaseDate = data.Properties["ReleaseDate"];
			SerialNumber = data.Properties["SerialNumber"];
			Version = data.Properties["Version"];
		}
	}
}
