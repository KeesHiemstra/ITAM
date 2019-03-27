using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
	public class Win32_Account
	{
		public string Caption { get; set; }
		public string Description { get; set; }
		public string Disabled { get; set; }
		public string Domain { get; set; }
		public string LocalAccount { get; set; }
		public string Name { get; set; }
		public string SID { get; set; }
		public string SIDType { get; set; }
		public string Status { get; set; }

		public Win32_Account(WMIRecord data)
		{
			Caption = data.Properties["Caption"];
			Description = data.Properties["Description"];
			Disabled = data.Properties["Disabled"];
			Domain = data.Properties["Domain"];
			LocalAccount = data.Properties["LocalAccount"];
			Name = data.Properties["Name"];
			SID = data.Properties["SID"];
			SIDType = data.Properties["SIDType"];
			Status = data.Properties["Status"];
		}
	}
}
