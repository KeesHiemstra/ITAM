using ITAM_Software.ModelViews;
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

namespace ITAM_Software
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainModelView ModelView;

		public MainWindow()
		{
			InitializeComponent();

			Title += $" ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()})";

			ModelView = new MainModelView(this);
			FilesListView.ItemsSource = ModelView.JsonFiles;
		}

		private void FilesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//string selected = (string)((ListView)e.Source).SelectedValue;
			ModelView.SelectedWMIClass((string)((ListView)e.Source).SelectedValue);
		}
	}
}
