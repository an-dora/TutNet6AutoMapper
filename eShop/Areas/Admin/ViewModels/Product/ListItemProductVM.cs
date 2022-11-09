namespace eShop.Areas.Admin.ViewModels.Product
{
    public class ListItemProductVM
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public int InStock { get; set; }
		//lay thong tin khoa ngoai
		public string? CategoryName { get; set; }
	}
}
