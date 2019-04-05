using ITAMLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAMLib
{
	public class ITAMDbContext : DbContext
	{
		public ITAMDbContext(string DbConnection) : base(DbConnection) { }

		public DbSet<Win32_Product_SQL> Product { get; set; }

		internal static void SeedData(ITAMDbContext context)
		{
			context.Database.CreateIfNotExists();
		}
	}
}
