using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License_Registration.Models
{
	[Table("Win32_Product")]
	public class Win32_Product_SQL : Win32_Product, IDisposable
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string ComputerName { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime DTCreation { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DTCheck { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DTDeletion { get; set; }

		public void Dispose() { }
	}
}
