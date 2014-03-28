using System.Data.Entity.ModelConfiguration;
using DemoApp.DataModel.Entities;

namespace DemoApp.Data.Configurations
{
	public class ContentObjectConfiguration : EntityTypeConfiguration<ContentObject>
	{
		public ContentObjectConfiguration()
		{
			HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
			ToTable("ContentObject");
			Property(t => t.Id).HasColumnName("Id");
			Property(t => t.Name).HasColumnName("Name");
			Property(t => t.Description).HasColumnName("Description");
			Property(t => t.Photo).HasColumnName("Photo");
		}
	}
}