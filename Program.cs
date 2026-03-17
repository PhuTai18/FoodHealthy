using ITHealthy.Data;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Razor Pages (bắt buộc cho Blazor Server)
builder.Services.AddRazorPages();

// 🔥 Blazor Server
builder.Services.AddServerSideBlazor();

// 🔥 SignalR (quan trọng để Blazor tạo WebSocket)
builder.Services.AddSignalR();

// Session
builder.Services.AddSession();


// Email
builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService, EmailService>();


// Cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<CloudinaryService>();


// Database
builder.Services.AddDbContext<ITHealthyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ITHealthyDBConnection")));

var app = builder.Build();


// Error
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();


// 🔥 Blazor WebSocket Hub
app.MapBlazorHub();

// Razor Pages
app.MapRazorPages();

// MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();