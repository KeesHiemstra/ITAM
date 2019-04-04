using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib
{
	/// <summary>
	/// Empty ITAMInventory
	/// </summary>
	public class ITAMInventory
	{
		public string ComputerName { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string UserDomainName { get; set; } = string.Empty;
		public string OSVersion { get; set; } = string.Empty;
		public string Version { get; set; } = string.Empty;
		public string InventoryVersion { get; set; } = string.Empty;
		public bool UserInteractive { get; set; } = false;

		public Win32_BaseBoard_List win32_BaseBoard = new Win32_BaseBoard_List();
		public Win32_SystemEnclosure_List win32_SystemEnclosure = new Win32_SystemEnclosure_List();
		public Win32_OperatingSystem_List win32_OperatingSystem = new Win32_OperatingSystem_List();
		public Win32_ComputerSystem_List win32_ComputerSystem = new Win32_ComputerSystem_List();
		public Win32_Account_List win32_Account = new Win32_Account_List();
		public Win32_BIOS_List win32_BIOS = new Win32_BIOS_List();
		public Win32_Product_List win32_Product = new Win32_Product_List();
	}
}
