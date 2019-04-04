using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInventory
{
	public class ITAMDbContext : DbContext
	{
		public ITAMDbContext(DbContextOptions<ITAMDbContext> options) : base(options) { }

		public DbSet<Win32_Product_SQL> Products { get; set; }
	}
}
