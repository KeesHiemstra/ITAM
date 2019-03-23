using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WMI_Explorer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private string WmiClass;

    public ObservableCollection<WMIProperty> WMIProperties { get; set; } = new ObservableCollection<WMIProperty>();

    public MainWindow()
    {
      InitializeComponent();
    }

    private void WMIClassComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      MessageBox.Show(e.NewValue.ToString());
    }

    private void WMIClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      WmiClass = (string)((ComboBoxItem)((ComboBox)e.Source).SelectedValue).Content;
      CollectWMI(WmiClass);

    }

    private void CollectWMI(string wmiName)
    {
      WMIProperties.Clear();

      ConnectionOptions options = new ConnectionOptions
      {
        Impersonation = System.Management.ImpersonationLevel.Impersonate
      };

      ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
      scope.Connect();

      //Query system for Operating System information
      ObjectQuery query = new ObjectQuery($"SELECT * FROM {wmiName}");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

      ManagementObjectCollection queryCollection = searcher.Get();

      try
      {
        CollectionCountTextBlock.Text = $"{queryCollection.Count}";

        int collectionIndex = 0;
        foreach (ManagementObject managementObject in queryCollection)
        {
          int propertyIndex = 0;
          foreach (PropertyData propertyData in managementObject.Properties)
          {
            WMIProperties.Add(new WMIProperty(collectionIndex, propertyIndex, propertyData));
            propertyIndex++;
          }
          //App.Log($"Added WMI record ({collectionIndex})");
          collectionIndex++;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error: {ex.Message}", "Exception error");
      }

      WMIPropertiesDataGrid.ItemsSource = WMIProperties;
    }

    private void ClassCode_Click(object sender, RoutedEventArgs e)
    {
      Dictionary<string, string> WmiRecord = new Dictionary<string, string>();

      foreach (WMIProperty item in WMIPropertiesDataGrid.ItemsSource)
      {
        if (item.Select)
        {
          try
          {
            WmiRecord.Add(item.Name, item.Name);
          }
          catch { /* MessageBox.Show($"Error adding {item.Name}"); */ }
        }
      }
      CodeWindow window = new CodeWindow(WmiClass, WmiRecord);
      window.Show();
    }
  }
}
