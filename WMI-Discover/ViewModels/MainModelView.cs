using ITAMLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WMI_Discover.ViewModels
{
  public class MainModelView
  {
    private const string JsonFileName = @"C:\Etc\ITAM\WMI\WMIClasses.json";
    private MainWindow Main;
    private int _classNameCount = -1;

    public static ObservableCollection<WMIClass> WMIClasses { get; set; } = new ObservableCollection<WMIClass>();
    public static string WMIClassName { get; set; } = string.Empty;
    public static List<string> CategoryNames { get; set; } = new List<string>();
    public static List<string> StatusNames { get; set; } = new List<string>();
    public string ClassNameContain { get; set; } = "BIOS";// string.Empty;
    public string CategoryName { get; set; } = "Win32";// string.Empty;
    public string StatusName { get; set; } = string.Empty;

    public MainModelView(MainWindow main)
    {
      Main = main;
      LoadWMIClasses();

      UpdateFilterWMIClassNames();
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
    }

    #endregion

  }
}
