using ITAMLib;
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
		public static ITAMInventory Inventory = new ITAMInventory();

		static void Main(string[] args)
		{
			Console.WriteLine(Environment.Version);
			Console.WriteLine(Environment.UserName);
			Console.WriteLine(Inventory.ComputerName);

			foreach (var item in Inventory.win32_BaseBoard.Items)
			{
				Console.WriteLine(item.Manufacturer);
				Console.WriteLine(item.Product);
				Console.WriteLine(item.SerialNumber);
				Console.WriteLine(item.Version);
			}

			Console.Write("\nPress any key...");
			Console.ReadKey();
		}
	}
}
