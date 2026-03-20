using ITHealthy.Data;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICES
// =========================

// MVC
builder.Services.AddControllersWithViews();

// Razor Pages (cho Blazor)
builder.Services.AddRazorPages();

// Blazor Server
builder.Services.AddServerSideBlazor();

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔥 THÊM AUTHENTICATION (QUAN TRỌNG)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";

        // 🔥 BẮT SỰ KIỆN redirect khi chưa login
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/Auth/Login?message=login_required");
            return Task.CompletedTask;
        };

        options.Events.OnRedirectToAccessDenied = context =>
        {
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                context.Response.Redirect("/Admin/Login");
            }
            else
            {
                context.Response.Redirect("/Auth/Login");
            }
            return Task.CompletedTask;
        };
    });



// =========================
// DI SERVICES
// =========================
builder.Services.AddScoped<StaffService>();


builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<CloudinaryService>();


// Đăng ký HttpClient cho MomoService
builder.Services.AddHttpClient();


// Đăng ký IMomoService
builder.Services.AddScoped<IMomoService, MomoService>();


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

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 THỨ TỰ CHUẨN (RẤT QUAN TRỌNG)
app.UseSession();
app.UseAuthentication();   // 👈 thiếu cái này là toang
app.UseAuthorization();

// =========================
// ENDPOINTS
// =========================

// Blazor
app.MapBlazorHub();

// Razor Pages
app.MapRazorPages();

// MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🔥 QUAN TRỌNG
app.MapFallbackToPage("/_Host");

app.Run();