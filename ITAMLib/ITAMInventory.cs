using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib
{
	public class ITAMInventory
	{
		public string ComputerName { get; set; } = Environment.MachineName;

		public Win32_BaseBoard_List win32_BaseBoard = new Win32_BaseBoard_List("Win32_BaseBoard", "Manufacturer,Product,SerialNumber,Version");

		public Win32_SystemEnclosure_List win32_SystemEnclosure = new Win32_SystemEnclosure_List("Win32_SystemEnclosure", "Manufacturer,SerialNumber,SMBIOSAssetTag,Version");

		public Win32_OperatingSystem_List win32_OperatingSystem = new Win32_OperatingSystem_List("Win32_OperatingSystem", "BootDevice,BuildNumber,BuildType,Caption,CodeSet,CountryCode,CSName,CurrentTimeZone,InstallDate,LastBootUpTime,Locale,Manufacturer,MUILanguages,NumberOfUsers,OperatingSystemSKU,Organization,OSArchitecture,OSLanguage,OSProductSuite,OSType,OtherTypeDescription,RegisteredUser,ServicePackMajorVersion,ServicePackMinorVersion,SystemDevice,SystemDirectory,SystemDrive,Version,WindowsDirectory");

		public Win32_ComputerSystem_List win32_ComputerSystem = new Win32_ComputerSystem_List("Win32_ComputerSystem", "AdminPasswordStatus,AutomaticManagedPagefile,AutomaticResetBootOption,AutomaticResetCapability,Caption,DNSHostName,Domain,DomainRole,EnableDaylightSavingsTime,Manufacturer,Model,Name,PartOfDomain,PrimaryOwnerName,Roles,SystemFamily,SystemType,TotalPhysicalMemory,UserName,Workgroup");

		public Win32_Account_List win32_Account = new Win32_Account_List("Win32_Account", "Caption,Description,Disabled,Domain,LocalAccount,Name,SID,SIDType,Status");

		public Win32_BIOS_List win32_BIOS = new Win32_BIOS_List("Win32_BIOS", "Manufacturer,Name,ReleaseDate,SerialNumber,Version");
	}
}
