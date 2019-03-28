using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITAMLib.Models
{
	public class Win32_ComputerSystem_List
	{
		public List<Win32_ComputerSystem> Items = new List<Win32_ComputerSystem>();

		public Win32_ComputerSystem_List(string WMIClass, string members)
		{
			CollectWmiClass(WMIClass, members);
		}

		private async void CollectWmiClass(string wmiClass, string members)
		{
			Items.Clear();

			try
			{
				foreach (ManagementObject managementObject in WMIList.GetCollection(wmiClass, members))
				{
					WMIRecord record = new WMIRecord(members);
					foreach (PropertyData propertyData in managementObject.Properties)
					{
						record.ProcessProperty(propertyData);
					}
					Items.Add(new Win32_ComputerSystem(record));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Querying the WMI Win32_ComputerSystem has an exception:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
	}
}
