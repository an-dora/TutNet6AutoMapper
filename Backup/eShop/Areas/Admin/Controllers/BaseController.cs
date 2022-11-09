using eShop.Database;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Areas.Admin.Controllers
{
	public class BaseController : Controller
	{
		protected readonly AppDbContext _db;
		public BaseController(AppDbContext db)
		{
			_db = db;
		}
	}
}
