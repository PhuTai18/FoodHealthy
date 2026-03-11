using ITHealthy.Data;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// 2️⃣ Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// 3️⃣ Email service
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService, EmailService>();

// 4️⃣ Database connection
var connectionString = builder.Configuration.GetConnectionString("ITHealthyDBConnection");
builder.Services.AddDbContext<ITHealthyDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// 5️⃣ JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

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
app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
