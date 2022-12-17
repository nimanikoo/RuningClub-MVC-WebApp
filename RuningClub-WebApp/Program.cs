using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Models;
using RuningClub_WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRaceService, RaceService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IPhotosService, PhotosService>();
builder.Services.Configure<CloudSettings>(builder.Configuration.GetSection("CloudSettings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add DB Service
builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Identity
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDataContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

AppDbIntializer.SeedingData(app);
AppDbIntializer.SeedUsersAndRolesAsync(app);
 
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
