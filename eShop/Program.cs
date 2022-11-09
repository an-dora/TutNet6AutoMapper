using AutoMapper;
using eShop.Database;
using eShop.WebConfigs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Kết nối db
builder.Services.AddDbContext<AppDbContext>(opts =>
{
	var connectionString = builder.Configuration.GetConnectionString("Db");
	opts.UseSqlServer(connectionString);
});

// Cấu hình AutoMapper
var mapperConfig = new MapperConfiguration(config =>
{
	config.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Cau hinh dang nhap bang cookie
builder.Services.AddAuthentication("Cookies").AddCookie(opt =>
{
	opt.LoginPath = "Login";
	opt.ExpireTimeSpan = TimeSpan.FromHours(8);
	opt.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();//
app.UseAuthorization(); //phan quyen

app.MapAreaControllerRoute(
	name: "AdminArea",
	areaName: "Admin",
	pattern: "/Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

PathHelper.SetWebRootPath(app.Environment.WebRootPath);

app.Run();
