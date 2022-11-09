using AutoMapper;
using eShop.Areas.Admin.ViewModels.Category;
using eShop.Database.Entities;

namespace eShop.WebConfigs
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			// Mapper cho danh mục sản phẩm

			// Mapper 2 chiều từ ViewModel <=> entity
			CreateMap<AddOrUpdateCategoryVM, ProductCategory>().ReverseMap();
		}

		// Cấu hình mapper cho việc select dữ liệu vào viewModel
		public static MapperConfiguration categoryAMC = new(opt =>
		{
			// opt.CreateMap<ListItemCategoryVM, ProductCategory>().ReverseMap();
			opt.CreateMap<ProductCategory, ListItemCategoryVM>();
			opt.CreateMap<ProductCategory, AddOrUpdateCategoryVM>();
		});

	}
}
