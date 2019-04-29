using License_Registration.Models;
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

namespace License_Registration.Views
{
  /// <summary>
  /// Interaction logic for SoftwareGroupWindow.xaml
  /// </summary>
  public partial class SoftwareGroupWindow : Window
  {
    public SoftwareGroupWindow(SoftwareGroup softwareGroup)
    {
      InitializeComponent();

      DataContext = softwareGroup;
      //SoftwareItemsDataGrid.ItemsSource = softwareGroup.SoftwareItems;
    }
  }
}
