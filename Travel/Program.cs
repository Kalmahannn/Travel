using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Serilog; // Егер Serilog та қосулы болса
using TravelistaMVC.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Travel.Data;
using Travel.Models;

var builder = WebApplication.CreateBuilder(args);


#region DbContext

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region IdentityDbContext
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<AppIdentityDbContext>()
	.AddRoles<IdentityRole>()
	.AddDefaultTokenProviders();

#endregion



// Localization сервистер
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");








builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
builder.Services.AddScoped<LoggingActionFilter>(); // Фильтрді қосу
builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddScoped<CustomResultFilter>();



// Serilog болса
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();


builder.Services.AddScoped<TokenService>();




var app = builder.Build();

// Глобализацияны Middleware ретінде қосу
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ru"),
    new CultureInfo("kk")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseMiddleware<TravelistaMVC.Middlewares.ResponseTimeMiddleware>(); //Response Time Middleware 
app.UseMiddleware<TravelistaMVC.Middlewares.RequestLoggingMiddleware>(); // RequestLoggingMiddleware (біздің Middleware)
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
