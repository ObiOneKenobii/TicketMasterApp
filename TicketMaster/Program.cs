using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Data;
using TicketMaster.Models;
using TicketMaster.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Register HttpClient for services with the correct API endpoint
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://ticketmasterapi-widv.onrender.com/"); // Live API URL
});


builder.Services.AddHttpClient<CartService>(client =>
{
    client.BaseAddress = new Uri("https://ticketmasterapi-widv.onrender.com/"); // Live API URL
});

builder.Services.AddHttpClient<BeverageService>(client =>
{
    client.BaseAddress = new Uri("https://ticketmasterapi-widv.onrender.com/"); // Live API URL
});

// CORS policy configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
    {
        corsBuilder.WithOrigins("https://ticketbookingapp-r524.onrender.com") // Replace with your actual MVC app URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
    });
});

var app = builder.Build();

// Use CORS before any other middleware
app.UseCors();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();

// Ensure authentication is configured correctly
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

app.Run();









