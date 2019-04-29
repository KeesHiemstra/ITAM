﻿using License_Registration.Models;
using License_Registration.ModelViews;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace License_Registration
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public static MainModelView ModelView;

    public MainWindow()
    {
      InitializeComponent();

      Title = $"Banking ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()})";

      ModelView = new MainModelView(this);
      DataContext = ModelView;
    }

    #region Exit command
    private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    private void ExitCommand_Execute(object sender, ExecutedRoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
    #endregion

    private void SoftwareGroupDateGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      ModelView.OpenSoftwareGroup((SoftwareGroup)((DataGrid)sender).CurrentItem);
    }

    private void SoftwareGroupDateGrid_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        ModelView.OpenSoftwareGroup((SoftwareGroup)((DataGrid)sender).CurrentItem);
      }
    }

    private void SoftwareGroupDateGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
    }
  }
}
