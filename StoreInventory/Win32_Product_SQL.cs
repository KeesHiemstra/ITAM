using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInventory
{
	[Table("Win32_Product")]
	public class Win32_Product_SQL : Win32_Product, IDisposable
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string ComputerName { get; set; }

		public void Dispose()
		{
			
		}
	}
}
