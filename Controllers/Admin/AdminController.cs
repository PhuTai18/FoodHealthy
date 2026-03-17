using ITHealthy.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITHealthyDbContext _context;

        public AdminController(ITHealthyDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Login
        public IActionResult Login()
        {
            return View("~/Views/Admin/Login/Login.cshtml");
        }

        // POST: /Admin/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s => s.Email == email);

            if (staff == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            if (staff.PasswordHash != password)
            {
                ViewBag.Error = "Sai mật khẩu";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            // if (staff.PasswordHash != AuthController.HashPassword(password))
            // {
            //     ViewBag.Error = "Sai mật khẩu";
            //     return View("~/Views/Admin/Login/Login.cshtml");
            // }

            if (staff.RoleStaff != "Admin")
            {
                ViewBag.Error = "Bạn không phải Admin";
                return View("~/Views/Admin/Login/Login.cshtml");
            }

            HttpContext.Session.SetString("Admin", staff.Email);

            return Redirect("/Admin/Dashboard");
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return Redirect("/Admin/Login");
            }

            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}