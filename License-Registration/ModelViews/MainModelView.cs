using License_Registration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.ModelViews
{
  public class MainModelView
  {
    private static string DbConnection;
    private readonly MainWindow Main;

    public MainModelView(MainWindow main)
    {
      Main = main;
      SetDbConnection();

      using (ITAMDbContext db = new ITAMDbContext(DbConnection))
      {
        ObservableCollection<SoftwareGroup> groups = new ObservableCollection<SoftwareGroup>();

        Main.SoftwareGroupDateGrid.ItemsSource = groups;
      }
    }

    private void SetDbConnection()
    {
      if (Directory.Exists("\\\\NASServer\\Data\\Kees"))
      {
        DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
      }
      else
      {
        DbConnection = @"Trusted_Connection=True;Data Source=(Local);Database=ITAM;MultipleActiveResultSets=true";
      }
    }
  }
}
