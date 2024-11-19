using MilkStore.Models;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register MilkstoreDbContext with the connection string
builder.Services.AddDbContext<MilkstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MilkstoreDbConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
