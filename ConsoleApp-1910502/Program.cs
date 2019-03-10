using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_1910502
{
  class Program
  {
    static void Main(string[] args)
    {
      ShowWMI("Win32_OperatingSystem");

      Console.Write("Press any key...");
      Console.ReadKey();
    }

    private static void ShowWMI(string wmiName)
    {
      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
      ConnectionOptions options = new ConnectionOptions();
      options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
      ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
      scope.Connect();

      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
      //Query system for Operating System information
      ObjectQuery query = new ObjectQuery($"SELECT * FROM {wmiName}");

      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
      ManagementObjectCollection queryCollection = searcher.Get();

      Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Collection: {queryCollection.Count}");

      int managementObjectIndex = 0;
      foreach (ManagementObject managementObject in queryCollection)
      {
        foreach (PropertyData propertyData in managementObject.Properties)
        {
          Console.Write($"{managementObjectIndex} {propertyData.Name} ");
          Console.Write($"[{propertyData.Type.ToString()}]: ");
          WriteValue(propertyData.IsArray, propertyData.Type.ToString(), propertyData.Value);
        }

        managementObjectIndex++;
      }

    }

    private static void WriteValue(bool isArray, string type, object value)
    {
      if (value == null)
      {
        Console.WriteLine("<null>");
        return;
      }

      if (isArray)
      {
        Console.WriteLine("<Array>");
        foreach (var item in (string[])value)
        {
          Console.WriteLine($"    {item}");
        }
        return;
      }

      string result = "";
      try
      {
        result = value.ToString();
      }
      catch (Exception ex)
      {
        result = ex.Message;
      }
      
      Console.WriteLine($"{result}");
    }
  }
}

/*
          if (propertyData.Value == null)
          {
            Console.WriteLine($"<null>");
          }
          else
          { 
            try
            {
              Console.WriteLine($"{propertyData.Value.ToString()}");
            }
            catch (Exception)
            {
              Console.WriteLine($"<no ToString()>");
            }

          }
 
*/
