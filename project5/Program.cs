using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using project5.Areas.Identity.Data;
using project5.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("project5ContextConnection") ?? throw new InvalidOperationException("Connection string 'project5ContextConnection' not found.");

builder.Services.AddDbContext<project5Context>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(10,4,32))));

builder.Services.AddDefaultIdentity<project5User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<project5Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
