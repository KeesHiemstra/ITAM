using ITAMLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAM_Dump
{
	class Program
	{
		public static ITAMInventory Inventory;

		static void Main(string[] args)
		{
			GetDump("W:\\Kees\\Inventory\\Inventory-PC06.json");

			Console.Write("\nPress any key...");
			Console.ReadKey();
		}

		private static void GetDump(string JsonFileName)
		{
			Inventory = new ITAMInventory();

			using (StreamReader stream = File.OpenText(JsonFileName))
			{
				string json = stream.ReadToEnd();
				Inventory = JsonConvert.DeserializeObject<ITAMInventory>(json);
			}

			foreach (var item in Inventory.GetType().GetProperties())
			{
				Console.WriteLine(item.Name);
			}
				
		}
	}
}
