using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.Models
{
  [Table("SoftwareItem")]
  public class SoftwareItem
  {
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }

    public string Name { get; set; }
    public string Vendor { get; set; }
    public string Version { get; set; }
    public string IdentifyingNumber { get; set; }

  }
}
