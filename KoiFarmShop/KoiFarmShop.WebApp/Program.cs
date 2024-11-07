using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Repositories;
using KoiFarmShop.Service.IServices;
using KoiFarmShop.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Auth/Login";
		options.LogoutPath = "/Logout";
		options.AccessDeniedPath = "/Login";

		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

		options.Cookie.Expiration = null;

		options.SlidingExpiration = true;
		options.Cookie.HttpOnly = true;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Customer"));
	options.AddPolicy("RequireAdminRole", policy =>
		policy.RequireRole("Admin"));

	options.AddPolicy("RequireStaffRole", policy =>
		policy.RequireRole("Staff"));
});

builder.Services.AddDbContext<KoiFarmShopContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

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

app.MapRazorPages();

app.Run();
