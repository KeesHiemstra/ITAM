using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
	class Program
	{
		Win32_BaseBoard_List win32_Product = new Win32_BaseBoard_List("Win32_BaseBoard", "Manufacturer,SerialNumber,Version");

		static void Main(string[] args)
		{
		}
	}
}
