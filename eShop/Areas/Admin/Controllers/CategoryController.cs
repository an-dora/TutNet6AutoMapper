using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Areas.Admin.ViewModels.Category;
using eShop.Database;
using eShop.Database.Entities;
using eShop.WebConfigs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eShop.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class CategoryController : BaseController
	{
		private readonly IMapper _mapper;
		public CategoryController(AppDbContext db, IMapper mapper) : base(db)
		{
			_mapper = mapper;
		}
		//Action nay sex chay trc tat ca action
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var method = context.HttpContext.Request.Method;
			if(method == HttpMethod.Post.Method)
            {
                if (!ModelState.IsValid)
                {
					var errorModel = new SerializableError(ModelState);
					context.Result = new BadRequestObjectResult(errorModel);
                }
            }
        }
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetAll( int page = 1, int pSize =1)
		{
			var query = _db.ProductCategories
						.ProjectTo<ListItemCategoryVM>(AutoMapperProfile.categoryAMC)
						.OrderByDescending(c => c.Id);
			var data = query.ToPagedList(page, pSize);
			return Ok(new
			{
				items = data,
				metadata = data.GetMetaData()

			}
				);
		}

		[HttpPost]
		public IActionResult Create([FromBody]AddOrUpdateCategoryVM categoryVM)
		{
			// xác thực dữ liệu
			if (ModelState.IsValid == false)
			{
				return Ok(new
				{
					success = false,
					mesg ="Loi"
				});
			}
			// Lưu vào db
			var category = new ProductCategory();
			category.CreatedAt = DateTime.Now;
			category.UpdatedAt = DateTime.Now;
			_mapper.Map(categoryVM, category);
			_db.ProductCategories.Add(category);
			_db.SaveChanges();
			return Ok(new
			{
				success = true
			});
		}

		//co the xoa
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
		public IActionResult Update(int id, [FromBody]AddOrUpdateCategoryVM categoryVM)
		{
			var category = _db.ProductCategories
					.FirstOrDefault(c => c.Id == id);
			category.UpdatedAt = DateTime.Now;
			_mapper.Map(categoryVM, category);
			_db.SaveChanges();
			return Ok(new
			{
				success = true
			});
		}
		public IActionResult Detail (int id)
        {
			var cate = _db.ProductCategories.Find(id);
			var cateVM = _mapper.Map<AddOrUpdateCategoryVM>(cate);
			return Ok(cateVM);

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
