using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITAMLib.Models
{
	public class Win32_OperatingSystem_List
	{
		public List<Win32_OperatingSystem> Items = new List<Win32_OperatingSystem>();

		public Win32_OperatingSystem_List(string WMIClass, string members)
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
					Items.Add(new Win32_OperatingSystem(record));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Querying the WMI Win32_OperatingSystem has an exception:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
	}
}
