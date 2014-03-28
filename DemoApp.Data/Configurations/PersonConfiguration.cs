using System.Data.Entity.ModelConfiguration;
using DemoApp.DataModel.Entities;

namespace DemoApp.Data.Configurations
{
	public class PersonConfiguration : EntityTypeConfiguration<Person>
	{
		public PersonConfiguration()
		{
			Property(t => t.FirstName).HasMaxLength(50);
			Property(t => t.LastName).HasMaxLength(50);

			// Table & Column Mappings
			ToTable("Person");
			Property(t => t.FirstName).HasColumnName("FirstName");
			Property(t => t.LastName).HasColumnName("LastName");
			Property(t => t.Gender).HasColumnName("Gender");
			Property(t => t.BirthDate).HasColumnName("BirthDate");
		}
	}
}