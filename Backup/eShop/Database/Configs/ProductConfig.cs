using eShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Database.Configs
{
	public class ProductConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(200);

			builder.Property(x => x.Description).HasColumnType("ntext");

			builder.Property(x => x.CoverImg).HasMaxLength(500);

			// khóa ngoại
			builder.HasOne(x => x.ProductCategory)
				.WithMany()
				.IsRequired(false)
				.HasForeignKey(x => x.CategoryId);
		}
	}
}
