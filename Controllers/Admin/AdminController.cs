using ITHealthy.Data;
using ITHealthy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ITHealthy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public AdminController(ITHealthyDbContext context)
        {
            _context = context;
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Admin/Login/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            email = email?.Trim();

            var staff = await _context.Staff
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Email == email);

            if (staff == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            if (staff.IsActive == false)
            {
                ViewBag.Error = "Tài khoản đã bị khóa";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            if (staff.PasswordHash != AuthController.HashPassword(password))
            {
                ViewBag.Error = "Sai mật khẩu";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            if (staff.RoleStaff != "Admin")
            {
                ViewBag.Error = "Bạn không phải Admin";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, staff.Email),
        new Claim("StaffId", staff.StaffId.ToString()),
        new Claim(ClaimTypes.Role, "Admin")
    };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                });

            return RedirectToAction("Dashboard");
        }

        // ================= DASHBOARD =================

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }

        // ================= LOGOUT =================

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}