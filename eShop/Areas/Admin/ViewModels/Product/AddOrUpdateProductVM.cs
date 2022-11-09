using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eShop.Areas.Admin.ViewModels.Product
{
    public class AddOrUpdateProductVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[MinLength(3, ErrorMessage = "{0} không được ít hơn 3 ký tự")]
		[DisplayName("Tên sản phẩm")]
		public string Name { get; set; }
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[Range(1, 0, ErrorMessage = "{0} phải lớn hơn {1}")]
		[DisplayName("Giá")]
		public decimal Price { get; set; }

		[Range(1, 0, ErrorMessage = "{0} phải lớn hơn {1}")]
		[DisplayName("Giảm")]
		public decimal? DiscountPrice { get; set; }

		[Required(ErrorMessage = "{0} là bắt buộc")]
		[Range(1, 0, ErrorMessage = "{0} phải lớn hơn {1}")]
		[DisplayName("Số lượng")]
		public int InStock { get; set; }
		//lay thong tin khoa ngoai
		public string? CategoryName { get; set; }
	}
}
