using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Fuel_Price_Prophecy.Models
{
	public class MyDbContext : DbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Database.SetInitializer<MyDbContext>(null);
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Client>()
						.HasOptional(s => s.ClientProfile)
						.WithRequired(ad => ad.Client);

		}

		public DbSet<Client> ClientsTable { get; set; }
		public DbSet<ClientProfile> ClientProfileTable { get; set; }
		public DbSet<FuelQuote> FuelQuotes { get; set; }
	}
}