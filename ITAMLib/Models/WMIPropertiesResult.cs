using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib.Models
{
  public class WMIPropertiesResult
  {
    private ObservableCollection<WMIProperty> properties { get; }
    private List<string> UniqueNames = new List<string>();

    public string ClassName { get; set; }
    public int PropertyCount { get; set; }
    public int RecordCount { get; set; }

    public readonly Dictionary<string, WMIPropertiesCount> Results = new Dictionary<string, WMIPropertiesCount>();

    public WMIPropertiesResult(ObservableCollection<WMIProperty> Properties)
    {
      properties = Properties;

      // Count all properties
      PropertyCount = properties.Count;

      // List all unique propery Name
      CreateUniqueNames();

      // Count the occerences on Name
    }

    private void CreateUniqueNames()
    {
      
    }
  }

  public class WMIPropertiesCount
  {
    public string PropertyName { get; set; }
    public int PropertyCount { get; set; }
    public int UniqueValueCount { get; set; }
    public List<string> UniqueValue { get; set; }
  }
}
