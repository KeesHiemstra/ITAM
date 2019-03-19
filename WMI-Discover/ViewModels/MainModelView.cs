using ITAMLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMI_Discover.ViewModels
{
  public class MainModelView
  {
    private const string JsonFileName = @"C:\Etc\ITAM\WMI\WMIClasses.json";
    private MainWindow Main;

    public static ObservableCollection<WMIClass> WMIClasses { get; set; } = new ObservableCollection<WMIClass>();


    public MainModelView()
    {
      LoadWMIClasses();


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

    private void FilteredWMIClasses()
    {
      var Filter;
      if (true)
      {
        Filter = Filter.WMIClasses.Where
      }
    }
  }
}
