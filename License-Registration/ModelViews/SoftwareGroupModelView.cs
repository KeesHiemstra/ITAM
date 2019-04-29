using License_Registration.Models;
using License_Registration.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.ModelViews
{
  public class SoftwareGroupModelView
  {
    SoftwareGroupWindow softwareGroupWindow;

    public SoftwareGroupModelView(SoftwareGroup currentItem)
    {
      softwareGroupWindow = new SoftwareGroupWindow(currentItem);

      softwareGroupWindow.ShowDialog();
    }
  }
}