using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_Product
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string Vendor { get; set; }
		public string IdentifyingNumber { get; set; }
		public string InstallSource { get; set; }
		public string InstallLocation { get; set; }
		public string InstallDate { get; set; }
		public string LocalPackage { get; set; }
		public string PackageCode { get; set; }
		public string PackageCache { get; set; }
		public string PackageName { get; set; }
		public string HelpLink { get; set; }
		public string URLInfoAbout { get; set; }
		public string URLUpdateInfo { get; set; }

		public Win32_Product(WMIRecord data)
		{
			HelpLink = data.Properties["HelpLink"];
			IdentifyingNumber = data.Properties["IdentifyingNumber"];
			InstallDate = data.Properties["InstallDate"];
			InstallLocation = data.Properties["InstallLocation"];
			InstallSource = data.Properties["InstallSource"];
			LocalPackage = data.Properties["LocalPackage"];
			Name = data.Properties["Name"];
			PackageCache = data.Properties["PackageCache"];
			PackageCode = data.Properties["PackageCode"];
			PackageName = data.Properties["PackageName"];
			URLInfoAbout = data.Properties["URLInfoAbout"];
			URLUpdateInfo = data.Properties["URLUpdateInfo"];
			Vendor = data.Properties["Vendor"];
			Version = data.Properties["Version"];
		}
	}
}
