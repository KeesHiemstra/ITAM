using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_ComputerSystem
	{
		public string AdminPasswordStatus { get; set; }
		public string AutomaticManagedPagefile { get; set; }
		public string AutomaticResetBootOption { get; set; }
		public string AutomaticResetCapability { get; set; }
		public string Caption { get; set; }
		public string DNSHostName { get; set; }
		public string Domain { get; set; }
		public string DomainRole { get; set; }
		public string EnableDaylightSavingsTime { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string Name { get; set; }
		public string PartOfDomain { get; set; }
		public string PrimaryOwnerName { get; set; }
		public string Roles { get; set; }
		public string SystemFamily { get; set; }
		public string SystemType { get; set; }
		public string TotalPhysicalMemory { get; set; }
		public string UserName { get; set; }
		public string Workgroup { get; set; }

		public Win32_ComputerSystem(WMIRecord data)
		{
			AdminPasswordStatus = data.Properties["AdminPasswordStatus"];
			AutomaticManagedPagefile = data.Properties["AutomaticManagedPagefile"];
			AutomaticResetBootOption = data.Properties["AutomaticResetBootOption"];
			AutomaticResetCapability = data.Properties["AutomaticResetCapability"];
			Caption = data.Properties["Caption"];
			DNSHostName = data.Properties["DNSHostName"];
			Domain = data.Properties["Domain"];
			DomainRole = data.Properties["DomainRole"];
			EnableDaylightSavingsTime = data.Properties["EnableDaylightSavingsTime"];
			Manufacturer = data.Properties["Manufacturer"];
			Model = data.Properties["Model"];
			Name = data.Properties["Name"];
			PartOfDomain = data.Properties["PartOfDomain"];
			PrimaryOwnerName = data.Properties["PrimaryOwnerName"];
			Roles = data.Properties["Roles"];
			SystemFamily = data.Properties["SystemFamily"];
			SystemType = data.Properties["SystemType"];
			TotalPhysicalMemory = data.Properties["TotalPhysicalMemory"];
			UserName = data.Properties["UserName"];
			Workgroup = data.Properties["Workgroup"];
		}
	}
}
