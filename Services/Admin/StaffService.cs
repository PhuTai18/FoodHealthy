using ITHealthy.Data;
using ITHealthy.DTOs;
using ITHealthy.Models;
using ITHealthy.Services;
using Microsoft.EntityFrameworkCore;

namespace ITHealthy.Services.Admin
{
    public class StaffService : IStaffService
    {
        private readonly ITHealthyDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public StaffService(ITHealthyDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<StaffResponseDTO>> GetAllAsync()
        {
            return await _context.Staff
                .Include(s => s.Store)
                .Select(s => new StaffResponseDTO
                {
                    StaffId = s.StaffId,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    Role = s.RoleStaff,
                    IsActive = s.IsActive ?? true,
                    Avatar = s.Avatar,
                    StoreName = s.Store != null ? s.Store.StoreName : null,
                    StoreId = s.StoreId
                })
                .OrderByDescending(x => x.StaffId)
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<StaffResponseDTO?> GetByIdAsync(int id)
        {
            return await _context.Staff
                .Include(s => s.Store)
                .Where(s => s.StaffId == id)
                .Select(s => new StaffResponseDTO
                {
                    StaffId = s.StaffId,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    Role = s.RoleStaff,
                    IsActive = s.IsActive ?? true,
                    Avatar = s.Avatar,
                    StoreName = s.Store != null ? s.Store.StoreName : null,
                    StoreId = s.StoreId
                })
                .FirstOrDefaultAsync();
        }

        // =========================
        // CREATE
        // =========================
        public async Task<(bool Success, string Message)> CreateAsync(StaffRequestDTO request)
        {
            try
            {
                // Validate
                if (await _context.Staff.AnyAsync(x => x.Email == request.Email))
                    return (false, "Email đã tồn tại");

                if (!string.IsNullOrEmpty(request.Phone) &&
                    await _context.Staff.AnyAsync(x => x.Phone == request.Phone))
                    return (false, "Số điện thoại đã tồn tại");

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
                    Gender = request.Gender,
                    Dob = request.Dob,
                    RoleStaff = request.RoleStaff,
                    HireDate = request.HireDate,
                    Avatar = avatarUrl,
                    IsActive = request.IsActive ?? true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash ?? "123456")
                };

                _context.Staff.Add(staff);
                await _context.SaveChangesAsync();

                return (true, "Thêm nhân viên thành công");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi: " + ex.Message);
            }
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<(bool Success, string Message)> UpdateAsync(int id, StaffRequestDTO request)
        {
            try
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                    return (false, "Không tìm thấy nhân viên");

                // Check email trùng (trừ chính nó)
                if (!string.IsNullOrEmpty(request.Email) &&
                    await _context.Staff.AnyAsync(x => x.Email == request.Email && x.StaffId != id))
                {
                    return (false, "Email đã tồn tại");
                }

                // Upload avatar mới
                if (request.Avatar != null && request.Avatar.Length > 0)
                {
                    var avatarUrl = await _cloudinaryService.UploadImageAsync(request.Avatar);
                    staff.Avatar = avatarUrl;
                }

                // Update field
                staff.FullName = request.FullName;
                staff.Email = request.Email;
                staff.Phone = request.Phone;
                staff.Gender = request.Gender;
                staff.Dob = request.Dob;
                staff.RoleStaff = request.RoleStaff;
                staff.StoreId = request.StoreId;
                staff.HireDate = request.HireDate;
                staff.IsActive = request.IsActive ?? true;

                if (!string.IsNullOrEmpty(request.PasswordHash))
                {
                    staff.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
                }

                await _context.SaveChangesAsync();

                return (true, "Cập nhật nhân viên thành công");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi: " + ex.Message);
            }
        }

        // =========================
        // DELETE
        // =========================
        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            try
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                    return (false, "Không tìm thấy nhân viên");

                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();

                return (true, "Xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi: " + ex.Message);
            }
        }
    }
}