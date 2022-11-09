using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Areas.Admin.ViewModels.Product;
using eShop.Database;
using eShop.WebConfigs;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;

        public ProductController(AppDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();

        }
        public List<ListItemProductVM> GetAll()
        {
            var query = _db.Products
            .ProjectTo<ListItemProductVM>(AutoMapperProfile.productAMC)
            .OrderByDescending(c => c.Id);
            var data = query.ToList();
            return data;

        }
        [HttpPost]
        public IActionResult Update(int id, [FromBody] AddOrUpdateProductVM productVM)
        {
            var product = _db.Products
                    .FirstOrDefault(c => c.Id == id);
            product.UpdatedAt = DateTime.Now;
            _mapper.Map(productVM, product);
            _db.SaveChanges();
            return Ok(new
            {
                success = true
            });
        }
        public IActionResult Detail(int id)
        {
            var product = _db.Products.Find(id);
            var productVM = _mapper.Map<AddOrUpdateProductVM>(product);
            return Ok(productVM);

        }
        public IActionResult Delete(int id)
        {

            var product = _db.Products.Find(id);
            _db.Remove(product);
            _db.SaveChanges();
            return Ok(new
            {
                success = true
            });
        }

    }
}
