using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_1911101
{
  class Program
  {
    public static Dictionary<string, string> Fields = new Dictionary<string, string>();
    public static List<string> Data = new List<string>();

    static void Main(string[] args)
    {
      Console.WriteLine("Query the registry for show installed software.\n");

      InitializeFields();
      InitializeData();

      GetInstalledSoftware(false);
      GetInstalledSoftware(true);

      //SaveData("C:\\Temp\\RegistryOutput.txt");

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }

    private static void SaveData(string dataFileName)
    {
      using (StreamWriter stream = new StreamWriter(dataFileName))
      {
        foreach (var item in Data)
        {
          stream.WriteLine(item);
        }
      }
    }

    private static void InitializeData()
    {
      if (Data.Count == 0)
      {
        string Line = "";
        foreach (var item in Fields)
        {
          if (!string.IsNullOrEmpty(Line))
          {
            Line += "\t";
          }
          Line += item.Key;
        }
        Data.Add(Line);
      }
    }

    private static void InitializeFields()
    {
      Fields.Add("ComputerName", "Computer");
      Fields.Add("32/64", "Function");
      Fields.Add("Name", "SubKey");
      Fields.Add("DisplayName", "Key");
      Fields.Add("Version", "Key");
      Fields.Add("DisplayVersion", "Key");
      Fields.Add("InstallLocation", "Key");
      Fields.Add("Publisher", "Key");
      Fields.Add("InstallDate", "Key");
      Fields.Add("UninstallString", "Key");
      Fields.Add("ModifyPath", "Key");
      Fields.Add("RepairPath", "Key");
      Fields.Add("URLInfoAbout", "Key");
      Fields.Add("NoRemove", "Key");
      Fields.Add("NoModify", "Key");
      Fields.Add("NoRepair", "Key");
      Fields.Add("HelpLink", "Key");
    }

    private static void GetInstalledSoftware(bool WOW6432Node)
    {
      string ComputerName = Environment.MachineName;
      string uninstallKey;
      string Bits = "";
      if (!WOW6432Node)
      {
        uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        Bits = "32";
      }
      else
      {
        uninstallKey = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        Bits = "64";
      }

      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(uninstallKey))
      {
        foreach (string subKeysNames in registryKey.GetSubKeyNames())
        {
          using (RegistryKey subRegistryKey = registryKey.OpenSubKey(subKeysNames))
          {
            string Name = subRegistryKey.Name.Replace($"HKEY_LOCAL_MACHINE\\{uninstallKey}\\", "");

            string Line = $"{ComputerName}\t{Bits}\t{Name}";
            foreach (var Field in Fields.Where(x => x.Value == "Key"))
            {
              try
              {
                Line += $"\t{subRegistryKey.GetValue(Field.Key).ToString()}";
              }
              catch /* (Exception ex) */
              {
                //Console.WriteLine($"{Bits}\t{Name}\t{Field.Key}\t{ex.Message}");
                Line += "\t";
              }
            }
            //toDo: Skip adding when it is already existing
            Data.Add(Line);
          }
        }//for each subKeyName
      }
    }
  }
}
