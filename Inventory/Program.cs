using ITAMLib;
using ITAMLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"Inventory version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			Console.WriteLine($"     Machine name: {Environment.MachineName}");
			Console.WriteLine($"        User name: {Environment.UserName}");
			Console.WriteLine($" User domain name: {Environment.UserDomainName}");
			Console.WriteLine($"       OS version: {Environment.OSVersion}");
			Console.WriteLine($"          Version: {Environment.Version}");
			Console.WriteLine($"   Is interactive: {Environment.UserInteractive}");

			Console.WriteLine();
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Start inventory");
			ITAMInventory Inventory = new ITAMInventory();
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Inventory completed");

			string json = JsonConvert.SerializeObject(Inventory, Formatting.Indented);
			using (StreamWriter stream = new StreamWriter($"\\\\NASServer\\Data\\Kees\\Inventory-{Environment.MachineName}.json"))
			{
				stream.Write(json);
			}

			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Inventory saved");

			Console.Write("\nPress any key...");
			Console.ReadKey();
		}
	}
}
