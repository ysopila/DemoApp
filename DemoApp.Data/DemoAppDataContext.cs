using System.Data.Entity;
using DemoApp.Data.Maps;
using DemoApp.Data.Entities;

namespace DemoApp.Data
{
    public class DemoAppDataContext : DbContext
    {
        public DemoAppDataContext() : base("DemoAppContext") { }

        static DemoAppDataContext()
        {
            Database.SetInitializer<DemoAppDataContext>(new DemoAppDataContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContentObjectMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new BookMap());
        }

        public DbSet<ContentObject> Content { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
    }
}