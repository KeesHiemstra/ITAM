using ITAMLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WMI_Classes
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private const string JsonFileName = @"C:\Etc\ITAM\WMI\WMIClasses.json";

    public List<string> ClassNames = new List<string>();
    public ObservableCollection<WMIClass> WMIClasses { get; set; } = new ObservableCollection<WMIClass>();

    public MainWindow()
    {
      InitializeComponent();

      LoadWMIClasses();
      CollectAllWMIClasses();
      CompareWMIClasses();

      WMIClassesDataGrid.ItemsSource = WMIClasses;
    }

    private void LoadWMIClasses()
    {
      if (File.Exists(JsonFileName))
      {
        using (StreamReader stream = File.OpenText(JsonFileName))
        {
          string json = stream.ReadToEnd();
          WMIClasses = JsonConvert.DeserializeObject<ObservableCollection<WMIClass>>(json);
        }
      }
    }

    private void CollectAllWMIClasses()
    {
      ConnectionOptions options = new ConnectionOptions
      {
        Impersonation = System.Management.ImpersonationLevel.Impersonate
      };

      ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
      scope.Connect();

      ObjectQuery query = new ObjectQuery($"SELECT * FROM meta_class");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

      ManagementObjectCollection queryCollection = searcher.Get();

      try
      {
        foreach (ManagementClass managementClass in queryCollection)
        {
          ClassNames.Add(managementClass.ToString().Split(':')[1].ToString());
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Exception");
      }
    }

    private void CompareWMIClasses()
    {
      Dictionary<string, string> CatagoryPairs = new Dictionary<string, string>
      {
        { "ccm", "CCM" },
        { "cim", "CIM" },
        { "hp", "HP" },
        { "msft", "MSFT" },
        { "office2013", "Office2013" },
        { "sms", "SMS" },
        { "win32", "Win32" },
        { "win32reg", "Win32Reg" }
      };

      foreach (string ClassName in ClassNames)
      {
        int FoundClass = WMIClasses.Where(x => x.Name == ClassName).Count();

        if (FoundClass == 0)
        {
          string category = string.Empty;
          if (ClassName.StartsWith("_"))
          {
            category = "Internal";
          }
          else if (ClassName.Contains("perf"))
          {
            category = "Perf";
          }
          else
          {
            int underscore = ClassName.IndexOf('_');
            if (underscore > 0)
            {
              if (CatagoryPairs.ContainsKey(ClassName.Substring(0, underscore).ToLower()))
              {
                category = CatagoryPairs[ClassName.Substring(0, underscore).ToLower()];
              }
              else
              {
                category = "unknown";
              }
            }
          }
          WMIClasses.Add(new WMIClass {
            Name = ClassName,
            Catagory = category,
            Status = "Unknown"
          });
        }
      }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      string json = JsonConvert.SerializeObject(WMIClasses.OrderBy(x => x.Name), Formatting.Indented);
      using (StreamWriter stream = new StreamWriter(JsonFileName))
      {
        stream.Write(json);
      }
    }
  }
}
