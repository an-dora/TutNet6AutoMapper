using eShop.Areas.Admin.ViewModels.Account;
using eShop.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShop.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AccountController : BaseController
    {
        public AccountController(AppDbContext db) : base(db)
        {
        }
        [Route("/login")]
        [Route("/{area}/{controller}/{action}")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("/{area}/{controller}/{action}")]
        [AllowAnonymous]
        public IActionResult Login(LoginVM model)
        {
            var data = _db.Users.SingleOrDefault(x => x.Username == model.UserName);
            if(data is null)
            {
                TempData["Err"] = "Username khong hop le";
                return View(model);
            }
#warning Hash password
            if(model.Password != data.Password)
            {
                TempData["Err"] = "Password khong hop le";
                return View(model);
            }
            // luu thong tin user
            var claims = new List<Claim> {
                            new Claim(ClaimTypes.NameIdentifier,data.Id.ToString()),
                            new Claim("UserName",data.Username),
                            new Claim("FullName",data.Fullname),
                            new Claim(ClaimTypes.Role,data.Role?.ToString())
                        };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(claimsIdentity);
            var authenPropeties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(10),
                IsPersistent = (bool)model.RememberMe
            };
            HttpContext.SignInAsync("Cookies", principal, authenPropeties).Wait();
            return RedirectToAction("Category", "Index", new { area = "Admin" });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Redirect("/login");
        }
    }
}
