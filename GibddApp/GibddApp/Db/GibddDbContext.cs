using GibddApp.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Configuration;

namespace GibddApp.Db
{
    internal class GibddDbContext : DbContext
	{
		static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>  
			builder.AddNLog()
			 .AddFilter(level => level >= LogLevel.Debug));

		public GibddDbContext()
		{					
			
		}

		public DbSet<Driver> Drivers { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<Protocol> Protocols{ get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<UserPrivilege> UserPrivileges { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var connectionString = ConfigurationManager.ConnectionStrings["GibddDatabase"].ConnectionString.TrimEnd(';')
				+$";user={LoginInfo.Login};password={LoginInfo.Password}";
				
			optionsBuilder
				.UseLoggerFactory(MyLoggerFactory)
                .UseFirebird(connectionString);
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
				.HasKey(t => t.NoProtocol)
				.HasName("INTEG_52");

			var violation = modelBuilder.Entity<Violation>();
			violation.Property(x => x.Article).HasColumnName("ARTICLE");
			violation.Property(x => x.CodeVio).HasColumnName("CODE_VIO");
			violation.Property(x => x.NameVio).HasColumnName("NAME_VIO");
			violation.Property(x => x.Sanction).HasColumnName("SANCTION");
			violation.ToTable("VIOLATION")
				.HasKey(t => t.CodeVio)
				.HasName("INTEG_21");

			var userPrivilege = modelBuilder.Entity<UserPrivilege>();
			userPrivilege.Property(x => x.UserName).HasColumnName("RDB$USER");
			userPrivilege.Property(x => x.TableName).HasColumnName("RDB$RELATION_NAME");
			userPrivilege.Property(x => x.Privilege).HasColumnName("RDB$PRIVILEGE");
			userPrivilege.ToTable("RDB$USER_PRIVILEGES")
				.HasNoKey();
		}
	}
}
