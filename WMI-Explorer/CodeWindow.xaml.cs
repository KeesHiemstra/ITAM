using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WMI_Explorer
{
  /// <summary>
  /// Interaction logic for CodeWindow.xaml
  /// </summary>
  public partial class CodeWindow : Window
  {
    private string WmiClass;
    private Dictionary<string, string> WmiRecord;

    public CodeWindow(string wmiClass, Dictionary<string, string> wmiRecord)
    {
      WmiClass = wmiClass;
      WmiRecord = wmiRecord;

      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      WriteCode();
    }

    private async Task WriteCode()
    {
      await WriteCodeRecord();
      await WriteCodeList();
      await WriteCodeExtra();
    }

    private async Task WriteCodeRecord()
    {
      CodeRecordTextBox.Text += $"\tpublic class {WmiClass}\n";
      CodeRecordTextBox.Text += "\t{\n";
      CodeRecordTextBox.Text += $"\t\tpublic {WmiClass}(WmiRecord data)\n";
      CodeRecordTextBox.Text += "\t\t{\n";
      // Loop through members
      foreach (var item in WmiRecord)
      {
        CodeRecordTextBox.Text += $"\t\t\t{item.Key} = data.Properties[\"{item.Value}\"];\n";
      }
      //End loop
      CodeRecordTextBox.Text += "\t\t}\n";
      CodeRecordTextBox.Text += "\n";
      // Loop through members
      foreach (var item in WmiRecord)
      {
        CodeRecordTextBox.Text += $"\t\tpublic string {item.Key} " + "{ get; private set; }\n";
      }
      // End loop
      CodeRecordTextBox.Text += "\t}\n";
    }

    private async Task WriteCodeList()
    {
      CodeListTextBox.Text += $"\tpublic class {WmiClass}_List\n";
      CodeListTextBox.Text += "\t{\n";
      CodeListTextBox.Text += "\t\tpublic string ComputerName { get; set; }\n";
      CodeListTextBox.Text += $"\t\tpublic List<{WmiClass}> Items = new List<{WmiClass}>();\n";
      CodeListTextBox.Text += "\n";
      CodeListTextBox.Text += $"\t\tpublic {WmiClass}_List(string WmiClass, string members)\n";
      CodeListTextBox.Text += "\t\t{\n";
      CodeListTextBox.Text += "\t\t\tComputerName = System.Environment.MachineName;\n";
      CodeListTextBox.Text += "\t\t\tCollectWmiClass(WmiClass, members);\n";
      CodeListTextBox.Text += "\t\t}\n";
      CodeListTextBox.Text += "\n";
      CodeListTextBox.Text += "\t\tprivate async void CollectWmiClass(string wmiClass, string members)\n";
      CodeListTextBox.Text += "\t\t{\n";
      CodeListTextBox.Text += "\t\t\tItems.Clear();\n";
      CodeListTextBox.Text += "\n";
      CodeListTextBox.Text += "\t\t\tforeach (ManagementObject managementObject in WmiList.GetCollection(wmiClass, members))\n";
      CodeListTextBox.Text += "\t\t\t{\n";
      CodeListTextBox.Text += $"\t\t\t\ttry\n";
      CodeListTextBox.Text += "\t\t\t\t{\n";
      CodeListTextBox.Text += "\t\t\t\t\tWmiRecord record = new WmiRecord(members);\n";
      CodeListTextBox.Text += "\t\t\t\t\tforeach (PropertyData propertyData in managementObject.Properties)\n";
      CodeListTextBox.Text += "\t\t\t\t\t{\n";
      CodeListTextBox.Text += "\t\t\t\t\t\trecord.ProcessProperty(propertyData);\n";
      CodeListTextBox.Text += "\t\t\t\t\t}\n";
      CodeListTextBox.Text += $"\t\t\t\t\tItems.Add(new {WmiClass}(record));\n";
      CodeListTextBox.Text += "\t\t\t\t}\n";
      CodeListTextBox.Text += "\t\t\t\tcatch { }\n";
      CodeListTextBox.Text += "\t\t\t}\n";
      CodeListTextBox.Text += "\t\t}\n";
      CodeListTextBox.Text += "\t}\n";
    }

    private async Task WriteCodeExtra()
    {
      string Members = "";
      foreach (var item in WmiRecord)
      {
        if (!string.IsNullOrEmpty(Members))
        {
          Members += ",";
        }
        Members += item.Value;
      }
      CodeExtraTextBox.Text += $"\t\t{WmiClass}_List {WmiClass.ToLower()} = new {WmiClass}_List(\"{WmiClass}\", \"{Members}\");\n";
    }

  }
}
