using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.Models
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

		public Win32_Product() { }
	}
}
