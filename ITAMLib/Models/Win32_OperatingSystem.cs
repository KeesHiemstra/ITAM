using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_OperatingSystem
	{
		public string BootDevice { get; set; }
		public string BuildNumber { get; set; }
		public string BuildType { get; set; }
		public string Caption { get; set; }
		public string CodeSet { get; set; }
		public string CountryCode { get; set; }
		public string CSName { get; set; }
		public string CurrentTimeZone { get; set; }
		public string InstallDate { get; set; }
		public string LastBootUpTime { get; set; }
		public string Locale { get; set; }
		public string Manufacturer { get; set; }
		public string MUILanguages { get; set; }
		public string NumberOfUsers { get; set; }
		public string OperatingSystemSKU { get; set; }
		public string Organization { get; set; }
		public string OSArchitecture { get; set; }
		public string OSLanguage { get; set; }
		public string OSProductSuite { get; set; }
		public string OSType { get; set; }
		public string OtherTypeDescription { get; set; }
		public string RegisteredUser { get; set; }
		public string ServicePackMajorVersion { get; set; }
		public string ServicePackMinorVersion { get; set; }
		public string SystemDevice { get; set; }
		public string SystemDirectory { get; set; }
		public string SystemDrive { get; set; }
		public string Version { get; set; }
		public string WindowsDirectory { get; set; }

		public Win32_OperatingSystem(WMIRecord data)
		{
			BootDevice = data.Properties["BootDevice"];
			BuildNumber = data.Properties["BuildNumber"];
			BuildType = data.Properties["BuildType"];
			Caption = data.Properties["Caption"];
			CodeSet = data.Properties["CodeSet"];
			CountryCode = data.Properties["CountryCode"];
			CSName = data.Properties["CSName"];
			CurrentTimeZone = data.Properties["CurrentTimeZone"];
			InstallDate = data.Properties["InstallDate"];
			LastBootUpTime = data.Properties["LastBootUpTime"];
			Locale = data.Properties["Locale"];
			Manufacturer = data.Properties["Manufacturer"];
			MUILanguages = data.Properties["MUILanguages"];
			NumberOfUsers = data.Properties["NumberOfUsers"];
			OperatingSystemSKU = data.Properties["OperatingSystemSKU"];
			Organization = data.Properties["Organization"];
			OSArchitecture = data.Properties["OSArchitecture"];
			OSLanguage = data.Properties["OSLanguage"];
			OSProductSuite = data.Properties["OSProductSuite"];
			OSType = data.Properties["OSType"];
			OtherTypeDescription = data.Properties["OtherTypeDescription"];
			RegisteredUser = data.Properties["RegisteredUser"];
			ServicePackMajorVersion = data.Properties["ServicePackMajorVersion"];
			ServicePackMinorVersion = data.Properties["ServicePackMinorVersion"];
			SystemDevice = data.Properties["SystemDevice"];
			SystemDirectory = data.Properties["SystemDirectory"];
			SystemDrive = data.Properties["SystemDrive"];
			Version = data.Properties["Version"];
			WindowsDirectory = data.Properties["WindowsDirectory"];
		}
	}
}
