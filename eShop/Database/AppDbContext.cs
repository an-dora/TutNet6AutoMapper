using eShop.Database.Configs;
using eShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using eShop.Areas.Admin.ViewModels.Product;
using eShop.Areas.Admin.ViewModels.Account;

namespace eShop.Database
{
	public class AppDbContext : DbContext
	{
		public DbSet<ProductCategory> ProductCategories{ get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }

		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// gọi class để tạo cấu hình
			modelBuilder.ApplyConfiguration(new ProductCategoryConfig());
			modelBuilder.ApplyConfiguration(new ProductConfig());
			modelBuilder.ApplyConfiguration(new UserConfig());
		}

		public DbSet<eShop.Areas.Admin.ViewModels.Product.ListItemProductVM> ListItemProductVM { get; set; }

		public DbSet<eShop.Areas.Admin.ViewModels.Account.LoginVM> LoginVM { get; set; }
	}
}
