using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ITAMLib.Models
{
  public class WMIPropertyPivot
  {
    public bool Select { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int TypeCount { get; set; } = -1; // Unique types
    public int TypeOcc { get; set; } = -1; // Occurrences of the types
    public int ValueCount { get; set; } = -1; // Values
    public int ValueUniqueCount { get; set; } = -1; // Unique values
    public int ValueCleanCount { get; set; } = -1;  // Clean unique values

    [JsonIgnore]
    public List<string> UniqueValues { get; set; }

    [JsonIgnore]
    public string Result { get { return Evaluate(); } }

    public WMIPropertyPivot(string name)
    {
      Name = name;
    }

    private string Evaluate()
    {
      string result = string.Empty;

      if (TypeCount > 1)
      {
        result = "Inconstant type";
      }

      return result;
    }
  }
}
