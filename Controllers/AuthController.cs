using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;



namespace ITHealthy.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITHealthyDbContext _context;
        private readonly AdminTokenService _adminTokenService;

        private readonly IEmailService _emailService;

        public AuthController(ITHealthyDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        //Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (customer == null)
            {
                ModelState.AddModelError("", "Email không tồn tại trong hệ thống");
                return View(dto);
            }
            if (!VerifyPassword(dto.Password, customer.PasswordHash))
            {
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View(dto);
            }
            if (customer.IsActive == false)
            {
                await SendOtpAsync(customer.CustomerId);
                TempData["Email"] = customer.Email;
                return RedirectToAction("VerifyOtp");
            }

            HttpContext.Session.SetString("Email", customer.Email);
            HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
            return RedirectToAction("", "Home");
        }



        //  Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _context.Customers.AnyAsync(x => x.Email == dto.Email))
            {
                ModelState.AddModelError("", "Email đã tồn tại");
            }
            if (await _context.Customers.AnyAsync(x => x.Phone == dto.Phone))
            {
                ModelState.AddModelError("", "Số điện thoại đã tồn tại");
            }

            var passwordHash = HashPassword(dto.Password);

            var user = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Phone = dto.Phone,
                CreatedAt = DateTime.Now,
                RoleUser = "User",
                IsActive = false
            };

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            // Gửi OTP xác minh email
            await SendOtpAsync(user.CustomerId);
            TempData["Email"] = user.Email;

            return RedirectToAction("VerifyOtp");
        }




        //  API Xác thực OTP
        public IActionResult VerifyOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDTO dto)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng");
                return View(dto);
            }

            // 🔍 Lấy OTP mới nhất của user
            var otp = await _context.UserOtps
                .Where(x => x.CustomerId == user.CustomerId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (otp == null)
            {
                ModelState.AddModelError("", "Không tìm thấy OTP nào đã được gửi");
                return View(dto);
            }

            if (otp.IsUsed == true)
            {
                ModelState.AddModelError("", "OTP đã được sử dụng");
                return View(dto);
            }

            if (otp.ExpiryTime < DateTime.UtcNow)
            {
                ModelState.AddModelError("", "OTP đã hết hạn");
                return View(dto);
            }

            if (otp.Otpcode != dto.Otp)
            {
                ModelState.AddModelError("", "Mã OTP không đúng hoặc đã bị thay thế");
                return View(dto);
            }

            // ✅ Hợp lệ
            otp.IsUsed = true;
            user.IsActive = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }



        // API Gửi OTP
        public IActionResult SendOtp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequestDTO dto)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng");
                return View("VerifyOtp");
            }
            await SendOtpAsync(user.CustomerId);
            ViewBag.Message = "OTP đã được gửi đến email của bạn";
            return View("VerifyOtp");
        }



        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }





        //  HÀM HỖ TRỢ: gửi OTP
        private async Task SendOtpAsync(int customerId)
        {
            var user = await _context.Customers.FindAsync(customerId);

            var otpCode = new Random().Next(100000, 999999).ToString();

            var userOtp = new UserOtp
            {
                CustomerId = customerId,
                Otpcode = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserOtps.Add(userOtp);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                user.Email!,
                "Mã OTP xác thực ITHealthy",
                $"Xin chào {user.FullName},\n\nMã OTP của bạn là: {otpCode}\nMã sẽ hết hạn sau 5 phút.\n\nITHealthy Team"
            );
        }

        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"]!;
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}
