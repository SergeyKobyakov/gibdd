﻿using GibddApp.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace GibddApp.Db
{
    class GibddDbContext : DbContext
	{
		static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>  
			builder.AddConsole()
			 .AddFilter(level => level >= LogLevel.Debug));

		public GibddDbContext()
		{			
		}

		public DbSet<Driver> Drivers { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<Protocol> Protocols{ get; set; }
		public DbSet<Violation> Violations { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder
				.UseLoggerFactory(MyLoggerFactory)
                .UseFirebird(ConfigurationManager.ConnectionStrings["GibddDatabase"].ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var car = modelBuilder.Entity<Car>();
			car.Property(x => x.License).HasColumnName("LICENSE");
			car.Property(x => x.Brand).HasColumnName("BRAND");
			car.Property(x => x.Gosno).HasColumnName("GOSNO");
			car.Property(x => x.Model).HasColumnName("MODEL");
			car.Property(x => x.YearCar).HasColumnName("YEAR_CAR");
			car.Property(x => x.Color).HasColumnName("COLOR");
				
			car.ToTable("CAR")
				.HasKey(t => t.Gosno)			
				.HasName("INTEG_23");

			var driver = modelBuilder.Entity<Driver>();
			driver.Property(x => x.License).HasColumnName("LICENSE");				
			driver.Property(x => x.Fio).HasColumnName("FIO");
			driver.Property(x => x.Adres).HasColumnName("ADRES");
			driver.ToTable("DRIVER")
				.HasKey(t => t.License)
				.HasName("INTEG_9");

			var protocol = modelBuilder.Entity<Protocol>();
			protocol.Property(x => x.NoProtocol).HasColumnName("NO_PROTOCOL");
			protocol.Property(x => x.CodeVio).HasColumnName("CODE_VIO");
			protocol.Property(x => x.DateVio).HasColumnName("DATE_VIO");
			protocol.Property(x => x.TimeVio).HasColumnName("TIME_VIO");
			protocol.Property(x => x.License).HasColumnName("LICENSE");
			protocol.ToTable("PROTOCOL")
				.HasNoKey();

			var violation = modelBuilder.Entity<Violation>();
			violation.Property(x => x.Article).HasColumnName("ARTICLE");
			violation.Property(x => x.CodeVio).HasColumnName("CODE_VIO");
			violation.Property(x => x.NameVio).HasColumnName("NAME_VIO");
			violation.Property(x => x.Sanction).HasColumnName("SANCTION");
			violation.ToTable("VIOLATION")
				.HasKey(t => t.CodeVio)
				.HasName("INTEG_21");
		}
	}
}
