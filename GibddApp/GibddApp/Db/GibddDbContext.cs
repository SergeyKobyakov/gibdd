using GibddApp.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibddApp.Db
{
    class GibddDbContext : DbContext
	{
		static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>  
			builder.AddConsole()
			 .AddFilter(level => level >= LogLevel.Debug));

		readonly string _connectionString;

		public GibddDbContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<Driver> Drivers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder
				.UseLoggerFactory(MyLoggerFactory)
				.UseFirebird(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var driver = modelBuilder.Entity<Driver>();				

			driver.Property(x => x.License).HasColumnName("LICENSE");				
			driver.Property(x => x.Fio).HasColumnName("FIO");
			driver.Property(x => x.Adres).HasColumnName("ADRES");
			driver.ToTable("DRIVER")
				.HasKey(t => t.License)
				.HasName("INTEG_9");
		}
	}
}
