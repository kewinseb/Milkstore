using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register MilkstoreDbContext with the connection string
builder.Services.AddDbContext<MilkstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MilkstoreDbConnectionString")));

// Add Authentication services with Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/HomeLogin/Login"; // Path to login page
        options.LogoutPath = "/HomeLogin/Logout"; // Path to logout page
    });

// Add Session services if you're managing sessions
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// Use authentication and session before authorization
app.UseAuthentication(); // Add this line
app.UseAuthorization();
app.UseSession(); // Add this line if you're using sessions

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeLogin}/{action=Login}/{id?}");
app.Run();
