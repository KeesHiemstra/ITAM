using ITAMLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInventory
{
	class Program
	{
		public static string JsonPath;
		public static ITAMInventory Inventory;

		private readonly ITAMDbContext _context;
		public Program(ITAMDbContext context) { _context = context; }

		static void Main(string[] args)
		{
			GetJsonPath();
			GetJsonFiles();

			Console.Write("\nPress any key...");
			Console.ReadKey();
		}

		private static void GetJsonFiles()
		{
			IEnumerable<string> jsonFiles = Directory.EnumerateFiles(JsonPath, "Inventory-*.json");
			foreach (string jsonFile in jsonFiles)
			{
				Inventory = new ITAMInventory();

				using (StreamReader stream = File.OpenText(jsonFile))
				{
					string json = stream.ReadToEnd();
					Inventory = JsonConvert.DeserializeObject<ITAMInventory>(json);

					ImportJson();
				}
			}
		}

		private static void ImportJson()
		{
			Console.WriteLine(Inventory.ComputerName);

			using (Win32_Product_SQL product_SQL = new Win32_Product_SQL())
			{
				foreach (var item in Inventory.win32_Product.Items)
				{
					product_SQL.ComputerName = Inventory.ComputerName;

				}
				
			}
		}

		private static void GetJsonPath()
		{
			if (File.Exists("\\\\NASServer\\Data\\Kees\\Inventory.exe"))
			{
				JsonPath = "\\\\NASServer\\Data\\Kees\\Inventory";
			}
			else if (Directory.Exists("C:\\Users\\Kees\\OneDrive\\Etc\\ITAM\\Inventory"))
			{
				JsonPath = "C:\\Users\\Kees\\OneDrive\\Etc\\ITAM\\Inventory";
			}
			else if (Directory.Exists("C:\\Etc\\ITAM\\Inventory"))
			{
				JsonPath = "C:\\Etc\\ITAM\\Inventory";
			}
		}
	}
}
