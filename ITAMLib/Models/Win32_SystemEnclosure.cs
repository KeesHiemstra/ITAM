using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_SystemEnclosure
	{
		public string Manufacturer { get; set; }
		public string SerialNumber { get; set; }
		public string SMBIOSAssetTag { get; set; }
		public string Version { get; set; }

		public Win32_SystemEnclosure(WMIRecord data)
		{
			Manufacturer = data.Properties["Manufacturer"];
			SerialNumber = data.Properties["SerialNumber"];
			SMBIOSAssetTag = data.Properties["SMBIOSAssetTag"];
			Version = data.Properties["Version"];
		}
	}
}
