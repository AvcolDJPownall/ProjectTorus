using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Torus.Areas.Identity.Data;
using Torus.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TorusContextConnection") ?? throw new InvalidOperationException("Connection string 'TorusContextConnection' not found.");

builder.Services.AddDbContext<TorusContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<TorusUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TorusContext>();;

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
