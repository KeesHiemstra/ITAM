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
		private readonly MainWindow Main;

		public string JsonPath = string.Empty;
		public IEnumerable<string> JsonFiles = new List<string>();

		public MainModelView(MainWindow main)
		{
			Main = main;

			CollectJsonFiles();
		}

		private void CollectJsonFiles()
		{
			if (File.Exists("\\\\NASServer\\Data\\Kees\\Inventory.exe"))
			{
				JsonPath = "\\\\NASServer\\Data\\Kees";
			}
			else if (Directory.Exists("C:\\Etc"))
			{
				JsonPath = "C:\\Etc";
			}

			//JsonFiles = Directory.EnumerateFiles(JsonPath, "Inventory-*.json");
		}

	}
}
