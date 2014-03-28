using System.Data.Entity;
using DemoApp.Data.Configurations;
using DemoApp.DataModel.Interfaces;

namespace DemoApp.Data
{
	public class DemoAppDataContext : DbContext, IDbContext
	{
		public DemoAppDataContext()
			: base("DemoAppContext")
		{
			this.Configuration.LazyLoadingEnabled = false;
		}

		static DemoAppDataContext()
		{
			Database.SetInitializer<DemoAppDataContext>(new DemoAppDataContextInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new ContentObjectConfiguration());
			modelBuilder.Configurations.Add(new BookConfiguration());
			modelBuilder.Configurations.Add(new PersonConfiguration());
			modelBuilder.Configurations.Add(new UserConfiguration());
		}
	}
	internal class DemoAppDataContextInitializer : MigrateDatabaseToLatestVersion<DemoAppDataContext, DemoApp.Data.Migrations.Configuration>
	{
	}
}