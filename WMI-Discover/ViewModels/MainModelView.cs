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
using System.Windows.Input;

namespace WMI_Discover.ViewModels
{
  public class MainModelView
  {
    private const string JsonFileName = @"C:\Etc\ITAM\WMI\WMIClasses.json";
    private MainWindow Main;
    private int _classNameCount = -1;
    private bool WMIClassesUpdated = false;

    public static ObservableCollection<WMIClass> WMIClasses { get; set; } = new ObservableCollection<WMIClass>();
    public ObservableCollection<WMIProperty> WMIProperties { get; set; } = new ObservableCollection<WMIProperty>();
    public static string WMIClassName { get; set; } = string.Empty; // Selected class name

    // Search panel
    public static List<string> CategoryNames { get; set; } = new List<string>();
    public static List<string> StatusNames { get; set; } = new List<string>();
    public string ClassNameContain { get; set; } = string.Empty; // Search box content
    public string CategoryName { get; set; } = string.Empty; // Category dropdown content
    public string StatusName { get; set; } = string.Empty; // Status dropdown content

    // WMIProperties results
    public bool ReadWMIPropertiesFromFile { get; set; } = false;

    public MainModelView(MainWindow main)
    {
      Main = main;
      LoadWMIClasses();

      UpdateFilterWMIClassNames();
    }

    #region On Opening and closing the application

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

    public void SaveWMIClasses()
    {
      if (WMIClassesUpdated)
      {
        string json = JsonConvert.SerializeObject(WMIClasses, Formatting.Indented);
        using (StreamWriter stream = new StreamWriter(JsonFileName))
        {
          stream.Write(json);
        }
      }
    }

    #endregion

    #region FilterWMIClassNames

    private List<string> FilterWMIClassNames()
    {
      IEnumerable<WMIClass> queryWMIClassNames = SubFilterWMIClassNames();

      List<string> result = queryWMIClassNames
          .Select(x => x.Name)
          .Distinct()
          .ToList();

      _classNameCount = result.Count;

      queryWMIClassNames = SubFilterWMIClassNames();

      CategoryNames = queryWMIClassNames
        .Select(x => x.Catagory)
        .Distinct()
        .ToList();

      Main.CategoryComboBox.ItemsSource = CategoryNames;

      queryWMIClassNames = SubFilterWMIClassNames();

      StatusNames = queryWMIClassNames
        .Select(x => x.Status)
        .Distinct()
        .ToList();

      Main.StatusComboBox.ItemsSource = StatusNames;

      return result;
    }

    private IEnumerable<WMIClass> SubFilterWMIClassNames()
    {
      IEnumerable<WMIClass> result = from q in WMIClasses
                                     select q;

      if (!string.IsNullOrEmpty(ClassNameContain))
      {
        result = result
          .Where(x => x.Name.ToLower().Contains(ClassNameContain.ToLower()));
      }

      if (!string.IsNullOrEmpty(CategoryName))
      {
        result = result
          .Where(x => x.Catagory == CategoryName);
      }

      if (!string.IsNullOrEmpty(StatusName))
      {
        result = result
          .Where(x => x.Status == StatusName);
      }

      return result;
    }

    public void UpdateFilterWMIClassNames()
    {
      Main.WMIClassComboBox.ItemsSource = FilterWMIClassNames();
      string extra = "";
      if (_classNameCount != 1)
      {
        extra = "s";
      }
      Main.ClassNameCountTextBlock.Text = $"{_classNameCount} class name{extra}";

      if (WMIProperties.Count != 0)
      {
        Main.WMIPropertiesDataGrid.ItemsSource = null;
      }
    }

    #endregion

    #region Acting on changes

    public async Task<bool> SelectWMIClassName(string wMIClassName)
    {
      WMIClassName = wMIClassName;
      string fileName = $"C:\\Etc\\ITAM\\WMI\\{WMIClassName}-Data.json";
      bool result = false;

      if (File.Exists(fileName))
      {
        try
        {
          using (StreamReader stream = File.OpenText(fileName))
          {
            string json = await stream.ReadToEndAsync();
            WMIProperties = JsonConvert.DeserializeObject<ObservableCollection<WMIProperty>>(json);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error reading json [{fileName}]:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        ReadWMIPropertiesFromFile = true;
      }
      else
      {
        ReadWMIPropertiesFromFile = false;
        result = await CollectWMIClass();

        if (result && WMIProperties.Count > 0)
        {
          string json = JsonConvert.SerializeObject(WMIProperties, Formatting.Indented);
          using (StreamWriter stream = new StreamWriter(fileName))
          {
            await stream.WriteAsync(json);
          }

          UpdateWMIClassesRecord("OK");
          WMIClassesUpdated = true;
        }
        else if (!result)
        {
          UpdateWMIClassesRecord("Error");
          WMIClassesUpdated = true;
        }
        else if (WMIProperties.Count == 0)
        {
          UpdateWMIClassesRecord("Empty");
          WMIClassesUpdated = true;
        }
      }

      string extra = "y";
      if (WMIProperties.Count != 1)
      {
        extra = "ies";
      }
      Main.PropertiesCountTextBlock.Text = $"{WMIProperties.Count} propert{extra}";

      return result;
    }

    private void UpdateWMIClassesRecord(string status)
    {
      IEnumerable<WMIClass> result = from q in WMIClasses
                                     select q;

      result = result
        .Where(x => x.Name == WMIClassName);

      WMIClass record = result
          .SingleOrDefault();

      record.Status = status;
    }

    private async Task<bool> CollectWMIClass()
    {
      if (string.IsNullOrEmpty(WMIClassName))
      {
        MessageBox.Show("There is no selected WMI Class", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        return false;
      }

      WMIProperties.Clear();

      #region Preparation

      ConnectionOptions options = new ConnectionOptions
      {
        Impersonation = System.Management.ImpersonationLevel.Impersonate
      };

      ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
      scope.Connect();

      //Query system for Operating System information
      ObjectQuery query = new ObjectQuery($"SELECT * FROM {WMIClassName}");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

      ManagementObjectCollection queryCollection = searcher.Get();

      #endregion

      try
      {
        int collectionIndex = 0;
        foreach (ManagementObject managementObject in queryCollection)
        {
          int propertyIndex = 0;
          foreach (PropertyData propertyData in managementObject.Properties)
          {
            WMIProperties.Add(new WMIProperty(collectionIndex, propertyIndex, propertyData));
            propertyIndex++;
          }
          collectionIndex++;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error: {ex.Message}", "Exception error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      //Main.WMIPropertiesDataGrid.ItemsSource = WMIProperties;
      return true;
    }

    #endregion

    #region Acting on SearchBox

    public void SearchTextBoxOnKey(KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        Main.WMIClassComboBox.Focus(); // Forces to send the box content to memory
        UpdateFilterWMIClassNames();
      }
    }

    #endregion
  }
}
