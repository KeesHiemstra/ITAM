using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ITAMLib.Models;

namespace ITAMLib.Models
{
	public class Win32_BaseBoard_List
	{
		public string ComputerName { get; set; }
		public List<Win32_BaseBoard> Items = new List<Win32_BaseBoard>();

		public Win32_BaseBoard_List(string WmiClass, string members)
		{
			ComputerName = System.Environment.MachineName;
			CollectWmiClass(WmiClass, members);
		}

		private async void CollectWmiClass(string wmiClass, string members)
		{
				Items.Clear();

				try
				{
					foreach (ManagementObject managementObject in WmiList.GetCollection(wmiClass, members))
					{
						WmiRecord record = new WmiRecord(members);
						foreach (PropertyData propertyData in managementObject.Properties)
						{
							record.ProcessProperty(propertyData);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Quering the WMI results in an exception:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}
	}
