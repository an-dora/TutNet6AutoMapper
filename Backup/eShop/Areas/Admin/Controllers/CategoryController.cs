using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Areas.Admin.ViewModels.Category;
using eShop.Database;
using eShop.Database.Entities;
using eShop.WebConfigs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Areas.Admin.Controllers
{
	public class CategoryController : BaseController
	{
		private readonly IMapper _mapper;
		public CategoryController(AppDbContext db, IMapper mapper) : base(db)
		{
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			return View();
		}

		public List<ListItemCategoryVM> GetAll()
		{
			var query = _db.ProductCategories
						.ProjectTo<ListItemCategoryVM>(AutoMapperProfile.categoryAMC)
						.OrderByDescending(c => c.Id);
			var data = query.ToList();
			return data;
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(AddOrUpdateCategoryVM categoryVM)
		{
			// xác thực dữ liệu
			if (ModelState.IsValid == false)
			{
				return View(categoryVM);
			}
			// Lưu vào db
			var category = new ProductCategory();
			category.CreatedAt = DateTime.Now;
			category.UpdatedAt = DateTime.Now;
			_mapper.Map(categoryVM, category);
			_db.ProductCategories.Add(category);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Update(int id)
		{
			var category = _db.ProductCategories
					.Where(c => c.Id == id)
					.ProjectTo<AddOrUpdateCategoryVM>(AutoMapperProfile.categoryAMC)
					.FirstOrDefault();
			if (category == null) return RedirectToAction(nameof(Index));
			return View(category);
		}

		[HttpPost]
		public IActionResult Update(int id, AddOrUpdateCategoryVM categoryVM)
		{
			var category = _db.ProductCategories
					.FirstOrDefault(c => c.Id == id);
			category.UpdatedAt = DateTime.Now;
			_mapper.Map(categoryVM, category);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			if (_db.Products.Any(p => p.CategoryId == id))
			{
				
				//TempData["Err"] = "Không thể xóa vì danh mục đã được sử dụng";
				return Ok(new
				{
					success = false,
					mesg = "Không thể xóa vì danh mục đã được sử dụng"
				});
			}
			else
			{
				var category = _db.ProductCategories.Find(id);
				_db.Remove(category);
				_db.SaveChanges();
			}
			return Ok(new
            {
				success = true
            });
		}
	}
}
