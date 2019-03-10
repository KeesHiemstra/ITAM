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

namespace WpfApp_1910501
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public ObservableCollection<WMIProperty> WMIProperties { get; set; } = new ObservableCollection<WMIProperty>();

    public MainWindow()
    {
      InitializeComponent();

      //DataContext = WMIProperties;
      WMIPropertiesDataGrid.ItemsSource = WMIProperties;

      CollectWMI("Win32_OperatingSystem");
    }

    private void CollectWMI(string wmiName)
    {
      ConnectionOptions options = new ConnectionOptions();
      options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

      ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
      scope.Connect();

      //Query system for Operating System information
      ObjectQuery query = new ObjectQuery($"SELECT * FROM {wmiName}");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

      ManagementObjectCollection queryCollection = searcher.Get();

      QueryCollectionTextBlock.Text = $"Collection count: {queryCollection.Count}";

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
  }
}
