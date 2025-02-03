using Microsoft.EntityFrameworkCore;
using MilkStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register MilkstoreDbContext with the connection string
builder.Services.AddDbContext<MilkstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MilkstoreDbConnectionString")));

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
    options.Cookie.HttpOnly = true; // Prevent client-side access
    options.Cookie.IsEssential = true; // Mark the cookie as essential
});

var app = builder.Build();

// Ensure DB connection works on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MilkstoreDbContext>();

    try
    {
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

// Enable session middleware
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeLogin}/{action=Login}/{id?}");
app.Run();
