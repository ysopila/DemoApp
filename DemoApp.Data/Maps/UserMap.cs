using DemoApp.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DemoApp.Data.Maps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Username).HasMaxLength(50);

            // Table & Column Mappings
            ToTable("User");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Username).HasColumnName("Username");
            Property(t => t.Password).HasColumnName("Password");
            Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            Property(t => t.DateCreated).HasColumnName("DateCreated");
            Property(t => t.AuthToken).HasColumnName("AuthToken");
        }
    }
}