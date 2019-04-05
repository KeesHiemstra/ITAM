using ITAMLib;
using ITAMLib.Models;
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
		public static string DbConnection;
		public static ITAMInventory Inventory;

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

			using (ITAMDbContext db = new ITAMDbContext(DbConnection))
			{
				using (Win32_Product_SQL product_SQL = new Win32_Product_SQL())
				{
					foreach (var productItem in Inventory.win32_Product.Items)
					{
						// Clear product_SQL, based on Inventory.win32_Product.Items
						foreach (var property in productItem.GetType().GetProperties())
						{
							property.SetValue(product_SQL, null);
						}
						product_SQL.ComputerName = Inventory.ComputerName;

						// Transfer data if not <null>
						foreach (var property in productItem.GetType().GetProperties())
						{
							string value = (string)property.GetValue(productItem);
							if (value != "<null>")
							{
								property.SetValue(product_SQL, value);
							}
						}

						try
						{
							db.Product.Add(product_SQL);
							db.SaveChanges();
						}
						catch
						{
							//Console.WriteLine($"==== {product_SQL.ComputerName}");
							//Console.WriteLine($"Name              = {productItem.Name.Length}: {productItem.Name}");
							//Console.WriteLine($"Vendor            = {productItem.Vendor.Length}: {productItem.Vendor}");
							//Console.WriteLine($"Version           = {productItem.Version.Length}: {productItem.Version}");
							//Console.WriteLine($"IdentifyingNumber = {productItem.IdentifyingNumber}: {productItem.IdentifyingNumber.Length}");
							//Console.WriteLine($"InstalDate        = {productItem.InstallDate}: {productItem.InstallDate.Length}");
							//Console.WriteLine($"InstallLocation   = {productItem.InstallLocation}: {productItem.InstallLocation.Length}");
							//Console.WriteLine($"InstallSoure      = {productItem.InstallSource}: {productItem.InstallSource.Length}");
							//Console.WriteLine($"LocalPackage      = {productItem.LocalPackage}: {productItem.LocalPackage.Length}");
							//Console.WriteLine($"PackageCache      = {productItem.PackageCache}: {productItem.PackageCache.Length}");
							//Console.WriteLine($"PackageCode       = {productItem.PackageCode}: {productItem.PackageCode.Length}");
							//Console.WriteLine($"PackageName       = {productItem.PackageName}: {productItem.PackageName.Length}");
							//Console.WriteLine($"HelpLink          = {productItem.HelpLink}: {productItem.HelpLink.Length}");
							//Console.WriteLine($"URLInfoAbout      = {productItem.URLInfoAbout}: {productItem.URLInfoAbout.Length}");
							//Console.WriteLine($"URLUpdateInf      = {productItem.URLUpdateInfo}: {productItem.URLUpdateInfo.Length}");
						}
					}

				}
			}
		}

		private static void GetJsonPath()
		{
			if (File.Exists("\\\\NASServer\\Data\\Kees\\Inventory.exe"))
			{
				JsonPath = "\\\\NASServer\\Data\\Kees\\Inventory";
				DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			}
			else if (Directory.Exists("C:\\Users\\Kees\\OneDrive\\Etc\\ITAM\\Inventory"))
			{
				JsonPath = "C:\\Users\\Kees\\OneDrive\\Etc\\ITAM\\Inventory";
				DbConnection = @"Trusted_Connection=True;Data Source=(Local);Database=ITAM;MultipleActiveResultSets=true";
			}
			else if (Directory.Exists("C:\\Etc\\ITAM\\Inventory"))
			{
				JsonPath = "C:\\Etc\\ITAM\\Inventory";
			}
		}
	}
}
