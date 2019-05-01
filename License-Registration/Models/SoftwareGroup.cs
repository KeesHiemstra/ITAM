using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.Models
{
  [Table("SoftwareGroup")]
  public class SoftwareGroup
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Vendor { get; set; }
    public string Version { get; set; }

    //public virtual ICollection<SoftwareItem> SoftwareItems { get; set; }
    public virtual ObservableCollection<SoftwareItem> SoftwareItems { get; set; }
  }
}
