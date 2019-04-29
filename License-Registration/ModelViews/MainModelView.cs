using License_Registration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

      OpenTable();
    }

    private async void OpenTable()
    {
      using (ITAMDbContext db = new ITAMDbContext(DbConnection))
      {
        ObservableCollection<SoftwareGroup> swGroups = new ObservableCollection<SoftwareGroup>();
        var SwGroups = await(from g in db.SwGroups
                           select g).ToListAsync();

        Main.SoftwareGroupDateGrid.ItemsSource = SwGroups;
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

    internal void OpenSoftwareGroup(SoftwareGroup currentItem)
    {
      SoftwareGroupModelView softwareGroupModelView = new SoftwareGroupModelView(currentItem);
    }
  }
}
