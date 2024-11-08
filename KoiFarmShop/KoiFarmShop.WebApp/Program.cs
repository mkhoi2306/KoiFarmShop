using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Repositories;
using KoiFarmShop.Service.IServices;
using KoiFarmShop.Service.Services;
using KoiFarmShop.Repository;
using KoiFarmShop.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IKoiFishService, KoiFishService>();

builder.Services.AddScoped<IKoiFishRepository, KoiFishRepository>();

builder.Services.AddScoped<IKoiOrderService, KoiOrderService>();

builder.Services.AddScoped<IKoiOrderRepository, KoiOrderRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Auth/Login";
		options.AccessDeniedPath = "/Auth/Login"; 
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		options.SlidingExpiration = true;
		options.Cookie.HttpOnly = true;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));

	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

	options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
});

builder.Services.AddDbContext<KoiFarmShopContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<KoiFarmShopContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapGet("/", async context =>
{
	context.Response.Redirect("/Index");
});

app.Run();
