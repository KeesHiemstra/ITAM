using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
  public class WMIPropertiesPivot
  {
    private ObservableCollection<WMIProperty> properties { get; }
    private List<string> UniqueNames = new List<string>();

    public string ClassName { get; set; }
    public int PropertyCount { get; set; }
    public int CollectionCount { get; set; }
    public int UniqueNameCount { get; set; }

    public ObservableCollection<WMIPropertyPivot> Pivots = new ObservableCollection<WMIPropertyPivot>();
    
    public WMIPropertiesPivot(ObservableCollection<WMIProperty> Properties)
    {
      properties = Properties;

      // Count all properties
      PropertyCount = properties.Count;

      // Count the number of collections
      CollectionCount = properties.Max(x => x.CollectionIndex);

      // List all unique property Name
      UniqueNames = CreateUniqueNames();

      // Count the occurrences on Name
      UniqueNameCount = UniqueNames.Count;

      // In theory PropertyCount = CollectionCount * UniqueNameCount
      // >> This will not happen with Win32_Account WMIClass, this WMIClass is not inconsistent.

      foreach (string UniqueName in UniqueNames)
      {
        GetPivots(UniqueName);
      }
    }

    private List<string> CreateUniqueNames()
    {
      // Collect all unique property names
      IEnumerable<WMIProperty> result = from q in properties
                                        select q;
      return result
        .Select(x => x.Name)
        .Distinct()
        .ToList();
    }

    private void GetPivots(string uniqueName)
    {
      IEnumerable<WMIProperty> queryProperty = from q in properties
                                               select q;
      queryProperty = queryProperty
        .Where(x => x.Name == uniqueName)
        .ToList();

      WMIPropertyPivot pivot = new WMIPropertyPivot(uniqueName);

      GetTypePivot(queryProperty, pivot);
    }

    private void GetTypePivot(IEnumerable<WMIProperty> queryProperty, WMIPropertyPivot pivot)
    {
      List<string> Types = new List<string>();
      Types = queryProperty
        .Select(x => x.Type)
        .Distinct()
        .ToList();

      pivot.TypeCount = Types.Count;

      if (Types.Count == 1)
      {
        pivot.Type = Types[0];
      }
      else
      {
        string result = "";
        foreach (string item in Types)
        {
          if (!string.IsNullOrEmpty(result))
          {
            result += ",";
          }
          result += item;
          pivot.Type = "{" + result + "}";
        }
      }
    }

  }

  public class WMIPropertyPivot
  {
    public bool Select { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int TypeCount { get; set; }
    public int PropertyCount { get; set; }
    public int UniqueValueCount { get; set; }

    // Not in Json
    public List<string> UniqueValues { get; set; }

    public WMIPropertyPivot(string name)
    {
      Name = name;
    }
  }
}
