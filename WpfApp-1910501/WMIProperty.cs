using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_1910501
{
  public class WMIProperty
  {
    public bool Select { get; set; }
    public int CollectionIndex { get; set; }
    public int PropertyIndex { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }

    public WMIProperty(int collectionIndex, int propertyIndex, PropertyData data)
    {
      CollectionIndex = collectionIndex;
      PropertyIndex = propertyIndex;
      Name = data.Name;
      Type = data.Type.ToString();
      if (data.Value == null)
      {
        Value = "<null>";
      }
      else if (data.IsArray)
      {
        string array = "";
        foreach (string item in (string[])data.Value)
        {
          if (!string.IsNullOrEmpty(array))
          {
            array += ", ";
          }

          array += item;
        }

        Value = "{" + array + "}";
      }
      else if (data.Type.ToString() == "DataTime")
      {
        Value = ((DateTime)data.Value).ToString("yyyy-MM-dd HH:mm:ss.fff");
      }
      else
      {
        try
        {
          Value = data.Value.ToString();
        }
        catch (Exception ex)
        {
          Value = $"Error: {ex.Message}";
        }
      }
    }
  }
}
