using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Controllers
{
    [Route("admin/staff")]
    public class StaffController : Controller
    {
        private readonly ITHealthyDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public StaffController(ITHealthyDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        // =============================
        // GET ALL STAFF
        // URL: /admin/staff
        // =============================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var staffs = await _context.Staff
                .Include(s => s.Store)
                .ToListAsync();

            return View("~/Views/Admin/Staff/Index.cshtml", staffs);
        }

        // =============================
        // STAFF DETAIL
        // URL: /admin/staff/detail/1
        // =============================
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var staff = await _context.Staff
                .Include(s => s.Store)
                .FirstOrDefaultAsync(s => s.StaffId == id);

            if (staff == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Admin/Staff/Detail.cshtml", staff);
        }

        // =============================
        // CREATE STAFF
        // URL: /admin/staff/create
        // =============================
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Staff/Create.cshtml");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StaffRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return View("~/Views/Admin/Staff/Create.cshtml", request);
            }

            if (await _context.Staff.AnyAsync(s => s.Email == request.Email))
            {
                TempData["Error"] = "Email đã tồn tại.";
                return View("~/Views/Admin/Staff/Create.cshtml", request);
            }

            if (await _context.Staff.AnyAsync(s => s.Phone == request.Phone))
            {
                TempData["Error"] = "Số điện thoại đã tồn tại.";
                return View("~/Views/Admin/Staff/Create.cshtml", request);
            }

            string? avatarUrl = null;

            if (request.Avatar != null && request.Avatar.Length > 0)
            {
                avatarUrl = await _cloudinaryService.UploadImageAsync(request.Avatar);
            }

            var staff = new Staff
            {
                StoreId = request.StoreId,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = AuthController.HashPassword(request.PasswordHash ?? "123456"),
                Gender = request.Gender,
                Dob = request.Dob,
                RoleStaff = request.RoleStaff,
                HireDate = request.HireDate,
                Avatar = avatarUrl,
                IsActive = true
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm nhân viên thành công";
            return RedirectToAction(nameof(Index));
        }

        // =============================
        // EDIT STAFF
        // URL: /admin/staff/edit/1
        // =============================
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Admin/Staff/Edit.cshtml", staff);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, StaffRequestDTO request)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (request.Avatar != null && request.Avatar.Length > 0)
                {
                    var avatarUrl = await _cloudinaryService.UploadImageAsync(request.Avatar);
                    staff.Avatar = avatarUrl;
                }

                staff.FullName = request.FullName;
                staff.Phone = request.Phone;
                staff.Email = request.Email;
                staff.Gender = request.Gender;
                staff.Dob = request.Dob;
                staff.RoleStaff = request.RoleStaff;
                staff.StoreId = request.StoreId;
                staff.HireDate = request.HireDate;
                staff.IsActive = request.IsActive ?? true;

                if (!string.IsNullOrEmpty(request.PasswordHash))
                {
                    staff.PasswordHash = AuthController.HashPassword(request.PasswordHash);
                }

                await _context.SaveChangesAsync();

                TempData["Success"] = "Cập nhật nhân viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật nhân viên";
                return RedirectToAction(nameof(Index));
            }
        }

        // =============================
        // DELETE STAFF
        // URL: /admin/staff/delete/1
        // =============================
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Admin/Staff/Delete.cshtml", staff);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên";
                return RedirectToAction(nameof(Index));
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa nhân viên thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}