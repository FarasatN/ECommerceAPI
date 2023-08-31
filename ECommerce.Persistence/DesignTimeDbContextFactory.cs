using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
	{
		public ECommerceAPIDbContext CreateDbContext(string[] args)
		{

			DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
			dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
			//dbContextOptionsBuilder.UseNpgsql("Username=postgres;Password=123456;Host=localhost;Port=5432;Database=ECommerceAPIDb;");

			return new(dbContextOptionsBuilder.Options);
		}
	}
}
