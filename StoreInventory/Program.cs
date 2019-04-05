﻿using ITAMLib;
using ITAMLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreInventory
{
	class Program
	{
		public static string JsonPath;
		public static string DbConnection;

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
							IEnumerable<object> find =
								new List<object> { product_SQL.ComputerName, product_SQL.IdentifyingNumber, product_SQL.Name, product_SQL.Vendor, product_SQL.Version };
							var result = db.Product.SqlQuery("SELECT * FROM Win32_Product WHERE [ComputerName] = @p0 AND [IdentifyingNumber] = @p1 AND [Name] = @p2 AND [Vendor] = @p3 AND [Version] = @p4", product_SQL.ComputerName, product_SQL.IdentifyingNumber, product_SQL.Name, product_SQL.Vendor, product_SQL.Version);
							if (result.Count() == 0)
							{
								db.Product.Add(product_SQL);
							}
							db.SaveChanges();
						}
						catch (Exception ex)
						{
							string message = $"{ex.Source}\n{ex.Message}\n\n";
							foreach (var item in productItem.GetType().GetProperties())
							{
								string value = (string)item.GetValue(productItem);
								message += $"{item.Name.PadRight(17)} = {value.Length.ToString().PadLeft(3)}: {value}\n";
							}
							MessageBox.Show(message, $"Error {Inventory.ComputerName}: 0x{ex.HResult:X}", MessageBoxButton.OK, MessageBoxImage.Error);
						}//try - catch
					}//for all items
				}//using product_SQL
			}//using ITAMDbContext
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
