using DemoApp.DataModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DemoApp.Data.Configurations
{
	public class BookConfiguration : EntityTypeConfiguration<Book>
	{
		public BookConfiguration()
		{
			Property(t => t.Copyright).HasMaxLength(200);

			// Table & Column Mappings
			ToTable("Book");
			Property(t => t.Copyright).HasColumnName("Copyright");
			Property(t => t.Published).HasColumnName("Published");
			Property(t => t.AuthorId).HasColumnName("AuthorId");

			HasRequired(t => t.Author).WithMany().HasForeignKey(t => t.AuthorId);
		}
	}
}