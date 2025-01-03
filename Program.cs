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

// Ensure DB connection works on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MilkstoreDbContext>();

    try
    {
        // Check if the database can be connected
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Successfully connected to the database.");
        }
        else
        {
            Console.WriteLine("Failed to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        // Log the exception if connection fails
        Console.WriteLine($"Error connecting to the database: {ex.Message}");
    }
}

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
    pattern: "{controller=ShipmentDetails}/{action=Index}/{id?}");

app.Run();
