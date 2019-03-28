using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public static class WMIList
	{
		public static ManagementObjectCollection GetCollection(string wmiClass)
		{
			return GetCollection(wmiClass, "*");
		}

		public static ManagementObjectCollection GetCollection(string wmiClass, string Members)
		{
			ConnectionOptions options = new ConnectionOptions
			{
				Impersonation = ImpersonationLevel.Impersonate
			};

			ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
			scope.Connect();

			ObjectQuery query = new ObjectQuery($"SELECT * FROM {wmiClass}");

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

			ManagementObjectCollection queryCollection = searcher.Get();

			return queryCollection;
		}
	}
}
