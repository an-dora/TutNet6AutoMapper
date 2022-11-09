using eShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Database.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(200);
            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Password).HasMaxLength(200);

            builder.Property(x => x.Fullname).HasMaxLength(200);
            builder.Property(x => x.Address).HasMaxLength(200);
        }
    }
}
