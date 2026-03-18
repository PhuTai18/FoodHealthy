using ITHealthy.Data;
using ITHealthy.Models;
using ITHealthy.Services;
using ITHealthy.Services.Admin;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICES
// =========================

// MVC
builder.Services.AddControllersWithViews();

// Razor Pages (bắt buộc cho Blazor)
builder.Services.AddRazorPages();

// 🔥 Blazor Server
builder.Services.AddServerSideBlazor();

// ❌ KHÔNG CẦN dòng này (Blazor đã dùng sẵn)
// builder.Services.AddSignalR();

// Session
builder.Services.AddSession();

// =========================
// DI SERVICES
// =========================
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<CloudinaryService>();

// =========================
// DATABASE
// =========================
builder.Services.AddDbContext<ITHealthyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ITHealthyDBConnection")));

var app = builder.Build();

// =========================
// MIDDLEWARE
// =========================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// =========================
// ENDPOINTS
// =========================

// 🔥 BẮT BUỘC cho Blazor
app.MapBlazorHub();

// Razor Pages (cho _Host)
app.MapRazorPages();

// MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();