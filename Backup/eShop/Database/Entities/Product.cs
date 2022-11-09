using eShop.Database.Entities.Base;

namespace eShop.Database.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string? CoverImg { get; set; }
		public int InStock { get; set; }
		public int? CategoryId { get; set; }

		public ProductCategory? ProductCategory { get; set; }
	}
}
