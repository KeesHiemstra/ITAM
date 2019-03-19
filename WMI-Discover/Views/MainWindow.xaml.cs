using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WMI_Discover.ViewModels;

namespace WMI_Discover
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    MainModelView ModelView = new MainModelView();

    public MainWindow()
    {
      InitializeComponent();

      DataContext = this;
    }

    private void SearchBotton_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
