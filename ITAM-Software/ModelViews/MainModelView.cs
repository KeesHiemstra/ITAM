using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITAM_Software.ModelViews
{
	public class MainModelView
	{
		private MainWindow Main;

		public string JsonPath = string.Empty;
		public List<string> JsonFiles = new List<string>();

		public MainModelView(MainWindow main)
		{
			Main = main;

			CollectJsonFiles();
		}

		private void CollectJsonFiles()
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

			IEnumerable<string> jsonFiles = Directory.EnumerateFiles(JsonPath, "Inventory-*.json");
			foreach (string item in jsonFiles)
			{
				JsonFiles.Add(item.Replace(JsonPath + "\\Inventory-", "").Replace(".json", ""));
			}
		}

	}
}
